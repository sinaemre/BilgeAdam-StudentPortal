using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Courses
{
    public class UpdateCourseVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Kurs")]
        public string? Name { get; set; }

        [Display(Name = "Toplam Saat")]
        public int? TotalHour { get; set; }
    }
}
