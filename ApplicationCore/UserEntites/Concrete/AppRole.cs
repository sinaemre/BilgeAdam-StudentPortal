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
    public class AppRole : IdentityRole<Guid>
    {
        private DateTime _createdDate = DateTime.Now;
        private Status _status = Status.Active;

        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get => _status; set => _status = value; }
    }
}
