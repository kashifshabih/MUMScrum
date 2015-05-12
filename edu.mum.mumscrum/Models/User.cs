using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace edu.mum.mumscrum.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] Sha1Password { get; set; }
        public DateTime RegisteredDate { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

      
    }
}