using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.RoleDTO
{
    public class GetRoleDTO
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
    }
}
