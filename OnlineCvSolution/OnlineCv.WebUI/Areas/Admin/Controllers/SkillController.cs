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
    public class SkillController : Controller
    {
        private OnlineCvDbContext db = new OnlineCvDbContext();

        // GET: Admin/Skill
        public ActionResult Index()
        {
            var skill = db.Skill.Where(w => w.DeletedDate == null).ToList();
            return View(skill);
        }

        // GET: Admin/Skill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // GET: Admin/Skill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Skill/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreationDate,CreatedId,ModifiedDate,ModifiedId,DeletedDate,DeletedId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.CreationDate = DateTime.UtcNow;
                db.Skill.Add(skill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(skill);
        }

        // GET: Admin/Skill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Admin/Skill/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreationDate,CreatedId,ModifiedDate,ModifiedId,DeletedDate,DeletedId")] Skill skill)
        {
            if (ModelState.IsValid)
            {
                skill.ModifiedDate = DateTime.UtcNow;
                db.Entry(skill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(skill);
        }

        // GET: Admin/Skill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Skill skill = db.Skill.Find(id);
            if (skill == null)
            {
                return HttpNotFound();
            }
            return View(skill);
        }

        // POST: Admin/Skill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Skill skill = db.Skill.Find(id);
            skill.DeletedDate = DateTime.UtcNow;
            db.Entry(skill).State = EntityState.Modified;
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
    }
}
