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
    public class UserStoriesController : Controller
    {
        private MUMScrumContext db = new MUMScrumContext();

        // GET: UserStories
        public ActionResult Index()
        {
            var userStories = db.UserStories.Include(u => u.ProductBacklog).Include(u => u.ReleaseBacklog).Include(u => u.Sprint);
            return View(userStories.ToList());
        }

        // GET: UserStories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStory userStory = db.UserStories.Find(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            return View(userStory);
        }

        // GET: UserStories/Create
        public ActionResult Create()
        {
            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name");
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name");
            ViewBag.SprintID = new SelectList(db.Sprints, "ID", "Name");
            return View();
        }

        // POST: UserStories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,DevelopmentStatus,TestStatus,SprintID,ReleaseBacklogID,ProductBacklogID,DeveloperID,TesterID,DeveloperEstimateInHours,TesterEstimateInHours,DeveloperHoursCompleted,TesterHoursCompleted")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                userStory.CreatedBy = 1;
                userStory.CreatedDate = DateTime.Now;
                userStory.DevelopmentStatus = USDevelopmentStatus.New;
                userStory.TestStatus = USTestStatus.WaitingDevelopment;

                db.UserStories.Add(userStory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", userStory.ProductBacklogID);
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name", userStory.ReleaseBacklogID);
            ViewBag.SprintID = new SelectList(db.Sprints, "ID", "Name", userStory.SprintID);
            return View(userStory);
        }

        // GET: UserStories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStory userStory = db.UserStories.Find(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", userStory.ProductBacklogID);
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name", userStory.ReleaseBacklogID);
            ViewBag.SprintID = new SelectList(db.Sprints, "ID", "Name", userStory.SprintID);
            return View(userStory);
        }

        // POST: UserStories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,DevelopmentStatus,TestStatus,SprintID,ReleaseBacklogID,ProductBacklogID,DeveloperID,TesterID,DeveloperEstimateInHours,TesterEstimateInHours,DeveloperHoursCompleted,TesterHoursCompleted")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userStory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", userStory.ProductBacklogID);
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name", userStory.ReleaseBacklogID);
            ViewBag.SprintID = new SelectList(db.Sprints, "ID", "Name", userStory.SprintID);
            return View(userStory);
        }

        // GET: UserStories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserStory userStory = db.UserStories.Find(id);
            if (userStory == null)
            {
                return HttpNotFound();
            }
            return View(userStory);
        }

        // POST: UserStories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserStory userStory = db.UserStories.Find(id);
            db.UserStories.Remove(userStory);
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
