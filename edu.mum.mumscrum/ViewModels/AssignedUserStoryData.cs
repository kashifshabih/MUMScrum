using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.ViewModels
{
    public class AssignedUserStoryData
    {
        public int UserStoryID { get; set; }
        public string Name { get; set; }
        public bool Assigned { get; set; }
    }
}