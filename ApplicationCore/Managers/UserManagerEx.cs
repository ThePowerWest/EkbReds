using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ApplicationCore.Managers
{
    /// <summary>
    /// Расширение функций менеджера сущности User
    /// </summary>
    public class UserManagerEx : UserManager<User>
    {
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
        { }
    }
}