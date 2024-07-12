using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SecurityInREST.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityInREST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Validate(loginModel.UserName, loginModel.Password);
                if (user != null)
                {
                    //Eğer; login başarılıysa istemciye JWT üret ve gönder
                  SymmetricSecurityKey key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes("burası-token-onayi-icin-kullanilacak"));
                    SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    //Pay-Load
                    //
                    var claims = new Claim[]
                    {
                        new Claim("takim","Yalovaspor"),
                        new Claim(JwtRegisteredClaimNames.Gender,"Kadın"),
                        new Claim("role",user.Role)

                    };

                    var token = new JwtSecurityToken(
                        issuer: "server.tgs",
                        audience: "client.tgs",
                        claims: claims,
                        notBefore: DateTime.UtcNow,
                        expires: DateTime.Now.AddDays(10),
                        signingCredentials: signingCredentials

                        );

                    return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
                }
                ModelState.AddModelError("loginFailed", "Giriş başarısız");

            }
            return BadRequest(ModelState);
        }
    }
}
