using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Factories
{
    public interface IUserFactory
    {
        User CreateUser(string userName, string firstName, string lastName, string email, string password);
    }
}
