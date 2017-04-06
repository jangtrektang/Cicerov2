using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Factories
{
    public class UserFactory : IUserFactory
    {
        public User CreateUser(string userName, string firstName, string lastName, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(userName));

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(email));

            var entity = new User(userName, firstName, lastName, email, password);

            return entity;
        }
    }
}
