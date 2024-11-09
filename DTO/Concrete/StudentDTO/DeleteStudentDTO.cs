using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.StudentDTO
{
    public class DeleteStudentDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Email { get; set; }
        public string ClassroomId { get; set; }
        public double? Average { get; set; }
        public string Status { get; set; }
        public string StudentStatus { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
    }
}
