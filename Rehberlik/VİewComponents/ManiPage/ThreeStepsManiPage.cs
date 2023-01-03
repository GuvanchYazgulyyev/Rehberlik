using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ManiPage
{
    public class ThreeStepsManiPage:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.ThreeSteps.Where(x => x.IsDelate == false).OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
