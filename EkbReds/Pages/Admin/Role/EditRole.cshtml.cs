using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    /// <summary>
    /// Страница редактирования роли
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class EditRoleModel : PageModel
    {
        private readonly RoleManager<Role> RoleManager;

        /// <summary>
        /// ctor
        /// </summary>
        public EditRoleModel(RoleManager<Role> roleManager)
        {
            RoleManager = roleManager;
        }

        /// <summary>
        /// Элемент передачи данных со страницы
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// Изменияет название роли
        /// </summary>
        public async Task<IActionResult> OnPostEditRoleAsync()
        {
            if (!string.IsNullOrEmpty(Input.Name))
            {
                Role role = await RoleManager.FindByNameAsync(Input.Name);
                if (role != null)
                {
                    role.Name = Input.NewName;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Role/Roles"));
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
        /// Модель ввода данных со страницы
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// Новое название роли
            /// </summary>
            [Required(ErrorMessage = "Поле обязательно!")]
            [Display(Name = "Новое название роли")]
            public string NewName { get; set; }

            /// <summary>
            /// Старое название роли
            /// </summary>
            [Display(Name = "Старое название роли")]
            public string Name { get; set; }

            /// <summary>
            /// Иднетификатор
            /// </summary>
            public string Id { get; set; }
        }
    }
}