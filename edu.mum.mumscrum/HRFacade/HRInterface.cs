using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using edu.mum.mumscrum.Models;
using edu.mum.mumscrum.DAL;

namespace edu.mum.mumscrum.HRFacade
{
    public interface HRInterface
    {
        IEnumerable<PartialEmployee> GetScrumMasters();
        IEnumerable<PartialEmployee> GetDevelopers();
        IEnumerable<PartialEmployee> GetTesters();
        void AssignScrumMaster(int employeeID, ReleaseBacklog releaseBacklog, MUMScrumContext db);
        void RemoveScrumMasterRole(ReleaseBacklog releaseBacklog, MUMScrumContext db);
        void AssignDeveloper(int employeeID, UserStory userStory, MUMScrumContext db);
        void AssignTester(int employeeID, UserStory userStory, MUMScrumContext db);
        void RemoveDeveloperRole(int employeeID, UserStory userStory, MUMScrumContext db);
        void RemoveTesterRole(int employeeID, UserStory userStory, MUMScrumContext db);
    }
}