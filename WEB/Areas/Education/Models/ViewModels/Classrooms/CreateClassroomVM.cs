using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Classrooms
{
    public class CreateClassroomVM
    {
        [Display(Name = "Sınıf")]
        public string? Name { get; set; }

        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        [Display(Name = "Eğitmen")]
        public Guid? TeacherId { get; set; }

        [Display(Name = "Kurs")]
        public Guid? CourseId { get; set; }
        
        public SelectList? Courses { get; set; }

        [Display(Name = "Başlangıç Tarihi")]
        public DateTime? StartDate { get; set; }
        
        [Display(Name = "Bitiş Tarihi")]
        public DateTime? EndDate { get; set; }

    }
}
