using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NadinsoftTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1", Deprecated = false)]
    [Authorize]
    public class TokenController : ControllerBase
    {
        #region Ctor
        private readonly IConfiguration configuration;
        public TokenController(IConfiguration configuration)
        {
            this.configuration = configuration;
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

        #region Get by id
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
            var claims = new List<Claim> {
            new Claim("firstname","nazgol"),
            new Claim("lastname","nasiri")
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JsonWebTokenConfig:key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["JsonWebTokenConfig:issuer"],
                audience: configuration["JsonWebTokenConfig:audience"],
                expires: DateTime.Now.AddMinutes(int.Parse(configuration["JsonWebTokenConfig:expireTime"])),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials);
            var jsonWebToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jsonWebToken);
        }
        #endregion

        #region Put Update
        // PUT api/<TokenController>/5
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
