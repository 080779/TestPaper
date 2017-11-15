using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.DTO.DTO
{
    public class ActivityImgDTO : BaseDTO
    {
        public string Description { get; set; }
        public string ImgUrl { get; set; }
        public long ActivityId { get; set; }
        public string ActivityName { get; set; }
    }
}
