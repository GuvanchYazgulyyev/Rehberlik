using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.AbouPage
{
    public class OurTeamMembers:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.Teams.OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
