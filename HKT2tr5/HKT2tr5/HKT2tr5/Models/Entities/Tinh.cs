using System.Collections.Generic;

namespace HKT2tr5.Models.Entities
{
    public class Tinh
    {
        public int TinhId { get; set; }
        public string TenTinh { get; set; }
        public List<Xe> Xe { get; set; }
    }
}
