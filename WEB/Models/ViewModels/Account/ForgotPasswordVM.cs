using System.ComponentModel.DataAnnotations;

namespace WEB.Models.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Display(Name = "E-Mail")]
        public string? Email { get; set; }
    }
}
