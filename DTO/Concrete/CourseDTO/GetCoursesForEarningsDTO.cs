using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.CourseDTO
{
    public class GetCoursesForEarningsDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? TotalEarning { get; set; }
    }
}
