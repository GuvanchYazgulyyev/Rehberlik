using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ContactPage
{
    public class ConInformation:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.ContactInformation.OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
