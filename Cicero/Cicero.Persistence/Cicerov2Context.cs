using Cicero.Core.Models;
using Cicero.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence
{
    public class Cicerov2Context : DbContext
    {
        public Cicerov2Context() : base("Name = Cicerov2Context")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new RoleConfiguration());
        }

        #region DbSets

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        #endregion
    }
}
