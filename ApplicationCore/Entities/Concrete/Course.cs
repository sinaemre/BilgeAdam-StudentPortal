using ApplicationCore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Concrete
{
    public class Course : BaseEntity
    {
        public Course()
        {
            Teachers = new List<Teacher>();
        }

        [Required]
        [MaxLength(30)]
        [MinLength(2)]
        public string Name { get; set; }

        public List<Teacher> Teachers { get; set; }

        public int? TotalHour { get; set; }
    }
}
