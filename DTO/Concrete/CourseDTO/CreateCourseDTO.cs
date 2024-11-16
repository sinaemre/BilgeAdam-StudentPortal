using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.CourseDTO
{
    public class CreateCourseDTO : BaseDTO
    {
        public string Name { get; set; }
        public int? TotalHour { get; set; }
    }
}
