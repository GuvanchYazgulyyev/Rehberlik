using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.Controllers
{
    public class MainController : Controller
    {
        Context dr = new Context();
        // Slider Done
        public IActionResult Index()
        {
            var sliderList = dr.Sliders.Where(k => k.IsDelate == false).OrderByDescending(k => k.EntryDate).ToList();
            return View(sliderList);
        }

        // Hakkimizda  Done
        public IActionResult Hakkimizda()
        {
            var verigetir = dr.Abouts.Where(k => k.IsDelete == false).OrderByDescending(f => f.Entrydate).ToList();
           // var verigetir = dr.Abouts.Where(k => k.IsDelete == false).OrderByDescending(f => f.Entrydate).FirstOrDefault();
            return View(verigetir);
        }

        // Hizmetler Done
        public IActionResult Hizmetler()
        {
            var verigetir = dr.OurServices.Where(k => k.IsDelate == false).ToList();
            return View(verigetir);
        }

        // Yazılar (Bloklar) done
        public IActionResult Bloglar()
        {
            var verigetir = dr.Blogs.Where(k => k.IsDelate == false).ToList();
            return View(verigetir);
        }

        // Takım  Done
        public IActionResult Takim()
        {
            var verigetir = dr.Teams.ToList();
            return View(verigetir);
        }

        // İletişim Mesajları
       [HttpGet]
        public IActionResult MGonderMesaj()
        {
            //var verigetir = dr.Contacts.FirstOrDefault();
            //return View(verigetir);
            return View();
        }

      //  [HttpPost]
        //public IActionResult MGonderMesaj()
        //{
        //    //var verigetir = dr.Contacts.FirstOrDefault();
        //    //return View(verigetir);
        //    return View();
        //}
        
        // Projeler All Done
        public IActionResult Projeler()
        {
            var veriListele = dr.OurProjects.Where(k => k.IsDelate == false).OrderByDescending(g => g.EntryDate).ToList();
            return View(veriListele);
        }
    }
}
