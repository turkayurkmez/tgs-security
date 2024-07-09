using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthNandAuthZ.Controllers
{
    [Authorize(Roles ="Admin,Editor")]
    public class ForbiddenController : Controller
    {
        //[AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Show()
        {
            return View();
        }
    }
}
