using Cicero.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cicero.Core.Models;

namespace Cicero.Persistence.Repositories
{
    public class AuthorizationRepository : IAuthorizationRepository
    {
        private readonly Cicerov2Context _context;

        public AuthorizationRepository(Cicerov2Context context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            _context = context;
        }

        public AuthorizationRepository()
        {
            _context = new Cicerov2Context();
        }

        public Client FindClient(string clientId)
        {
            return _context.Clients.Find(clientId);
        }

        public async Task<bool> AddRefreshToken(RefreshToken refreshToken)
        {
            var existingToken = _context.RefreshTokens
                .Where(x => x.Subject == refreshToken.Subject && x.ClientId == refreshToken.ClientId)
                .SingleOrDefault();

            if (existingToken != null)
            {
                var result = await RemoveRefreshToken(existingToken);
            }

            _context.RefreshTokens.Add(refreshToken);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<RefreshToken> FindRefreshToken(string refreshTokenId)
        {
            return await _context.RefreshTokens.FindAsync(refreshTokenId);
        }        

        public async Task<bool> RemoveRefreshToken(RefreshToken refreshToken)
        {
            _context.RefreshTokens.Remove(refreshToken);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRefreshToken(string refreshTokenId)
        {
            var refreshToken = await _context.RefreshTokens.FindAsync(refreshTokenId);

            if (refreshToken == null)
                return false;

            _context.RefreshTokens.Remove(refreshToken);
            return await _context.SaveChangesAsync() > 0;
        }

        public List<RefreshToken> GetAllRefreshTokens()
        {
            return _context.RefreshTokens.ToList();
        }
    }
}
