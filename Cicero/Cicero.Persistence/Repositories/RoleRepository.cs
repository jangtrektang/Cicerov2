using Cicero.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Models;

namespace Cicero.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly Cicerov2Context _context;

        public RoleRepository(Cicerov2Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }
       
        public IQueryable<Role> FindAll()
        {
            return _context.Roles.AsQueryable();
        }

        public Role FindById(int roleId)
        {
            return _context.Roles.SingleOrDefault(x => x.Id == roleId);
        }

        public void Save(params Role[] roles)
        {
            foreach(var role in roles)
            {
                if (_context.Entry(role).State == System.Data.Entity.EntityState.Detached)
                    _context.Roles.Add(role);

                // TODO: add update 
            }

            _context.SaveChanges();
        }

        public void Delete(params Role[] roles)
        {
            _context.Roles.RemoveRange(roles);
            _context.SaveChanges();
        }

    }
}
