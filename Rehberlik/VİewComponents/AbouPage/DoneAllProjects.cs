using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.AbouPage
{
    public class DoneAllProjects:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.DoneAllProjects.Where(x => x.IsDelate == false).OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
