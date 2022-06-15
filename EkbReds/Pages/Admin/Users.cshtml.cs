using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class UsersModel : PageModel
    {
        public UserManager<User> UserManager;
        private readonly RoleManager<Role> RoleManager;
        public IEnumerable<User> Users;
        public IEnumerable<Role> Roles;
        private readonly IUserService UserService;

        /// <summary>
        /// ctor
        /// </summary>
        public UsersModel(UserManager<User> userManager, IEnumerable<User> users, RoleManager<Role> roleManager, IEnumerable<Role> roles, IUserService userService)
        {
            UserManager = userManager;
            Users = users;
            RoleManager = roleManager;
            Roles = roles;
            UserService = userService;
        }

        [BindProperty]
        public InputUserModel Input { get; set; }

        /// <summary>
        /// Получает список пользователей
        /// </summary>
        public async void OnGet()
        {
            Users = UserManager.Users.ToList();
            Roles = RoleManager.Roles.ToList();
        }

        /// <summary>
        /// Удаляет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostDeleteUserAsync(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                User user = await UserManager.FindByIdAsync(id);
                if (user != null)
                {
                    IdentityResult result = await UserManager.DeleteAsync(user);
                    if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Users"));
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            return Page();
        }

        /// <summary>
        /// Добавляет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostAddUserAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new User { Email = Input.Email, UserName = Input.UserName, EmailConfirmed=Input.EmailConfirmed };
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded) 
                {
                    await UserService.AddToRoleAsync(user.Id, Input.Role);
                    return LocalRedirect(Url.Content("~/Admin/Users"));
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return Page();
        }

        /// <summary>
        /// Модель ввода данных со страницы
        /// </summary>
        public class InputUserModel
        {
            [Required(ErrorMessage = "Поле обязательно!")]
            [StringLength(20,
                ErrorMessage = "{0} должно содержать от {2} до {1} символов.",
                MinimumLength = 4)]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Поле обязательно!")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Поле обязательно!")]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; }

            [Display(Name = "Подтвержден")]
            public bool EmailConfirmed { get; set; }

            [Required(ErrorMessage = "Поле обязательно!")]
            public string Role { get; set; }
        }
    }
}