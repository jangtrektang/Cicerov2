using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Repositories
{
    public interface IUserRepository
    {
        IQueryable<User> FindAll();

        User FindById(int userId);

        void Save(params User[] users);

        void Delete(params User[] users);
    }
}
