using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ManiPage
{
    public class OurServiceMain:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.OurServices.Where(x => x.IsDelate == false).OrderByDescending(k => k.Id).Take(6).ToList();
            return View(result);
        }
    }
}
