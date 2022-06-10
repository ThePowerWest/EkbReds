using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    public class RolesModel : PageModel
    {
        RoleManager<Role> RoleManager;
        public IEnumerable<Role> Roles;
        public RolesModel(RoleManager<Role> roleManager, IEnumerable<Role> roles)
        {
            RoleManager = roleManager;
            Roles = roles;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
            Roles = RoleManager.Roles.ToList();
        }
        /// <summary>
        /// Добавляет новую роль.
        /// </summary>
        public async Task<IActionResult> OnPostAddRoleAsync()
        {

            if (!string.IsNullOrEmpty(Input.Name))
            {
                IdentityResult result = await RoleManager.CreateAsync(new Role { Name = Input.Name });
                if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Roles"));
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
        /// Удаляет существующую роль по ее названию.
        /// </summary>
        public async Task<IActionResult> OnPostDeleteRoleAsync()
        {

            if (!string.IsNullOrEmpty(Input.Name))
            {
                Role role = await RoleManager.FindByNameAsync(Input.Name);
                if (role != null) 
                {
                    IdentityResult result = await RoleManager.DeleteAsync(role);
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
    }
    public class InputModel
    {
        [Display(Name = "Название роли")]
        public string Name { get; set; }
    }

}