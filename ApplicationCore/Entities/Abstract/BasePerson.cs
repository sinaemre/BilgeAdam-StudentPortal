using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Abstract
{
    public abstract class BasePerson : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        [MinLength(2)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(150)]
        [MinLength(2)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
    }
}
