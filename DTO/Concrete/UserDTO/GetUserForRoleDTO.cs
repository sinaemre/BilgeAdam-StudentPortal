using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.UserDTO
{
    public class GetUserForRoleDTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
