using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using edu.mum.mumscrum.DAL;
using edu.mum.mumscrum.Models;

namespace edu.mum.mumscrum.Controllers
{
    [Authorize(Roles="Developer")]
    public class DeveloperController : Controller
    {
        private MUMScrumContext db = new MUMScrumContext();

        // GET: Developer
        public ActionResult Index()
        {
            string loggedInUserName = User.Identity.GetUserName();

            var userStories = db.UserStories.Include(u => u.ProductBacklog).Include(u => u.ReleaseBacklog).Include(u => u.Sprint)
                                .Where(u => u.SprintID != null && u.Developer.UserName == loggedInUserName);
            return View(userStories.ToList());
        }

        public ActionResult EstimateEffort(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EstimateEffort([Bind(Include = "ID,Name,Description,CreatedBy,CreatedDate,StartDate,ExpectedEndDate,ActualEndDate,DevelopmentStatus,TestStatus,SprintID,ReleaseBacklogID,ProductBacklogID,DeveloperID,TesterID,DeveloperEstimateInHours,TesterEstimateInHours,DeveloperHoursCompleted,TesterHoursCompleted")] UserStory userStory)
        {
            if (ModelState.IsValid)
            {
                if (userStory.DeveloperEstimateInHours == userStory.DeveloperHoursCompleted)
                {
                    userStory.DevelopmentStatus = USDevelopmentStatus.Completed;
                }
                
                db.Entry(userStory).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userStory);
        }
    }
}