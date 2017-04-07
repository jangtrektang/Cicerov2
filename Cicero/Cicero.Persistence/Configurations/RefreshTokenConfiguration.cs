using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence.Configurations
{
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration()
        {
            HasKey(x => new { x.Id });

            Property(x => x.Subject)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.ClientId)
                .IsRequired()
                .HasMaxLength(50);

            Property(x => x.ProtectedTicket);
        }
    }
}
