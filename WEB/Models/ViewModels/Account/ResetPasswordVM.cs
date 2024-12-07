using System.ComponentModel.DataAnnotations;

namespace WEB.Models.ViewModels.Account
{
    public class ResetPasswordVM
    {
        public string Token { get; set; }
        public string Email { get; set; }

        [Display(Name = "Şifre")]
        public string? NewPassword { get; set; }

        [Display(Name = "Şifre Tekar")]
        public string? CheckNewPassword { get; set; }
    }
}
