using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.Controllers
{
    [AllowAnonymous]
    public class OrganizationController : Controller
    {

        Context dr = new Context();

        [HttpGet]
        public async Task<IActionResult> OrganiztionLogin()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> OrganiztionLogin(Admin administration)
        {
            if (ModelState.IsValid)

            {
                var datavalue = dr.Admins.FirstOrDefault(k => k.EMail == administration.EMail && k.Password == administration.Password);
                if (datavalue != null)
                {
                    var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,administration.EMail)
                };
                    var userIdentity = new ClaimsIdentity(claims, "a");
                    ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                    await HttpContext.SignInAsync(principal);
                    return RedirectToAction("BlogList", "Admin");
                }
                else
                {
                    TempData["mesaj"] = "Hatalı giriş!!!";
                    return View();
                }
            }
            TempData["mesaj"] = "Bu Hesap Kayutlı Değil!!!";
            return View();
        }

    }
}
