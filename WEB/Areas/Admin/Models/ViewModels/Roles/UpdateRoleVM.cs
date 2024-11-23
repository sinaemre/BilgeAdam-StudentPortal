namespace WEB.Areas.Admin.Models.ViewModels.Roles
{
    public class UpdateRoleVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Name { get; set; }
    }
}
