using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ManiPage
{
    public class CustomerRaitingMain:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.CustomerComments.OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
