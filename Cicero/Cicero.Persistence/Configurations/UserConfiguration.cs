using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence.Configurations
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(x => x.UserName)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.Email)
                .IsRequired();
        }
    }
}
