using ApplicationCore.Consts;

namespace WEB.Areas.Education.Models.ViewModels.Teachers
{
    public class GetTeacherVM
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string BirthDate { get; set; }
        public string HireDate { get; set; }
        public string CourseName { get; set; }
    }
}
