using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ManiPage
{
    public class OurProjectMain:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.OurProjects.Where(x => x.IsDelate == false).OrderByDescending(k => k.Id).Take(4).ToList();
            return View(result);
        }
    }
}
