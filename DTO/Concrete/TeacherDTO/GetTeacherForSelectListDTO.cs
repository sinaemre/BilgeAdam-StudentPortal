using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.TeacherDTO
{
    public class GetTeacherForSelectListDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
    }
}
