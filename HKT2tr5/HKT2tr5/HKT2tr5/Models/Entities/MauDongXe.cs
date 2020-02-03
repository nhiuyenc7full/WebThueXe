namespace HKT2tr5.Models.Entities
{
    public class MauDongXe
    {
        public int MauDongXeId { get; set; }
        public MauXe MauXe { get; set; }
        public int MauXeId { get; set; }
        public int DongXeId { get; set; }
        public DongXe DongXe { get; set; }
    }
}
