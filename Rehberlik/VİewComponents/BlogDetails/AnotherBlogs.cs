using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.BlogDetails
{
    public class AnotherBlogs:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.Blogs.Where(g=>g.IsDelate==false).OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
