using DTO.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.StudentDTO
{
    public class CreateStudentDTO : BaseDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Guid? ClassroomId { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? Image { get; set; }
        public double? RegisterPrice { get; set; }
    }
}
