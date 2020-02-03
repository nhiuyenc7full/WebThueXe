using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Http;

namespace HKT2tr5.Models
{
    public class XeViewModel
    {
        // public int XeId { get; set; }
        public string Tittle { get; set; }
        public int NamSx { get; set; }
        public decimal GiaTheoGio { get; set; }
        public decimal GiaTheoNgay { get; set; }
        public int TinhId { get; set; }
        public bool DaThue { get; set; }
        public bool DangKinhDoanh { get; set; }
        //public float Rate { get; set; }
        public int MauXeId { get; set; }
        public int LoaiXeId { get; set; }
        public int DongXeId { get; set; }
        public int NhaSanXuatId { get; set; }
        public NhaSanXuat NhaSanXuat { get; set; }
        public IFormFile ImageDauXe { get; set; }
        public IFormFile ImageDuoiXe { get; set; }
        public IFormFile ImageThanXe { get; set; }
        public IFormFile ImageNoiThatXe { get; set; }
    }
}
