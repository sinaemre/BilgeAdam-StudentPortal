using ApplicationCore.Consts;
using System.ComponentModel.DataAnnotations;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class GetStudentVM
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string CourseName { get; set; }
        public string ClassroomName { get; set; }
        public string TeacherName { get; set; }
        public string? RegisterPrice { get; set; }
        public double? Average { get; set; }
        public string Status { get; set; }
        public string StudentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
