using ApplicationCore.Entities.Identity;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    public class EditUserModel : PageModel
    {
        public UserManager<User> UserManager;
        private readonly RoleManager<Role> RoleManager;
        private readonly IUserService UserService;
        public IEnumerable<Role> Roles;

        /// <summary>
        /// ctor
        /// </summary>
        public EditUserModel(UserManager<User> userManager, RoleManager<Role> roleManager, IUserService userService, IEnumerable<Role> roles)
        {
            UserManager = userManager;
            RoleManager = roleManager;
            UserService = userService;
            Roles = roles;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnPost()
        {
            Roles = RoleManager.Roles.ToList();
        }

        /// <summary>
        /// Изменяет пользователя
        /// </summary>
        public async Task<IActionResult> OnPostEditUserAsync()
        {
            if (ModelState.IsValid)
            {
                var user = UserManager.Users.FirstOrDefault(x => x.UserName == Input.UserName);
                if (Input.NewUserName != null) user.UserName = Input.NewUserName;
                user.Email=Input.Email;
                user.EmailConfirmed = Input.EmailConfirmed;
                if (Input.Role=="Admin")
                {
                    try
                    {
                        await UserService.RemoveFromRoleAsync(user.Id, "User");
                    }
                    catch { }
                    await UserService.AddToRoleAsync(user.Id, Input.Role);
                }
                if (Input.Role == "User")
                {
                    try
                    {
                        await UserService.RemoveFromRoleAsync(user.Id, "Admin");
                    }
                    catch { }
                    await UserService.AddToRoleAsync(user.Id, Input.Role);
                }
                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Users"));
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }

        /// <summary>
        /// Модель ввода данных со страницы
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "Поле обязательно!")]
            [EmailAddress]
            public string Email { get; set; }

            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Имя пользователя")]
            public string UserName { get; set; }

            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Новое Имя пользователя")]
            public string NewUserName { get; set; }

            [Display(Name = "Подтвержден")]
            public bool EmailConfirmed { get; set; }

            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name ="Роль")]
            public string Role { get; set; }
        }
    }
}