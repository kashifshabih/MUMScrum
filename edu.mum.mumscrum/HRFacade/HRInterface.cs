using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using edu.mum.mumscrum.Models;

namespace edu.mum.mumscrum.HRFacade
{
    public interface HRInterface
    {
        IEnumerable<PartialEmployee> GetScrumMasters();
        IEnumerable<PartialEmployee> GetDevelopers();
        IEnumerable<PartialEmployee> GetTesters();
    }
}