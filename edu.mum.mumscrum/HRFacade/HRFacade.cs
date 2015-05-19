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
        MUMScrumContext db = new MUMScrumContext();

        public IEnumerable<PartialEmployee> GetScrumMasters() 
        {
            EmployeeController objEmployee = new EmployeeController(); ;
            return objEmployee.GetScrumMasters();
        }
        public IEnumerable<PartialEmployee> GetDevelopers()
        {
            return null;
        }
        public IEnumerable<PartialEmployee> GetTesters()
        {
            return null;
        }
    }
}