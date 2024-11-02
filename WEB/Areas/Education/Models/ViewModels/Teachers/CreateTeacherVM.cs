using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Teachers
{
    public class CreateTeacherVM
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

        [Display(Name = "Ders")]
        public Guid? CourseId { get; set; }
        public SelectList? Courses { get; set; }
    }
}
