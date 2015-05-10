using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using edu.mum.mumscrum.DAL;
using edu.mum.mumscrum.Models;

namespace edu.mum.mumscrum.Controllers
{
    public class ReleaseBacklogController : Controller
    {
        private MUMScrumContext db = new MUMScrumContext();

        // GET: ReleaseBacklog
        public ActionResult Index()
        {
            var releaseBacklogs = db.ReleaseBacklogs.Include(r => r.ProductBacklog);
            return View(releaseBacklogs.ToList());
        }

        // GET: ReleaseBacklog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleaseBacklog releaseBacklog = db.ReleaseBacklogs.Find(id);
            if (releaseBacklog == null)
            {
                return HttpNotFound();
            }
            return View(releaseBacklog);
        }

        // GET: ReleaseBacklog/Create
        public ActionResult Create()
        {
            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name");
            return View();
        }

        // POST: ReleaseBacklog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,ProductBacklogID,ScrumMasterID")] ReleaseBacklog releaseBacklog)
        {
            if (ModelState.IsValid)
            {
                releaseBacklog.CreatedBy = 1;
                releaseBacklog.CreatedDate = DateTime.Now;

                db.ReleaseBacklogs.Add(releaseBacklog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", releaseBacklog.ProductBacklogID);
            return View(releaseBacklog);
        }

        // GET: ReleaseBacklog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleaseBacklog releaseBacklog = db.ReleaseBacklogs.Find(id);
            if (releaseBacklog == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", releaseBacklog.ProductBacklogID);
            return View(releaseBacklog);
        }

        // POST: ReleaseBacklog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,ProductBacklogID,ScrumMasterID")] ReleaseBacklog releaseBacklog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(releaseBacklog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", releaseBacklog.ProductBacklogID);
            return View(releaseBacklog);
        }

        // GET: ReleaseBacklog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReleaseBacklog releaseBacklog = db.ReleaseBacklogs.Find(id);
            if (releaseBacklog == null)
            {
                return HttpNotFound();
            }
            return View(releaseBacklog);
        }

        // POST: ReleaseBacklog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReleaseBacklog releaseBacklog = db.ReleaseBacklogs.Find(id);
            db.ReleaseBacklogs.Remove(releaseBacklog);
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
