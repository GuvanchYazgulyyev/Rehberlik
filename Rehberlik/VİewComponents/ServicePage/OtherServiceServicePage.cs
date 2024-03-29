﻿using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.VİewComponents.ServicePage
{
    public class OtherServiceServicePage:ViewComponent
    {
        Context dr = new Context();
        public IViewComponentResult Invoke()
        {
            var result = dr.OurServices.Where(x => x.IsDelate == false).OrderByDescending(k => k.Id).ToList();
            return View(result);
        }
    }
}
