using Cicero.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Models;
using System.Data.Entity;
using Cicero.Core.Helpers;

namespace Cicero.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly Cicerov2Context _context;

        public UserRepository(Cicerov2Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public UserRepository()
        {
            _context = new Cicerov2Context();
        }

        public IQueryable<User> FindAll()
        {
            return _context.Users.AsQueryable();
        }

        public User FindById(int userId)
        {
            return _context.Users.SingleOrDefault(x => x.Id == userId);
        }

        public async Task<User> FindUser(string userName, string password)
        {
            var passwordHashed = MembershipHelper.HashPassword(password);
            var user = await _context.Users.Where(x => x.UserName == userName && x.Password == passwordHashed).FirstOrDefaultAsync();
            return user;
        }

        public void Save(params User[] users)
        {
            foreach(var user in users)
            {
                if (_context.Entry(user).State == System.Data.Entity.EntityState.Detached)
                    _context.Users.Add(user);
                
                // TODO: update changes
            }

            _context.SaveChanges();
        }

        public void Delete(params User[] users)
        {
            _context.Users.RemoveRange(users);
            _context.SaveChanges();
        }
    }
}
