using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Web.Pages.Admin
{
    /// <summary>
    /// �������� �������������� ����
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
        /// ������� �������� ������ �� ��������
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        /// ��������� �������� ����
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
        /// ������ ����� ������ �� ��������
        /// </summary>
        public class InputModel
        {
            /// <summary>
            /// ����� �������� ����
            /// </summary>
            [Required(ErrorMessage = "���� �����������!")]
            [Display(Name = "����� �������� ����")]
            public string NewName { get; set; }

            /// <summary>
            /// ������ �������� ����
            /// </summary>
            [Display(Name = "������ �������� ����")]
            public string Name { get; set; }

            /// <summary>
            /// �������������
            /// </summary>
            public string Id { get; set; }
        }
    }
}