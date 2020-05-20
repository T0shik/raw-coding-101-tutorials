using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "";
        }

        [HttpGet]
        public IActionResult SignIn([FromServices] ClaimsService claimsService)
        {
            return View(claimsService.Claims());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(IEnumerable<string> claims)
        {
            var identity = new ClaimsIdentity(claims.Select(x => new Claim(Constants.WebAppClaimType, x)), "Identity");
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index");
        }

        [Authorize]
        public string Secret()
        {
            return "";
        }
    }
}
