using ApplicationCore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Concrete
{
    public class Teacher : BasePerson
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }

        [Required]
        public Guid CourseId { get; set; }
        public Course Course { get; set; }
    }
}
