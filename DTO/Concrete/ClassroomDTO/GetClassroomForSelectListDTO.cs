using ApplicationCore.Consts;
using DTO.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Concrete.ClassroomDTO
{
    public class GetClassroomForSelectListDTO : BaseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
