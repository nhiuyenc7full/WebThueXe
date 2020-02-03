using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.TagHelpers;

namespace HKT2tr5.Models.Entities
{
    public class Xe
    {
        public int XeId { get; set; }
        public string Tittle { get; set; }
        public int NamSx { get; set; }
        public decimal GiaTheoGio { get; set; }
        public decimal GiaTheoNgay { get; set; }
        public Tinh Tinh { get; set; }
        public int TinhId { get; set; }
        public bool DaThue { get; set; }
        public bool DangKinhDoanh { get; set; }
        public float Rate { get; set; }
        public string Mau { get; set; }
        public LoaiXe LoaiXe { get; set; }
        public int LoaiXeId { get; set; }
        public DongXe DongXe { get; set; }
        public int DongXeId { get; set; }
        //ảnh
        public string ImageDauXe { get; set; }
        public string ImageDuoiXe { get; set; }
        public string ImageThanXe { get; set; }
        public string ImageNoiThatXe { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
