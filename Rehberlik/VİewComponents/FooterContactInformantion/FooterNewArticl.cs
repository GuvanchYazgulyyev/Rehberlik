using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.FooterContactInformantion
{
    public class FooterNewArticl:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.Blogs.OrderByDescending(k => k.Id).Take(2).ToList();
            return View(result);
        }
    }
}
