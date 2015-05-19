using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using edu.mum.mumscrum.Models;
using edu.mum.mumscrum.DAL;
using edu.mum.mumscrum.Controllers;

namespace edu.mum.mumscrum.HRFacade
{
    public class clsHRFacade : HRInterface
    {
        EmployeeController objEmployee = new EmployeeController(); 
        public IEnumerable<PartialEmployee> GetScrumMasters() 
        {
            return objEmployee.GetScrumMasters();
        }
        public IEnumerable<PartialEmployee> GetDevelopers()
        {
            return objEmployee.GetDevelopers();
        }
        public IEnumerable<PartialEmployee> GetTesters()
        {
            return objEmployee.GetTesters();
        }
        public void AssignScrumMaster(int employeeID, ReleaseBacklog releaseBacklog, MUMScrumContext db)
        { 
            objEmployee.AssignScrumMaster(employeeID, releaseBacklog, db);
        }
        public void RemoveScrumMasterRole(ReleaseBacklog releaseBacklog, MUMScrumContext db)
        {
            objEmployee.RemoveScrumMasterRole(releaseBacklog, db);
        }
        public void AssignDeveloper(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            objEmployee.AssignDeveloper(employeeID, userStory, db);
        }
        public void AssignTester(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            objEmployee.AssignTester(employeeID, userStory, db);
        }
        public void RemoveDeveloperRole(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            objEmployee.RemoveDeveloperRole(employeeID, userStory, db);
        }
        public void RemoveTesterRole(int employeeID, UserStory userStory, MUMScrumContext db)
        {
            objEmployee.RemoveTesterRole(employeeID, userStory, db);
        }
    }
}