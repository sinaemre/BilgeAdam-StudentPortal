using WEB.Areas.Admin.Models.ViewModels.Users;

namespace WEB.Areas.Admin.Models.ViewModels.Roles
{
    public class AssignRoleVM
    {
        public GetRoleVM? Role { get; set; }
        public string? RoleName { get; set; }

        public List<GetUserForRoleVM>? HasRole { get; set; }
        public List<GetUserForRoleVM>? HasNotRole { get; set; }

        public string[]? AddIds { get; set; }
        public string[]? RemoveIds { get; set; }
    }
}
