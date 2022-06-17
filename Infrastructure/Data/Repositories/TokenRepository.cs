using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces;

namespace Infrastructure.Data.Repositories
{
    /// <summary>
    /// Репозиторий токенов
    /// </summary>
    public class TokenRepository:EfRepository<ApiToken>, IReadRepository<ApiToken>, IRepository<ApiToken>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public TokenRepository(MainContext context):base (context)
        {}
    }
}