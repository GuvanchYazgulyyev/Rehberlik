using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.Controllers
{
    [AllowAnonymous]
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
            return View(verigetir);
        }

        // Hizmetler Done
        public IActionResult Hizmetler()
        {
            var verigetir = dr.OurServices.Where(k => k.IsDelate == false).ToList();
            return View(verigetir);
        }
        // Hizmet Detay
        public async Task<IActionResult> HizmetDetay(string id)
        {
            var detay = dr.OurServices.Where(k => k.ServiceNo == id && k.IsDelate == false).SingleOrDefault();
            return View(detay);
        }

        // Yazılar (Bloklar) done
        public IActionResult Bloglar()
        {
            var verigetir = dr.Blogs.Where(k => k.IsDelate == false).ToList();
            return View(verigetir);
        }

        // Blog Detay
        public IActionResult BlogDetay(string id)
        {
            var detay = dr.Blogs.Where(k => k.ItemNo == id).FirstOrDefault();
            return View(detay);
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
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H", "V", "Q", "W", "Z" };
            int k1, k2, k3;
            k1 = rnd.Next(0, karakterler.Length);
            k2 = rnd.Next(0, karakterler.Length);
            k3 = rnd.Next(0, karakterler.Length);
            int s1, s2, s3;
            s1 = rnd.Next(100, 1000);
            s2 = rnd.Next(10, 99);
            s3 = rnd.Next(10, 99);
            string kod = s1.ToString() + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.takipkod = kod;
            return View();
        }

        [HttpPost]
        public IActionResult MGonderMesaj(Contact contact)
        {
            contact.EntryDate = DateTime.Now;
            contact.IsDelete = false;
            dr.Contacts.Add(contact);
            dr.SaveChanges();
            return RedirectToAction("Index", "Main");
        }

        // Projeler All Done
        public IActionResult Projeler()
        {
            var veriListele = dr.OurProjects.Where(k => k.IsDelate == false).OrderByDescending(g => g.EntryDate).ToList();
            return View(veriListele);
        }
    }
}
