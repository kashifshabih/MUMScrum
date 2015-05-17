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
using edu.mum.mumscrum.ViewModels;

namespace edu.mum.mumscrum.Controllers
{
    [Authorize(Roles = "ScrumMaster")]
    public class SprintController : Controller
    {
        private MUMScrumContext db = new MUMScrumContext();

        // GET: Sprint
        public ActionResult Index()
        {
            var sprints = db.Sprints.Include(s => s.ReleaseBacklog);
            return View(sprints.ToList());
        }

        // GET: Sprint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // GET: Sprint/Create
        public ActionResult Create()
        {
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name");

            var sprint = new Sprint();
            sprint.UserStories = new List<UserStory>();

            PopulateAssignedUserStories(sprint);

            return View();
        }

        private void PopulateAssignedUserStories(Sprint sprint)
        {
            //var allUserStories = db.UserStories.Where(u => u.ReleaseBacklogID == null || u.ReleaseBacklogID == releaseBacklog.ID);
            var allUserStories = db.UserStories.Where(u => u.SprintID == null || u.SprintID == sprint.ID);

            //var releaseBacklogUserStories = new HashSet<int>(releaseBacklog.UserStories.Select(u => u.ID));
            var sprintUserStories = new HashSet<int>(sprint.UserStories.Select(u => u.ID));

            var viewModel = new List<AssignedUserStoryData>();

            foreach (var userStory in allUserStories)
            {
                viewModel.Add(new AssignedUserStoryData
                {
                    UserStoryID = userStory.ID,
                    Name = userStory.Name,
                    Assigned = sprintUserStories.Contains(userStory.ID)
                });
            }

            ViewBag.UserStories = viewModel;
        }

        // POST: Sprint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,ReleaseBacklogID")] Sprint sprint, string[] selectedUserStories)
        {
            if (selectedUserStories != null)
            {
                //releaseBacklog.UserStories = new List<UserStory>();
                sprint.UserStories = new List<UserStory>();
                
                //foreach (var userStory in selectedUserStories)
                foreach (var userStory in selectedUserStories)
                {
                    var userStoryToAdd = db.UserStories.Find(int.Parse(userStory));
                    //releaseBacklog.UserStories.Add(userStoryToAdd);
                    sprint.UserStories.Add(userStoryToAdd);
                }
            }
            
            if (ModelState.IsValid)
            {
                sprint.CreatedBy = 1;
                sprint.CreatedDate = DateTime.Now;

                db.Sprints.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            PopulateAssignedUserStories(sprint);

            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name", sprint.ReleaseBacklogID);
            return View(sprint);
        }

        // GET: Sprint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            //Sprint sprint = db.Sprints.Find(id);

            Sprint sprint = db.Sprints
                .Include(s => s.UserStories)
                .Where(s => s.ID == id)
                .Single();

            PopulateAssignedUserStories(sprint);
            
            
            if (sprint == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name", sprint.ReleaseBacklogID);
            return View(sprint);
        }

        // POST: Sprint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,ReleaseBacklogID")] Sprint sprint)
        public ActionResult Edit(int? id, string[] selectedUserStories)
        {
            Sprint sprint = db.Sprints
            .Include(s => s.UserStories)
            .Where(s => s.ID == id)
            .Single();

            if (ModelState.IsValid)
            {
                UpdateSprintUserStories(selectedUserStories, sprint);

                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReleaseBacklogID = new SelectList(db.ReleaseBacklogs, "ID", "Name", sprint.ReleaseBacklogID);
            return View(sprint);
        }

        private void UpdateSprintUserStories(string[] selectedUserStories, Sprint sprintToUpdate)
        {
            if (selectedUserStories == null)
            {
                sprintToUpdate.UserStories = new List<UserStory>();
                return;
            }

            var selectedUserStoriesHS = new HashSet<string>(selectedUserStories);
            var sprintUserStories = new HashSet<int>(sprintToUpdate.UserStories.Select(u => u.ID));

            foreach (var userStory in db.UserStories)
            {
                if (selectedUserStoriesHS.Contains(userStory.ID.ToString()))
                {
                    if (!sprintUserStories.Contains(userStory.ID))
                    {
                        sprintToUpdate.UserStories.Add(userStory);
                    }
                }
                else
                {
                    if (sprintUserStories.Contains(userStory.ID))
                    {
                        sprintToUpdate.UserStories.Remove(userStory);
                    }
                }
            }
        }

        // GET: Sprint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // POST: Sprint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            db.Sprints.Remove(sprint);
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
