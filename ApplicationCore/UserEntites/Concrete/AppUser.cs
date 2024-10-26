using ApplicationCore.Consts;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.UserEntites.Concrete
{
    public class AppUser : IdentityUser<Guid>
    {
        private DateTime _createdDate = DateTime.UtcNow;
        private Status _status = Status.Active;

        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }
     
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get => _status; set => _status = value; }
    }
}
