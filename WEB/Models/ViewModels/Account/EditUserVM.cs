using System.ComponentModel.DataAnnotations;

namespace WEB.Models.ViewModels.Account;

public class EditUserVM
{
  public Guid Id { get; set; }
  public DateTime CreatedDate { get; set; }
  public DateTime? UpdatedDate { get; set; }

  [Display(Name = "Ad")]
  public string? FirstName { get; set; }
  
  [Display(Name = "Soyad")]
  public string? LastName { get; set; }
  
  [Display(Name = "E-Mail")]
  public string? Email { get; set; }
  
  [Display(Name = "Kullanıcı Adı")]
  public string? UserName { get; set; }
  
  [Display(Name = "Doğum Tarihi")]
  [DataType(DataType.Date)]
  public DateTime? BirthDate { get; set; }
  
  [Display(Name = "Şifre")]
  public string? Password { get; set; }
}