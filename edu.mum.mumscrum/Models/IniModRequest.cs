using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.Models
{
    public enum IMRStatus
    {
        New, Approved, Disapproved
    }

    public class IniModRequest
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public int ProductBacklogID { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public IMRStatus IMRStatus { get; set; }
    }
}