using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.Models
{
    public enum USDevelopmentStatus
    {
        New, Assigned, Working, Completed
    }

    public enum USTestStatus
    {
        WaitingDevelopment, Assigned, Working, Completed
    }


    public class UserStory
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties // this is product ownerID
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public USDevelopmentStatus DevelopmentStatus { get; set; }
        public USTestStatus TestStatus { get; set; }
        public int? SprintID { get; set; }
        public int? ReleaseBacklogID { get; set; }
        public int ProductBacklogID { get; set; }
        public int? DeveloperID { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public int? TesterID { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public int? DeveloperEstimateInHours { get; set; }
        public int? TesterEstimateInHours { get; set; }

        public int? DeveloperHoursCompleted { get; set; }
        public int? TesterHoursCompleted { get; set; }

        public virtual Sprint Sprint { get; set; }
        public virtual ReleaseBacklog ReleaseBacklog { get; set; }
        public virtual ProductBacklog ProductBacklog { get; set; }

        public virtual Employee Developer { get; set; }

        public virtual Employee Tester { get; set; }
    }
}