using ApplicationCore.Consts;
using ApplicationCore.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Areas.Education.Models.ViewModels.Students
{
    public class StudentDetailForProjectVM
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public string ClassroomName { get; set; }
        public string CourseName { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectPath { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Project { get; set; }
        public Guid ClassroomId { get; set; }

    }
}
