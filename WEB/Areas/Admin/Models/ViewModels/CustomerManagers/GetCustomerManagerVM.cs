namespace WEB.Areas.Admin.Models.ViewModels.CustomerManagers
{
    public class GetCustomerManagerVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string HireDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
