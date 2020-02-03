using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Data
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Xe> Xe { get; set; }
        public DbSet<DongXe> DongXe { get; set; }
        public DbSet<NhaSanXuat> NhaSanXuat { get; set; }
        public DbSet<LoaiXe> LoaiXe { get; set; }
        public DbSet<MauXe> MauXe { get; set; }
        public DbSet<Tinh> Tinh { get; set; }
        public DbSet<Banner> Banner { get; set; }
        public DbSet<MauDongXe> MauDongXe { get; set; }
    }
}
