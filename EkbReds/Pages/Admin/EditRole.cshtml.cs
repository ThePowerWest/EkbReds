using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    public class EditRoleModel : PageModel
    {
        RoleManager<Role> RoleManager;

        /// <summary>
        /// ctor
        /// </summary>
        public EditRoleModel(RoleManager<Role> roleManager)
        {
            RoleManager = roleManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

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
                    if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Roles"));
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
            [Display(Name="Новое название роли")]
            public string NewName { get; set; }
            [Display(Name = "Старое название роли")]
            public string Name { get; set; }
            public string Id { get; set; }
        }
    }
}