using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehberlik.Models.SqlDat;

namespace Rehberlik.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        Context dr = new Context();
        private readonly ILogger<HomeController> _logger;
        public AdminController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }


        #region Blogs

        // List Blog
        public async Task<IActionResult> BlogList()
        {
            var returnValue = dr.Blogs.Where(k => k.IsDelate == false).OrderByDescending(k => k.EntryDate).ToList();
            return View(returnValue);
        }


        // All Blog Add
        [HttpGet]
        public async Task<IActionResult> BlogAdd()
        {
            var mail = User.Identity.Name;

            var soyad = dr.Admins.Where(k => k.EMail == mail).Select(f => f.NameSurname).FirstOrDefault();
            ViewBag.soyad = soyad;
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
        public async Task<IActionResult> BlogAdd(Blog trBlog, IFormFile userPicture)
        {
            if (ModelState.IsValid)
            {
                // Resim Kaydetme----------------------------------------------
                if (userPicture.Length > 0)
                {
                    // Resim Yolunu buluyor
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);
                    // Veri tabanı yolunu buluyor
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Picture/", fileName);
                    // Kaydetmek için
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        userPicture.CopyToAsync(stream);
                        trBlog.Image = fileName;
                    }
                }
                trBlog.IsDelate = false;
                trBlog.EntryDate = DateTime.Now;
                dr.Blogs.Add(trBlog);
                dr.SaveChanges();
                return RedirectToAction("BlogList");
            }
            return View();

        }
        // Delete

        public async Task<IActionResult> DeleteBlog(int id)
        {
            var findID = dr.Blogs.Find(id);
            findID.IsDelate = true;
            dr.SaveChanges();
            return RedirectToAction("BlogList");
        }

        [HttpGet]
        public async Task<IActionResult> AllBlogUpdate(int id)
        {
            var getList = dr.Blogs.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> AllBlogUpdate(Blog trBlog, IFormFile userPicture)
        {

            var returnDetail = dr.Blogs.Find(trBlog.Id);
            // Resim Kaydetme----------------------------------------------
            if (userPicture.Length > 0)
            {
                // Resim Yolunu buluyor
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);
                // Veri tabanı yolunu buluyor
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Picture/", fileName);
                // Kaydetmek için
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    userPicture.CopyToAsync(stream);
                    returnDetail.Image = fileName;
                }
            }

            if (trBlog.Image != null)
            {
                returnDetail.Image = trBlog.Image;
            }

            returnDetail.Title = trBlog.Title;
            returnDetail.Description = trBlog.Description;

            returnDetail.IsDelate = false;
            dr.SaveChanges();
            return RedirectToAction("BlogList");
        }
        #endregion
    }
}
