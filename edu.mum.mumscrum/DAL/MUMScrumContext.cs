using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using edu.mum.mumscrum.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace edu.mum.mumscrum.DAL
{
    public class MUMScrumContext : DbContext
    {
        public MUMScrumContext()
            : base("MUMScrumContext")
        {
        }

        public DbSet<ProductBacklog> ProductBacklogs { get; set; }
        public DbSet<ReleaseBacklog> ReleaseBacklogs { get; set; }
        public DbSet<Sprint> Sprints { get; set; }
        public DbSet<UserStory> UserStories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<edu.mum.mumscrum.Models.Employee> Employees { get; set; }

        public System.Data.Entity.DbSet<edu.mum.mumscrum.Models.Position> Positions { get; set; }
    }
}