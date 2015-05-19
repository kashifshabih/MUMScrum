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
    }
}