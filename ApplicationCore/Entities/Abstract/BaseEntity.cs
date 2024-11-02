using ApplicationCore.Consts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities.Abstract
{
    public abstract class BaseEntity
    {
        private DateTime _createdDate = DateTime.Now;
        private Status _status = Status.Active;

        public Guid Id { get; set; }
        public DateTime CreatedDate { get => _createdDate; set => _createdDate = value; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Status Status { get => _status; set => _status = value; }
    }
}
