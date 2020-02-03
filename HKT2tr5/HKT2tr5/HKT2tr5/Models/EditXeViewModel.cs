using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Models
{
    public class EditXeViewModel : XeViewModel
    {
        public int XeId { get; set; }
        [Display(Name = "Ảnh")]
        
        public string ImageDauXe1 { get; set; }
        public string ImageDuoiXe1 { get; set; }
        public string ImageThanXe1 { get; set; }
        public string ImageNoiThatXe1 { get; set; }
        [Range(0, 5)]
        public float Rate { get; set; }
    }
}
