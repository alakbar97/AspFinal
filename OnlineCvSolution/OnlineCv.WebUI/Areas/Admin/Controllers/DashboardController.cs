﻿using OnlineCv.WebUI.AppCode.Constant;
using OnlineCv.WebUI.Models;
using OnlineCv.WebUI.Models.Entity;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineCv.WebUI.Areas.Admin.Controllers
{
    [OnlineAuthorizationAttribute]
    [OnlineExceptionFilter]
    public class DashboardController : MainController
    {
        OnlineCvDbContext db = new OnlineCvDbContext();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Category()
        {
            var cat = db.Category.Where(w=>w.DeletedDate==null).ToList();
            return View(cat);
        }

        [ChildActionOnly]
        public ActionResult Skill()
        {
            var skil = db.Skill.Where(w => w.DeletedDate == null).ToList();
            return View(skil);
        }
        [AllowAnonymous]
        public ActionResult Resume()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Resume(Models.Entity.Admin postAdmin)
        {
            Models.Entity.Admin admin = db.Admin.FirstOrDefault();
            var person = db.PersonDetails.FirstOrDefault();
            Session[SessionKey.User] = admin;
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
                    person.ModifiedDate = DateTime.UtcNow;
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
            var ex = db.ProfessionalExperiences.Where(w => w.DeletedDate == null).ToList();
            return View(ex);
        }

        [ChildActionOnly]
        public ActionResult Bio()
        {
            var bio = db.Bio.OrderByDescending(w => w.IsTag == false).Where(w=>w.DeletedDate==null).ToList();
            return View(bio);
        }

        [ChildActionOnly]
        public ActionResult SocialProfile()
        {
            var ex = db.SocialProfiles.Where(w => w.DeletedDate == null).FirstOrDefault();
            return View(ex);
        }

        [HttpPost]
        public ActionResult Socials(SocialProfiles profiles)
        {
            if (db.SocialProfiles.Any())
            {
                var s = db.SocialProfiles.Where(w => w.DeletedDate == null).FirstOrDefault();
                s.Facebook = profiles.Facebook;
                s.Google = profiles.Google;
                s.Linked = profiles.Linked;
                s.Skype = profiles.Skype;
                s.Twitter = profiles.Twitter;
                db.SaveChanges();
                return View("EditCV");
            }
            db.SocialProfiles.Add(profiles);
            db.SaveChanges();
            return View("EditCV");
        }

        [HttpPost]
        public ActionResult AddExperiences(ProfessionalExperience experience, HttpPostedFileBase mediaUrl)
        {
            if (ModelState.IsValid)
            {
                string image = mediaUrl.SaveImage(Server.MapPath("~/Template/media"));
                experience.MediaUrl = image;
                experience.CreationDate = DateTime.UtcNow;
                db.ProfessionalExperiences.Add(experience);
                db.SaveChanges();
            }
            return View("EditCV");
        }

        [ChildActionOnly]
        public ActionResult AddBackground()
        {
            var ex = db.AcademicBackground.Where(w => w.DeletedDate == null).ToList();
            return View(ex);
        }

        [HttpPost]
        public ActionResult AddBackgrounds(AcademicBackground academic, HttpPostedFileBase mediaUrl)
        {
            var person = db.PersonDetails.FirstOrDefault();
            if (ModelState.IsValid)
            {
                string image = mediaUrl.SaveImage(Server.MapPath("~/Template/media"));
                academic.mediaUrl = image;
                academic.CreationDate = DateTime.UtcNow;
                db.AcademicBackground.Add(academic);
                db.SaveChanges();
            }
            return View("EditCV");
        }

        [HttpPost]
        public ActionResult addSkill(Bio info)
        {
            //ModelState.Remove(info.Category);
            if (ModelState.IsValid)
            {
                info.CreationDate = DateTime.UtcNow;
                db.Bio.Add(info);
                db.SaveChanges();
            }

            return View("EditCV");
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AcademicBackground academic = db.AcademicBackground.Find(id);
            if (academic == null)
            {
                if (Request.IsAjaxRequest())
                    return Content("");
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialBack.cshtml", academic);
            }
            return View(academic);
        }

        public ActionResult getEx(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProfessionalExperience exx = db.ProfessionalExperiences.Find(id);
            if (exx == null)
            {
                if (Request.IsAjaxRequest())
                    return Content("");
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialExperience.cshtml", exx);
            }
            return View(exx);
        }

        [HttpPost]
        public ActionResult saveBack(int? id, AcademicBackground academic)
        {
            AcademicBackground entity = db.AcademicBackground.Find(id);
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                entity.Degree = academic.Degree;
                entity.Details = academic.Details;
                entity.ModifiedDate = DateTime.UtcNow;
                entity.Qualification = academic.Qualification;
                entity.UniversityName = academic.UniversityName;
                entity.YearofObtention = academic.YearofObtention;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                var acs = db.AcademicBackground.ToList();
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialBackList.cshtml", acs);
            }
            return View();
        }


        [HttpPost]
        public ActionResult saveExp(int? id, ProfessionalExperience exx)
        {
            ProfessionalExperience entity = db.ProfessionalExperiences.Find(id);
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)
            {
                entity.CompanyName = exx.CompanyName;
                entity.Details = exx.Details;
                entity.Duration = exx.Duration;
                entity.JobTitle = exx.JobTitle;
                entity.Location = exx.Location;
                entity.CreationDate = DateTime.UtcNow;

                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
                var exs = db.ProfessionalExperiences.ToList();
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialExperienceList.cshtml", exs);
            }
            return View();
        }

        [HttpPost]
        public ActionResult addSk(Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.CreationDate = DateTime.UtcNow;
                db.Skill.Add(skill);
                db.SaveChanges();
                return View("EditCV");
            }
            if (skill == null)
                throw new NullReferenceException();
            return View();
        }

        [HttpPost]
        public ActionResult addCate(Category cat)
        {
            if (ModelState.IsValid)
            {
                cat.CreationDate = DateTime.UtcNow;
                db.Category.Add(cat);
                db.SaveChanges();
                return View("EditCV");
            }
            if (cat == null)
                throw new NullReferenceException();
            return View();
        }

        public ActionResult getBio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bio exx = db.Bio.Find(id);
            if (exx == null)
            {
                if (Request.IsAjaxRequest())
                    return Content("");
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialBio.cshtml", exx);
            }
            return View(exx);
        }


        [HttpPost]
        public ActionResult saveBio(int? id, Bio exx)
        {
            Bio entity = db.Bio.Find(id);
            if (entity == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    entity.Description = exx.Description;
                    entity.SkillLevel = exx.SkillLevel;
                    entity.SkillDescription = exx.SkillDescription;
                    entity.IsBar = exx.IsBar;
                    entity.IsTag = exx.IsTag;
                    entity.ModifiedDate = DateTime.UtcNow;
                    db.Entry(entity).State = EntityState.Modified;
                    db.SaveChanges();
                    var bios = db.Bio.Where(w => w.DeletedDate == null).ToList();
                    return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialBioList.cshtml", bios);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult deleteBio(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request.IsAjaxRequest())
            {
                var bio = db.Bio.Find(id);
                bio.DeletedDate = DateTime.UtcNow;
                db.Entry(bio).State = EntityState.Modified;
                db.SaveChanges();
                var bios = db.Bio.Where(w => w.DeletedDate == null).ToList();
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialBioList.cshtml", bios);
            }
            return View();
        }

        [HttpPost]
        public ActionResult deleteBack(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request.IsAjaxRequest())
            {
                var back = db.AcademicBackground.Find(id);
                back.DeletedDate = DateTime.UtcNow;
                db.Entry(back).State = EntityState.Modified;
                db.SaveChanges();
                var backs = db.AcademicBackground.Where(w => w.DeletedDate == null).ToList();
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialBackList.cshtml", backs);
            }

            return View();
        }
        [HttpPost]
        public ActionResult deleteExp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (Request.IsAjaxRequest())
            {
                var exp = db.ProfessionalExperiences.Find(id);
                exp.DeletedDate = DateTime.UtcNow;
                db.Entry(exp).State = EntityState.Modified;
                db.SaveChanges();
                var exps = db.ProfessionalExperiences.Where(w => w.DeletedDate == null).ToList();
                return PartialView("~/Areas/Admin/Views/Dashboard/partial/_PartialExperienceList.cshtml", exps);
            }

            return View();
        }
    }
}