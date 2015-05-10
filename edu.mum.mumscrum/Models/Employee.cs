using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.Models
{
    public enum Gender
    {
        Male, Female
    }
    public enum EmployeeStatus
    {
        Active, Archived
    }

    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime DOB { get; set; }
        public int CreatedBy { get; set; } //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public DateTime CreatedDate { get; set; }
        public DateTime HiringDate { get; set; }
        public int PositionID { get; set; }  //have to specify this as the foreign key // see whether these have to be formed into navigation properties
        public EmployeeStatus EmployeeStatus { get; set; }

        public virtual Position Position { get; set; }
    }
}