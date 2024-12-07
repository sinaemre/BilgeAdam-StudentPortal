using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.AccountDTO
{
    public class ResetPasswordDTO
    {
        public string Token { get; set; }
        public string Email { get; set; }
        public string NewPassword { get; set; }
    }
}
