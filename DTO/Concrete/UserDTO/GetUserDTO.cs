using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.UserDTO
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public string? FirstPassword { get; set; }
        public bool HasPasswordChanged { get; set; }
    }
}
