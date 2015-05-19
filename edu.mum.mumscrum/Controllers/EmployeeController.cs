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
using edu.mum.mumscrum.HRFacade;

namespace edu.mum.mumscrum.Controllers
{
    [Authorize(Roles = "HRAdministrator")]
    public class EmployeeController : Controller
    {
        private MUMScrumContext db = new MUMScrumContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Position);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "EmpPosition");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Gender,CreatedBy,CreatedDate,HiringDate,PositionID,EmployeeStatus,UserName,Password,Role")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.CreatedBy = 1;
                employee.CreatedDate = DateTime.Now;

                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.Positions, "ID", "EmpPosition", employee.PositionID);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "EmpPosition", employee.PositionID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Gender,CreatedBy,CreatedDate,HiringDate,PositionID,EmployeeStatus,UserName,Password,Role")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionID = new SelectList(db.Positions, "ID", "EmpPosition", employee.PositionID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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

        public IEnumerable<PartialEmployee> GetScrumMasters()
        {
            IEnumerable<PartialEmployee> scrumMastersList = db.Employees.ToList()
                                .Where(e => e.Position.EmpPosition == "Senior Software Engineer")
                                .Select(
                                    e => new PartialEmployee { ID = e.ID, Name = e.FirstName + ' ' + e.LastName }
                                );

            return scrumMastersList;
        }

        public IEnumerable<PartialEmployee> GetDevelopers()
        {
            IEnumerable<PartialEmployee> DevelopersList = db.Employees.ToList()
                                .Where(e => e.Position.EmpPosition == "Software Engineer")
                                .Select(
                                    e => new PartialEmployee { ID = e.ID, Name = e.FirstName + ' ' + e.LastName }
                                );

            return DevelopersList;
        }

        public IEnumerable<PartialEmployee> GetTesters()
        {
            IEnumerable<PartialEmployee> TestersList = db.Employees.ToList()
                                .Where(e => e.Position.EmpPosition == "Software Test Engineer")
                                .Select(
                                    e => new PartialEmployee { ID = e.ID, Name = e.FirstName + ' ' + e.LastName }
                                );

            return TestersList;
        }

        public void AssignScrumMaster(int employeeID, ReleaseBacklog releaseBacklog, MUMScrumContext db)
        {
            var employee = db.Employees.Find(employeeID);
            employee.Role = Role.ScrumMaster;
            employee.ReleaseBacklogs.Add(releaseBacklog);

            db.Entry(employee).State = EntityState.Modified;
        }

        public void RemoveScrumMasterRole(ReleaseBacklog releaseBacklog, MUMScrumContext db)
        {
            var employee = db.Employees.Find(releaseBacklog.EmployeeID);

            if (employee != null)
            {
                if (employee.ReleaseBacklogs.Count == 1)
                {
                    employee.Role = null;
                }
                employee.ReleaseBacklogs.Remove(releaseBacklog);

                db.Entry(employee).State = EntityState.Modified;
            }
        }
        public void AssignDeveloper(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            var employee = db.Employees.Find(employeeID);
            employee.Role = Role.Developer;
            employee.UserStories.Add(userStory);
            
            db.Entry(employee).State = EntityState.Modified;
        }
        public void AssignTester(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            var employee = db.Employees.Find(employeeID);
            employee.Role = Role.Tester;
            employee.UserStories.Add(userStory);

            db.Entry(employee).State = EntityState.Modified;
        }
        public void RemoveDeveloperRole(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            var employee = db.Employees.Find(employeeID);

            if (employee != null)
            {
                if (employee.UserStories.Count == 1)
                {
                    employee.Role = null;
                }

                employee.UserStories.Remove(userStory);

                db.Entry(employee).State = EntityState.Modified;
            }
        }
        public void RemoveTesterRole(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            var employee = db.Employees.Find(employeeID);

            if (employee != null)
            {
                if (employee.UserStories.Count == 1)
                {
                    employee.Role = null;
                }

                employee.UserStories.Remove(userStory);

                db.Entry(employee).State = EntityState.Modified;
            }
        }
    }
}
