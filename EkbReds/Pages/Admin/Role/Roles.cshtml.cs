using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    /// <summary>
    /// �������� � ������
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class RolesModel : PageModel
    {
        public RoleManager<Role> RoleManager;

        /// <summary>
        /// ctor
        /// </summary>
        public RolesModel(RoleManager<Role> roleManager)
        {
            RoleManager = roleManager;
        }

        /// <summary>
        /// ������� �������� ������ �� ��������
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// ��������� ����� ����
        /// </summary>
        public async Task<IActionResult> OnPostAddRoleAsync()
        {
            if (!string.IsNullOrEmpty(Input.Name))
            {
                IdentityResult result = await RoleManager.CreateAsync(new Role { Name = Input.Name, Description = "test" });
                if (result.Succeeded) return LocalRedirect(Url.Content("~/Admin/Role/Roles"));
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
        /// ������� ������������ ���� �� �� ��������
        /// </summary>
        public async Task<IActionResult> OnPostDeleteRoleAsync(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Role role = await RoleManager.FindByNameAsync(name);
                if (role != null)
                {
                    IdentityResult result = await RoleManager.DeleteAsync(role);
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
        /// ������ ����� ������ �� ��������
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// �������� ����
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "�������� ����")]
            public string Name { get; set; }
        }
    }
}