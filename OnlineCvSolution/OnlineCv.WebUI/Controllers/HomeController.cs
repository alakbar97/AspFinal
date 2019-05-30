using OnlineCv.WebUI.Models;
using OnlineCv.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace OnlineCv.WebUI.Controllers
{
    [OnlineExceptionFilter]
    public class HomeController : Controller
    {
        OnlineCvDbContext db = new OnlineCvDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.PersonDetails.Where(w => w.DeletedDate == null).FirstOrDefault());
        }

        public ActionResult BlogSingle()
        {
            return View();
        }

        

        public ActionResult ResumePage()
        {
            return View(db.Bio.OrderByDescending(s => s.IsBar == true).Where(w=>w.DeletedDate==null).ToList());
        }

        public ActionResult Portfolio()
        {
            return View();
        }

        public ActionResult Blog()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View(db.PersonDetails.Where(w => w.DeletedDate == null).FirstOrDefault());
        }

        [ChildActionOnly]
        public ActionResult SideBar()
        {
            PersonDetails person = db.PersonDetails.Where(w => w.DeletedDate == null).FirstOrDefault();
            return View(person);
        }

        [ChildActionOnly]
        public ActionResult ProExperience()
        {
            IEnumerable<ProfessionalExperience> pro = db.ProfessionalExperiences.Where(w => w.DeletedDate == null).ToList();
            return View(pro);
        }

        [ChildActionOnly]
        public ActionResult AcBack()
        {
            IEnumerable<AcademicBackground> ac = db.AcademicBackground.Where(w => w.DeletedDate == null).ToList();
            return View(ac);
        }

       [ChildActionOnly]
       public ActionResult Sc()
        {
            return View(db.SocialProfiles.Where(w => w.DeletedDate == null).FirstOrDefault());
        }

        [ChildActionOnly]
        public ActionResult Skills()
        {
            return View(db.Bio.Where(w=>w.IsBar==true).Where(w => w.DeletedDate == null).ToList());
        }

        [HttpPost]
        public ActionResult Contact(EmailModel emailModel)
        {
            if (emailModel.Name==null||emailModel.UserEmail==null||emailModel.Body==null||emailModel.Subject==null)
            {
                TempData["fill"] = "All inputs must be written";
                return RedirectToAction("Contact");
            }
            if (ModelState.IsValid)
            {
                db.EmailModel.Add(emailModel);
                db.SaveChanges();
                emailModel.ToMails.SendMail(emailModel.Subject, emailModel.Body);
                return RedirectToAction("Contact");
            }
            return RedirectToAction("Contact");
        }

       
    }
}