using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence.Configurations
{
    public class RoleConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleConfiguration()
        {
            Property(x => x.CodeName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnAnnotation(IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new System.ComponentModel.DataAnnotations.Schema.IndexAttribute() { IsUnique = true }));

            Property(x => x.DisplayName)
                .IsRequired()
                .HasMaxLength(150);
        }
    }
}
