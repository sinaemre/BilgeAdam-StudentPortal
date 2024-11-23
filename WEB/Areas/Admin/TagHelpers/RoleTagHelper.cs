using Business.Manager.Interface;
using DTO.Concrete.RoleDTO;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WEB.Areas.Admin.TagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-role")]
    public class RoleTagHelper : TagHelper
    {
        private readonly IRoleManager _roleManager;
        private readonly IUserManager _userManager;

        public RoleTagHelper(IRoleManager roleManager, IUserManager userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("user-role")]
        public string RoleId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var role = await _roleManager.FindRoleByIdAsync<GetRoleDTO>(Guid.Parse(RoleId));
            var userNames = await _userManager.GetUserNamesHasRoleAsync(role.Name);

            output.Content.SetContent
                (
                    userNames.Count == 0 ? "Bu rolde hiçbir kullanıcı yok!" :
                    userNames.Count > 3 ?
                    (string.Join(", ", userNames.Take(3)) + "...") :
                    string.Join(", ", userNames)
                );

        }
    }
}
