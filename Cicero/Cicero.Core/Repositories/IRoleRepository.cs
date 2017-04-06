using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Repositories
{
    public interface IRoleRepository
    {
        IQueryable<Role> FindAll();

        Role FindById(int roleId);

        void Save(params Role[] roles);

        void Delete(params Role[] roles);
    }
}
