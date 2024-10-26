using ApplicationCore.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Concrete
{
    public class CustomerManager : BasePerson
    {
        [Required]
        [Column(TypeName = "date")]
        public DateTime HireDate { get; set; }
    }
}
