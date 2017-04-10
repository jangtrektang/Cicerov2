using Cicero.Core.Models;
using Cicero.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly Cicerov2Context _context;

        public ClientRepository(Cicerov2Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<Client> FindAll()
        {
            return _context.Clients.AsQueryable();
        }

        public Client FindById(string clientId)
        {
            return _context.Clients.SingleOrDefault(x => x.Id == clientId);
        }

        public void Save(params Client[] clients)
        {
            foreach(var client in clients)
            {
                if (_context.Entry(client).State == System.Data.Entity.EntityState.Detached)
                    _context.Clients.Add(client);

                // TODO: add update
            }

            _context.SaveChanges();
        }

        public void Delete(params Client[] clients)
        {
            _context.Clients.RemoveRange(clients);
            _context.SaveChanges();
        }
    }
}
