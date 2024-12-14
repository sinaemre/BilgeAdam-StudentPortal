using ApplicationCore.Consts;
using DTO.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.StudentDTO
{
    public class StudentDetailForProjectDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public double? Exam1 { get; set; }
        public double? Exam2 { get; set; }
        public double? ProjectExam { get; set; }
        public double? Average { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public string? ProjectName { get; set; }
        public string? ProjectPath { get; set; }
        public string? ImagePath { get; set; }
        public Guid ClassroomId { get; set; }
    }
}
