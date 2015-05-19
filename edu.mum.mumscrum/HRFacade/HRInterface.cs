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
        void AssignScrumMaster(int emplyeeID, ReleaseBacklog releaseBacklog, MUMScrumContext db);
        void RemoveScrumMasterRole(ReleaseBacklog releaseBacklog, MUMScrumContext db);
        //void AssignScrumMaster();
        //void AssignScrumMaster();
    }
}