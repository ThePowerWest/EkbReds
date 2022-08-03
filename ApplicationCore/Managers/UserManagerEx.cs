using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationCore.Managers
{
    /// <summary>
    /// 
    /// </summary>
    public class UserManagerEx : UserManager<User>
    {
        ISeasonRepository SeasonRepository;

        /// <summary>
        /// ctor
        /// </summary>
        public UserManagerEx(IUserStore<User> store,
                             IOptions<IdentityOptions> optionsAccessor,
                             IPasswordHasher<User> passwordHasher,
                             IEnumerable<IUserValidator<User>> userValidators,
                             IEnumerable<IPasswordValidator<User>> passwordValidators,
                             ILookupNormalizer keyNormalizer,
                             IdentityErrorDescriber errors,
                             IServiceProvider services,
                             ILogger<UserManager<User>> logger,
                             ISeasonRepository seasonRepository) :
        base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            SeasonRepository = seasonRepository;
        }

        /// <summary>
        /// Найти пользователей кто оплатил текущий сезон
        /// </summary>
        /// <returns>Список пользователей</returns>
        public async Task<IEnumerable<User>> FindUsersWithCurrentSeasonPaid()
        {
            Season currentSeason = await SeasonRepository.CurrentAsync();
            return base.Users.Where(user => user.SeasonPaids.Any(seasonPaid => seasonPaid.Season == currentSeason));
        }
    }
}