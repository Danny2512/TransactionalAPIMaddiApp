using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using TransactionalAPIMaddiApp.Helpers.Token;

namespace TransactionalAPIMaddiApp.Controllers
{
    [EnableCors("PolicyCore")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly ITokenHelper _Token;
        public TokenController(ITokenHelper token)
        {
            _Token = token;
        }
        [HttpGet]
        public IActionResult GetToken()
        {
            return Ok(new { Token = _Token.CreateToken(null,TimeSpan.FromSeconds(3)) });
        }
    }
}
