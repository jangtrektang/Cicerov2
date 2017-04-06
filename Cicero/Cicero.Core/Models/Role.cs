using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string CodeName { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public Role()
        {

        }

        public Role(string codeName, string displayName)
        {
            CodeName = codeName;
            DisplayName = displayName;
            CreatedOn = DateTime.Now;
        }
    }
}
