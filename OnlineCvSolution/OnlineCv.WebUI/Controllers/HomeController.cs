using OnlineCv.WebUI.Models;
using OnlineCv.WebUI.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OnlineCv.WebUI.Controllers
{
    public class HomeController : Controller
    {
        OnlineCvDbContext db = new OnlineCvDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BlogSingle()
        {
            return View();
        }

        

        public ActionResult ResumePage()
        {
            return View();
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
            return View();
        }

        [ChildActionOnly]
        public ActionResult SideBar()
        {
            PersonDetails person = db.PersonDetails.FirstOrDefault();
            return View(person);
        }

        [ChildActionOnly]
        public ActionResult ProExperience()
        {
            IEnumerable<ProfessionalExperience> pro = db.ProfessionalExperiences.ToList();
            return View(pro);
        }

        [ChildActionOnly]
        public ActionResult AcBack()
        {
            IEnumerable<AcademicBackground> ac = db.AcademicBackground.ToList();
            return View(ac);
        }

       

        [HttpPost]
        public ActionResult SendMessage(EmailModel emailModel)
        {
            if (!ModelState.IsValid)
            {
                throw new NullReferenceException();
            }
            else
            {
                db.EmailModel.Add(emailModel);
                db.SaveChanges();
                emailModel.ToMails.SendMail(emailModel.Subject, emailModel.Body);
            }
            return RedirectToAction("Index");
        }

       
    }
}