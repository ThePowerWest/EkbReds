﻿using ApplicationCore.Entities.Identity;
using ApplicationCore.Entities.Main;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IEnumerable<User>> FindUsersWithCurrentSeasonPaidAsync()
        {
            Season currentSeason = await SeasonRepository.CurrentAsync();
            return base.Users.Where(user => user.SeasonPaids.Any(seasonPaid => seasonPaid.Season == currentSeason))
                .Include(user => user.Predictions)
                    .ThenInclude(prediction => prediction.Match)
                        .ThenInclude(match => match.Tournament)
                            .ThenInclude(tournament => tournament.Season);
        }

        public IEnumerable<User> GetRandomUsers()
        {
            Random rnd = new();
            IEnumerable<User> users = base.Users.Where(user => user.EmailConfirmed);

            return users.OrderBy(x => rnd.Next()).Take(4);
        }
    }
}