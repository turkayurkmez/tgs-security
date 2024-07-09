using AuthNandAuthZ.DataModels;
using AuthNandAuthZ.Models;
using AuthNandAuthZ.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthNandAuthZ.Controllers
{
    public class KullaniciController : Controller
    {
        private readonly IUserService userService;

        public KullaniciController(IUserService userService)
        {
            this.userService = userService;
        }

        public IActionResult Giris(string? gidilecekAdres)
        {
            UserLoginModel userLoginModel = new UserLoginModel();
            userLoginModel.ReturnUrl = gidilecekAdres;
            return View(userLoginModel);

        }

        [HttpPost]
        public async Task<IActionResult> Giris(UserLoginModel userLoginModel)
        {
            if (ModelState.IsValid)
            {
                var user = userService.ValidateUser(userLoginModel.UserName, userLoginModel.Password);
                if (user != null)
                {
                    var claims = new Claim[]
                    {
                        new Claim("Takim","Yalovaspor"),
                        new Claim(ClaimTypes.Role,user.Role),
                        new Claim(ClaimTypes.Name,user.Name)
                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                   await HttpContext.SignInAsync(claimsPrincipal);

                    if (!string.IsNullOrEmpty(userLoginModel.ReturnUrl) && Url.IsLocalUrl(userLoginModel.ReturnUrl))
                    {
                        return Redirect(userLoginModel.ReturnUrl);
                    }

                    return Redirect("/");

                }
                ModelState.AddModelError("login", "Kullanıcı adı ve şifre eşleşmiyor!");

            }
            return View(userLoginModel);
        }

        public async Task<IActionResult> Cikis()
        {
            await HttpContext.SignOutAsync();
            return Redirect("/");
        }


        public IActionResult erisimEngellendi() => View();

        public IActionResult Kayit() => View();

        [HttpPost]
        public IActionResult Kayit(RealUser realUser)
        {
            if (ModelState.IsValid)
            {
                userService.RegisterUser(realUser);
                return Redirect("/");
            }
            return View();
        }

    }
}
