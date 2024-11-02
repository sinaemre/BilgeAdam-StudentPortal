using ApplicationCore.Consts;

namespace WEB.Areas.Education.Models.ViewModels.Courses
{
    public class GetCourseVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
    }
}
