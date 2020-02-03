using System.Collections.Generic;

namespace HKT2tr5.Models.Entities
{
    public class NhaSanXuat
    {
        public int NhaSanXuatId { get; set; }
        public string TenNSX { get; set; }
        public List<DongXe> DongXe { get; set; }
    }
}
