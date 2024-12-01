using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class UpdateStudentVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Display(Name = "Ad")]
        public string? FirstName { get; set; }

        [Display(Name = "Soyad")]
        public string LastName { get; set; }

        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Sınıf")]
        public Guid? ClassroomId { get; set; }

        [Display(Name = "Kurs")]
        public Guid? CourseId { get; set; }

        [Display(Name = "Eğitmen")]
        public Guid? TeacherId { get; set; }

        public string? ImagePath { get; set; }

        [Display(Name = "Resim")]
        public IFormFile? Image { get; set; }

        [Display(Name = "Kayıt Ücreti")]
        public double? RegisterPrice { get; set; }

        public SelectList? Courses { get; set; }
        public SelectList? Classrooms { get; set; }
        public SelectList? Teachers { get; set; }
    }
}
