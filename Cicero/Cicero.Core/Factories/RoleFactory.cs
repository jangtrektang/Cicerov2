using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Models;

namespace Cicero.Core.Factories
{
    public class RoleFactory : IRoleFactory
    {
        public Role CreateRole(string codeName, string displayName)
        {
            if (string.IsNullOrWhiteSpace(codeName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(codeName));

            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(displayName));

            var entity = new Role(codeName, displayName);

            return entity;
        }
    }
}
