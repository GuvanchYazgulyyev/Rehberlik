using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ManiPage
{
    public class ArticlesMain:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.Blogs.Where(x => x.IsDelate == false).OrderByDescending(k => k.Id).Take(2).ToList();
            return View(result);
        }
    }
}
