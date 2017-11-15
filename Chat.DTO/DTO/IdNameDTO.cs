using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class IdNameDTO:BaseDTO
    {
        public string Name { get; set; }
        public string TypeName { get; set; }
        public string ImgUrl { get; set; }
    }
}
