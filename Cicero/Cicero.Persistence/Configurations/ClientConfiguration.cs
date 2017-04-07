using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            HasKey(x => new { x.Id });

            Property(x => x.Secret)
                .IsRequired();

            Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            Property(x => x.AllowedOrigin)
                .HasMaxLength(100);
        }
    }
}
