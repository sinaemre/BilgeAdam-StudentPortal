using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Admin.Models.ViewModels.CustomerManagers
{
    public class CreateCustomerManagerVM
    {
        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string? LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string? Email { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "İşe Giriş Tarihi")]
        public DateTime? HireDate { get; set; }
    }
}
