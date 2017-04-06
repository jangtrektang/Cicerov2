using Cicero.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        public User()
        {

        }

        public User(string userName, string firstName, string lastName, string email, string password)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = MembershipHelper.HashPassword(password);
            CreatedOn = DateTime.Now;
        }

        public User(string userName, string firstName, string lastName, string email, string password, IEnumerable<Role> roles) 
            : this(userName, firstName, lastName, email, password)
        {
            Roles = roles.ToArray();
        }
    }
    
}
