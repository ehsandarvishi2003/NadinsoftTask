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
            #region JWT Token
            var claims = new List<Claim> //ایجاد یک claim با اطلاعات
            {
                new Claim("Firstname","Ehsan"),
                new Claim("Lastname","darvishi")
            };

            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenConfig:Key"]));
            var credentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);//بیاد چه چیزی رو با چه الگوریتمی هش بکنه

            var token = new JwtSecurityToken(
                issuer: _configuration["JsonWebTokenConfig:issur"],
                audience: _configuration["JsonWebTokenConfig:audience"],
                expires: DateTime.Now.AddMinutes(int.Parse(_configuration["JsonWebTokenConfig:expireTime"])),
                notBefore: DateTime.Now,
                claims: claims,
                signingCredentials: credentials
                );

            var jsoneWebToken = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(jsoneWebToken);
            #endregion
        }

        #endregion

        #region Put

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
