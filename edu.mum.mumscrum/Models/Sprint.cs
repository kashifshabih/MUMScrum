using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.Models
{
    public class Sprint
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public DateTime CreatedDate { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? ExpectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public int ReleaseBacklogID { get; set; }

        public virtual ReleaseBacklog ReleaseBacklog { get; set; }
        public virtual ICollection<UserStory> UserStories { get; set; }
    }
}