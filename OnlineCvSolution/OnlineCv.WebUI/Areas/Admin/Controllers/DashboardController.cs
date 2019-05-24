using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineCv.WebUI.Models;
using OnlineCv.WebUI.Models.Entity;
using OnlineCv.WebUI.Models.ViewModel;

namespace OnlineCv.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        OnlineCvDbContext db = new OnlineCvDbContext();
        // GET: Admin/Dashboard
        [ChildActionOnly]
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var cat = db.Category.ToList();
            return View(cat);
        }

        [ChildActionOnly]
        public ActionResult Skill()
        {
            var skil = db.Skill.ToList();
            return View(skil);
        }

        public ActionResult Resume()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Resume(Models.Entity.Admin postAdmin)
        {
            Models.Entity.Admin admin = db.Admin.FirstOrDefault();
            var person = db.PersonDetails.FirstOrDefault();
            if (ModelState.IsValid)
            {
                if (postAdmin.Email == admin.Email && postAdmin.Password == admin.Password)
                {
                    return View("Index");
                }
            }
            return RedirectToAction("Resume");
        }
        
        public ActionResult EditCV()
        {
            
            return View();
        }
        [ChildActionOnly]
        public ActionResult addInfo()
        {
            var person = db.PersonDetails.FirstOrDefault();
            return View(person);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addInfos(PersonDetails postPerson, HttpPostedFileBase mediaUrl)
        {
            if (mediaUrl == null)
                ModelState.AddModelError("mediaUrl", "Şəkil seçilməyib!");
            else
            {
                if (!mediaUrl.CheckImageType())
                    ModelState.AddModelError("mediaUrl", "Şəkil uyğun deyil!");
                if (!mediaUrl.CheckImageSize(5))
                    ModelState.AddModelError("mediaUrl", "Şəklin ölçüsü uyğun deyil!");
            }
            
            var personExists = db.PersonDetails.Any();
            var person = db.PersonDetails.FirstOrDefault();
            if (personExists)
            {
                if (ModelState.IsValid)
                {
                    person.Age = postPerson.Age;
                    person.CareerLevel = postPerson.CareerLevel;
                    person.Degree = postPerson.Degree;
                    person.Email = postPerson.Email;
                    person.Experience = postPerson.Experience;
                    person.Fax = postPerson.Fax;
                    person.Location = postPerson.Location;
                    person.MediaUrl = postPerson.MediaUrl;
                    person.Name = postPerson.Name;
                    person.Phone = postPerson.Phone;
                    person.Website = postPerson.Website;
                    string image = mediaUrl.SaveImage(Server.MapPath("~/Template/media"));
                    person.MediaUrl = image;
                    db.SaveChanges();
                    return PartialView("EditCV");
                }
            }
            if (ModelState.IsValid)
            {
                postPerson.MediaUrl = mediaUrl.SaveImage(Server.MapPath("~/Template/media"));
                db.PersonDetails.Add(postPerson);
                db.SaveChanges();
            }
            return View("EditCV");
        }
        [ChildActionOnly]
        public ActionResult AddExperience()
        {
            var ex = db.ProfessionalExperiences.ToList();
            return View(ex);
        }

        [ChildActionOnly]
        public ActionResult Bio()
        {
            var ex = db.Bio.ToList();
            return View(ex);
        }

        [ChildActionOnly]
        public ActionResult SocialProfile()
        {
            var ex = db.Bio.ToList();
            return View(ex);
        }

        [HttpPost]
        public ActionResult AddExperiences(ProfessionalExperience experience)
        {
            var person = db.PersonDetails.FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.ProfessionalExperiences.Add(experience);
                db.SaveChanges();
            }
            return View("EditCV");
        }

        [ChildActionOnly]
        public ActionResult AddBackground()
        {
            var ex = db.AcademicBackground.ToList();
            return View(ex);
        }

        [HttpPost]
        public ActionResult AddBackgrounds(AcademicBackground academic)
        {
            var person = db.PersonDetails.FirstOrDefault();
            if (ModelState.IsValid)
            {
                db.AcademicBackground.Add(academic);
                db.SaveChanges();
            }
            return View("EditCV");
        }

        [HttpPost]
        public ActionResult addSkill(Bio info)
        {
            return View();
        }
    }
}