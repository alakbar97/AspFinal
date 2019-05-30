using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineCv.WebUI.Models;
using OnlineCv.WebUI.Models.Entity;

namespace OnlineCv.WebUI.Areas.Admin.Controllers
{
    [OnlineAuthorizationAttribute]
    [OnlineExceptionFilter]
    public class EmailModelController : MainController
    {
        private OnlineCvDbContext db = new OnlineCvDbContext();

        // GET: Admin/EmailModel
        public ActionResult Index()
        {
            return View(db.EmailModel.Where(w => w.DeletedDate == null).ToList());
        }

        // GET: Admin/EmailModel/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailModel emailModel = db.EmailModel.Find(id);
            if (emailModel == null)
            {
                if (Request.IsAjaxRequest())
                    return Content("");
                return HttpNotFound();
            }
            if (Request.IsAjaxRequest())
            {
                emailModel.IsRead = true;
                db.SaveChanges();
                return PartialView("~/Areas/Admin/Views/EmailModel/Partial/_PartialList.cshtml", emailModel);
            }
            emailModel.IsRead = true;
            db.Entry(emailModel).State = EntityState.Modified;
            db.SaveChanges();
            return View(emailModel);
        }

        // GET: Admin/EmailModel/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailModel emailModel = db.EmailModel.Find(id);
            if (emailModel == null)
            {
                return HttpNotFound();
            }
            return View(emailModel);
        }

        // POST: Admin/EmailModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailModel emailModel = db.EmailModel.Find(id);
            emailModel.DeletedDate = DateTime.UtcNow;
            db.Entry(emailModel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpPost]
        public ActionResult SendMessage(int? id,EmailModel emailModel)
        {
            if (!ModelState.IsValid)
            {
                throw new NullReferenceException();
            }
            else
            {
                EmailModel email = db.EmailModel.Find(id);
                email.Answer = emailModel.Answer;
                db.Entry(email).State = EntityState.Modified;
                db.SaveChanges();
                emailModel.UserEmail.SendUserMail(emailModel.Subject, emailModel.Answer);
            }
            if (Request.IsAjaxRequest())
            {
                var emails = db.EmailModel.ToList();
                return PartialView("~/Areas/Admin/Views/EmailModel/Partial/_Partialtbody.cshtml",emails);
            }
            return View();
        }
    }
}
