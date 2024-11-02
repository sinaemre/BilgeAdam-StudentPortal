using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Courses
{
    public class CreateCourseVM
    {
        [Display(Name = "Kurs")]
        public string? Name { get; set; }
    }
}
