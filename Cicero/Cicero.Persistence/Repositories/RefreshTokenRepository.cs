using Cicero.Core.Models;
using Cicero.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Persistence.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly Cicerov2Context _context;

        public RefreshTokenRepository(Cicerov2Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public IQueryable<RefreshToken> FindAll()
        {
            return _context.RefreshTokens.AsQueryable();
        }

        public RefreshToken FindById(string refreshTokenId)
        {
            return _context.RefreshTokens.SingleOrDefault(x => x.Id == refreshTokenId);
        }

        public void Save(params RefreshToken[] refreshTokens)
        {
            foreach(var refreshToken in refreshTokens)
            {
                if (_context.Entry(refreshToken).State == System.Data.Entity.EntityState.Detached)
                    _context.RefreshTokens.Add(refreshToken);

                // TODO: add update
            }

            _context.SaveChanges();
        }

        public void Delete(params RefreshToken[] refreshTokens)
        {
            _context.RefreshTokens.RemoveRange(refreshTokens);
            _context.SaveChanges();
        }
    }
}
