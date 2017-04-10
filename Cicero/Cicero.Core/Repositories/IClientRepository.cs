using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Repositories
{
    public interface IClientRepository
    {
        IQueryable<Client> FindAll();

        Client FindById(string clientId);

        void Save(params Client[] clients);

        void Delet(params Client[] clients);
    }
}
