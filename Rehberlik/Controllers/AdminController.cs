using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rehberlik.Models.SqlDat;
using System.Xml.Linq;

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
            var AdSoyad = User.Identity.Name;
            // Ad
            var ad = dr.Admins.Where(k => k.EMail == AdSoyad && k.IsDelete == false).Select(f => f.NameSurname).FirstOrDefault();
            ViewBag.ad = ad;
            // Hizmetler
            var hizmetler = dr.OurServices.Where(k => k.IsDelate == false).Count();
            ViewBag.hizmetler = hizmetler;

            //Makale
            var makale = dr.Blogs.Where(f => f.IsDelate == false).Count();
            ViewBag.makale = makale;

            //  Takım
            var takim = dr.Teams.Count();
            ViewBag.takim = takim;

            // Gelen Mesaj
            var mesajlar = dr.Contacts.Where(k => k.IsDelete == false).Count();
            ViewBag.mesajlar = mesajlar;
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



        #region About Us
        // About Us
        public async Task<IActionResult> AboutUsList()
        {
            var returnValue = dr.Abouts.ToList();
            return View(returnValue);
        }


        [HttpGet]
        public async Task<IActionResult> AboutUsUpdate(int id)
        {
            var getList = dr.Abouts.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> AboutUsUpdate(About hakkimizdum, IFormFile userPicture)
        {

            var returnDetail = dr.Abouts.Find(hakkimizdum.Id);

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

            if (hakkimizdum.Image != null)
            {
                returnDetail.Image = hakkimizdum.Image;
            }

            returnDetail.Title = hakkimizdum.Title;
            returnDetail.Description = hakkimizdum.Description;
            returnDetail.LoverTitle = hakkimizdum.LoverTitle;
            returnDetail.IsDelete = false;
            dr.SaveChanges();
            return RedirectToAction("AboutUsList");
        }

        #endregion



        #region Contact Information

        // Contact Information
        public async Task<IActionResult> ContacInformtList()
        {
            var returnValue = dr.ContactInformation.ToList();
            return View(returnValue);
        }


        [HttpGet]
        public async Task<IActionResult> ContactInformUpdate(int id)
        {
            var getList = dr.ContactInformation.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> ContactInformUpdate(ContactInformation trContact)
        {

            var returnDetail = dr.ContactInformation.Find(trContact.Id);

            returnDetail.Address = trContact.Address;
            returnDetail.Tel = trContact.Tel;
            returnDetail.Email = trContact.Email;
            returnDetail.Map = trContact.Map;
            dr.SaveChanges();
            return RedirectToAction("ContacInformtList");
        }

        #endregion



        #region Customer Comments
        // List Custom Comments
        public async Task<IActionResult> CustomCommentsList()
        {
            var returnValue = dr.CustomerComments.Where(k => k.IsDalete == false).OrderByDescending(k => k.EntryDate).ToList();
            return View(returnValue);
        }



        [HttpGet]
        public async Task<IActionResult> CustomCommentsListAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CustomCommentsListAdd(CustomerComment trComment, IFormFile userPicture)
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
                        trComment.Image = fileName;
                    }
                }
                trComment.IsDalete = false;
                trComment.EntryDate = DateTime.Now;
                dr.CustomerComments.Add(trComment);
                dr.SaveChanges();
                return RedirectToAction("CustomCommentsList");
            }
            return View();

        }
        // Delete

        public async Task<IActionResult> DeleteCustomCommentsList(int id)
        {
            var findID = dr.CustomerComments.Find(id);
            findID.IsDalete = true;
            dr.SaveChanges();
            return RedirectToAction("CustomCommentsList");
        }

        [HttpGet]
        public async Task<IActionResult> CustomCommentsListUpdate(int id)
        {
            var getList = dr.CustomerComments.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> CustomCommentsListUpdate(CustomerComment trBlog, IFormFile userPicture)
        {

            var returnDetail = dr.CustomerComments.Find(trBlog.Id);
            // Resim Kaydetme----------------------------------------------
            if (userPicture.Length > 0 || userPicture != null)
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
            dr.CustomerComments = dr.CustomerComments;
            returnDetail.Branch = trBlog.Branch;
            returnDetail.Description = trBlog.Description;
            returnDetail.NameSurname = trBlog.NameSurname;

            returnDetail.IsDalete = false;
            dr.SaveChanges();
            return RedirectToAction("CustomCommentsList");
        }

        #endregion




        #region InComing Message
        // List Gelen Mesaj

        public async Task<IActionResult> IncomingCustomerMessage()
        {
            var result = dr.Contacts.ToList();
            return View(result);
        }

        // DElete Message
        public async Task<IActionResult> DeleteMeesageList(int id)
        {
            var findId = dr.Contacts.Find(id);
            findId.IsDelete = true;
            dr.SaveChanges();
            return RedirectToAction("IncomingCustomerMessage");
        }

        #endregion



        #region Hizmetler

        // List Blog
        public async Task<IActionResult> ServiceList()
        {
            var returnValue = dr.OurServices.Where(k => k.IsDelate == false).OrderByDescending(k => k.EntryDate).ToList();
            return View(returnValue);
        }


        // All Blog Add
        [HttpGet]
        public async Task<IActionResult> ServiceAdd()
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
        public async Task<IActionResult> ServiceAdd(OurService trBlog, IFormFile userPicture, IFormFile ab)
        {
            //if (ModelState.IsValid)
            //{
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
                    trBlog.ImageUrl = fileName;
                }
            }
            // Icon
            if (ab.Length > 0)
            {
                // Resim Yolunu buluyor
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ab.FileName);
                // Veri tabanı yolunu buluyor
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Picture/", fileName);
                // Kaydetmek için
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ab.CopyToAsync(stream);
                    trBlog.Icon = fileName;
                }
            }

            trBlog.IsDelate = false;
            trBlog.EntryDate = DateTime.Now;
            dr.OurServices.Add(trBlog);
            dr.SaveChanges();
            return RedirectToAction("ServiceList");
            // }
            // return View();

        }
        // Delete

        public async Task<IActionResult> DeleteService(int id)
        {
            var findID = dr.Blogs.Find(id);
            findID.IsDelate = true;
            dr.SaveChanges();
            return RedirectToAction("ServiceList");
        }

        [HttpGet]
        public async Task<IActionResult> ServiceUpdate(int id)
        {
            var getList = dr.Blogs.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> ServiceUpdate(OurService trBlog, IFormFile userPicture, IFormFile ab)
        {

            var returnDetail = dr.OurServices.Find(trBlog.Id);
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
                    returnDetail.ImageUrl = fileName;
                }
            }

            if (trBlog.ImageUrl != null)
            {
                returnDetail.ImageUrl = trBlog.ImageUrl;
            }
            // Icon
            if (ab.Length > 0)
            {
                // Resim Yolunu buluyor
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ab.FileName);
                // Veri tabanı yolunu buluyor
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Picture/", fileName);
                // Kaydetmek için
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    ab.CopyToAsync(stream);
                    returnDetail.Icon = fileName;
                }
            }

            if (trBlog.Icon != null)
            {
                returnDetail.Icon = trBlog.Icon;
            }

            returnDetail.Title = trBlog.Title;
            returnDetail.Description = trBlog.Description;

            returnDetail.IsDelate = false;
            dr.SaveChanges();
            return RedirectToAction("ServiceList");
        }
        #endregion



        #region Why Us 
        // List Why Us
        public async Task<IActionResult> WhyUsList()
        {
            var returnValue = dr.ThreeSteps.Where(k => k.IsDelate == false).OrderByDescending(k => k.EntryDate).ToList();
            return View(returnValue);
        }


        // Delete

        public async Task<IActionResult> DeleteWhyUs(int id)
        {
            var findID = dr.ThreeSteps.Find(id);
            findID.IsDelate = true;
            dr.SaveChanges();
            return RedirectToAction("WhyUsList");
        }

        [HttpGet]
        public async Task<IActionResult> WhyUsUpdate(int id)
        {
            var getList = dr.ThreeSteps.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> WhyUsUpdate(ThreeStepProgress biz, IFormFile userPicture)
        {

            var returnDetail = dr.ThreeSteps.Find(biz.Id);
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

            if (biz.Image != null)
            {
                returnDetail.Image = biz.Image;
            }

            returnDetail.Title = biz.Title;
            returnDetail.Description = biz.Description;

            returnDetail.IsDelate = false;
            dr.SaveChanges();
            return RedirectToAction("WhyUsList");
        }

        #endregion






        #region Admin Information

        public async Task<IActionResult> AdmistrationList()
        {
            var inform = dr.Admins.Where(k => k.IsDelete == false).ToList();
            return View(inform);
        }


        // All City Add
        [HttpGet]
        public async Task<IActionResult> AdmistrationAdd()
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
        public async Task<IActionResult> AdmistrationAdd(Admin allCity)
        {
            if (ModelState.IsValid)
            {
                allCity.IsDelete = false;
                allCity.EntryDate = DateTime.Now;
                dr.Admins.Add(allCity);
                dr.SaveChanges();
                return RedirectToAction("AdmistrationList");
            }
            return View();

        }
        // Delete

        public async Task<IActionResult> AdmistrationDelete(int id)
        {
            var findID = dr.Admins.Find(id);
            findID.IsDelete = true;
            dr.SaveChanges();
            return RedirectToAction("AdmistrationList");
        }

        [HttpGet]
        public async Task<IActionResult> AdmistrationUpdate(int id)
        {
            var getList = dr.Admins.Find(id);
            return View(getList);
        }

        // Get Update
        [HttpPost]
        public async Task<IActionResult> AdmistrationUpdate(Admin trAllCity)
        {
            if (ModelState.IsValid)
            {
                var returnDetail = dr.Admins.Find(trAllCity.Id);
                returnDetail.NameSurname = trAllCity.NameSurname;
                returnDetail.EMail = trAllCity.EMail;
                returnDetail.Tel = trAllCity.Tel;
                returnDetail.Password = trAllCity.Password;
                returnDetail.IsDelete = false;
                dr.SaveChanges();
                return RedirectToAction("AdmistrationList");
            }
            return View();

        }


        #endregion




        #region Çıkış Yap
        ///----------------------------------------------------------------- Çıkış (Log Out)
        ///

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Main");
        }


        #endregion

    }
}
