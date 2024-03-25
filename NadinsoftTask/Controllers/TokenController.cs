
﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NadinsoftTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1", Deprecated = false)]
    [Authorize]
    public class TokenController : ControllerBase
    {

        #region Add Configuration JWT 

        private readonly IConfiguration _configuration;
        public TokenController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        #endregion

        #region Get
        // GET: api/<TokenController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        #endregion

        #region Get by Id
        // GET api/<TokenController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        #endregion

        #region Post

        // POST api/<TokenController>
        [HttpPost]
        public IActionResult Post()
        {
            var claims = new List<Claim> 
            {
                new Claim("firstname","nazgol"),
                new Claim("lastname","nasiri")
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenConfig:key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JsonWebTokenConfig:issuer"],
                audience: _configuration["JsonWebTokenConfig:audience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JsonWebTokenConfig:expireTime"])),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials);

            var jsonWebToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jsonWebToken);
        }
        #endregion

        #region Put

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
        #endregion

        #region Delete
        // DELETE api/<TokenController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

    }
}
