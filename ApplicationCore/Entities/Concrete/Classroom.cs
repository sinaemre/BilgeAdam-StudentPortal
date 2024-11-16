using ApplicationCore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Concrete
{
    public class Classroom : BaseEntity
    {
        public Classroom()
        {
            Students = new List<Student>();
        }

        [Required]
        [MaxLength(100)]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MaxLength(200)]
        [MinLength(3)]
        public string Description { get; set; }

        [Required]
        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

        public List<Student> Students { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
