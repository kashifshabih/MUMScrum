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
    [Authorize(Roles = "ProductOwner")]
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

            var releaseBacklog = new ReleaseBacklog();
            releaseBacklog.UserStories = new List<UserStory>();

            PopulateAssignedUserStories(releaseBacklog);

            // hr interface method has to be given here
            ViewBag.ScrumMasters = db.Employees.ToList()
                                            .Where(e => e.Position.EmpPosition == "Senior Software Engineer" && e.Role == null)
                                            .Select(
                                                e => new { ID = e.ID, Name = e.FirstName + ' ' + e.LastName }
                                            );


            return View();
        }

        private void PopulateAssignedUserStories(ReleaseBacklog releaseBacklog)
        {
            var allUserStories = db.UserStories.Where(u => u.ReleaseBacklogID == null || u.ReleaseBacklogID == releaseBacklog.ID);

            var releaseBacklogUserStories = new HashSet<int>(releaseBacklog.UserStories.Select(u => u.ID));

            var viewModel = new List<AssignedUserStoryData>();

            foreach (var userStory in allUserStories)
            {
                viewModel.Add(new AssignedUserStoryData
                {
                    UserStoryID = userStory.ID,
                    Name = userStory.Name,
                    Assigned = releaseBacklogUserStories.Contains(userStory.ID)
                });
            }

            ViewBag.UserStories = viewModel;
        }

        // POST: ReleaseBacklog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,ProductBacklogID,ScrumMasterID")] ReleaseBacklog releaseBacklog, string[] selectedUserStories, string ScrumMasterList)
        {
            if (selectedUserStories != null)
            {
                //instructor.Courses = new List<Course>();
                releaseBacklog.UserStories = new List<UserStory>();
                //foreach (var course in selectedCourses)
                foreach (var userStory in selectedUserStories)
                {
                    //var courseToAdd = db.Courses.Find(int.Parse(course));
                    var userStoryToAdd = db.UserStories.Find(int.Parse(userStory));
                    //instructor.Courses.Add(courseToAdd);
                    releaseBacklog.UserStories.Add(userStoryToAdd);
                }
            }

            if (ModelState.IsValid)
            {
                if (ScrumMasterList != "")
                {
                    releaseBacklog.ScrumMasterID = Convert.ToInt32(ScrumMasterList);
                    //hr interface method has to be given here
                }
                
                releaseBacklog.CreatedBy = 1;
                releaseBacklog.CreatedDate = DateTime.Now;

                db.ReleaseBacklogs.Add(releaseBacklog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //PopulateAssignedCourseData(instructor);
            PopulateAssignedUserStories(releaseBacklog);

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

            // hr interface method has to be given here
            ViewBag.ScrumMasters = db.Employees.ToList()
                                .Where(e => e.Position.EmpPosition == "Senior Software Engineer")
                                .Select(
                                    e => new { ID = e.ID, Name = e.FirstName + ' ' + e.LastName }
                                );

            
            //ReleaseBacklog releaseBacklog = db.ReleaseBacklogs.Find(id);

            ReleaseBacklog releaseBacklog = db.ReleaseBacklogs
                .Include(r => r.UserStories)
                .Where(r => r.ID == id)
                .Single();

            PopulateAssignedUserStories(releaseBacklog);


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
        //public ActionResult Edit([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,ProductBacklogID,ScrumMasterID")] ReleaseBacklog releaseBacklog, string[] selectedUserStories)
        public ActionResult Edit(int? id, string[] selectedUserStories)
        {
            ReleaseBacklog releaseBacklog = db.ReleaseBacklogs
            .Include(r => r.UserStories)
            .Where(r => r.ID == id)
            .Single();
            
            if (ModelState.IsValid)
            {
                UpdateReleaseBacklogUserStories(selectedUserStories, releaseBacklog);

                db.Entry(releaseBacklog).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProductBacklogID = new SelectList(db.ProductBacklogs, "ID", "Name", releaseBacklog.ProductBacklogID);

            PopulateAssignedUserStories(releaseBacklog);
            return View(releaseBacklog);
        }

        private void UpdateReleaseBacklogUserStories(string[] selectedUserStories, ReleaseBacklog releaseBacklogToUpdate)
        {
            if (selectedUserStories == null)
            {
                releaseBacklogToUpdate.UserStories = new List<UserStory>();
                return;
            }

            var selectedUserStoriesHS = new HashSet<string>(selectedUserStories);
            var releaseBacklogUserStories = new HashSet<int>(releaseBacklogToUpdate.UserStories.Select(u => u.ID));
            
            foreach (var userStory in db.UserStories)
            {
                if (selectedUserStoriesHS.Contains(userStory.ID.ToString()))
                {
                    if (!releaseBacklogUserStories.Contains(userStory.ID))
                    {
                        releaseBacklogToUpdate.UserStories.Add(userStory);
                    }
                }
                else
                {
                    if (releaseBacklogUserStories.Contains(userStory.ID))
                    {
                        releaseBacklogToUpdate.UserStories.Remove(userStory);
                    }
                }
            }
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
