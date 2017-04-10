using Cicero.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cicero.Core.Repositories
{
    public interface IRefreshTokenRepository
    {
        IQueryable<RefreshToken> FindAll();

        RefreshToken FindById(string tokenId);

        void Save(params RefreshToken[] refreshTokens);

        void Delete(params RefreshToken[] refreshTokens);
    }
}
