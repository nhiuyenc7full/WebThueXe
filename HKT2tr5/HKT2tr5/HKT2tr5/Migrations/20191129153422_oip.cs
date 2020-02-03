using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HKT2tr5.Migrations
{
    public partial class oip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Banner",
                columns: table => new
                {
                    BannerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenBanner = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banner", x => x.BannerId);
                });

            migrationBuilder.CreateTable(
                name: "LoaiXe",
                columns: table => new
                {
                    LoaiXeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenLoaiXe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiXe", x => x.LoaiXeId);
                });

            migrationBuilder.CreateTable(
                name: "MauXe",
                columns: table => new
                {
                    MauXeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenMauXe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauXe", x => x.MauXeId);
                });

            migrationBuilder.CreateTable(
                name: "NhaSanXuat",
                columns: table => new
                {
                    NhaSanXuatId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenNSX = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaSanXuat", x => x.NhaSanXuatId);
                });

            migrationBuilder.CreateTable(
                name: "Tinh",
                columns: table => new
                {
                    TinhId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenTinh = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tinh", x => x.TinhId);
                });

            migrationBuilder.CreateTable(
                name: "DongXe",
                columns: table => new
                {
                    DongXeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TenDongXe = table.Column<string>(nullable: true),
                    NhaSanXuatId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DongXe", x => x.DongXeId);
                    table.ForeignKey(
                        name: "FK_DongXe_NhaSanXuat_NhaSanXuatId",
                        column: x => x.NhaSanXuatId,
                        principalTable: "NhaSanXuat",
                        principalColumn: "NhaSanXuatId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MauDongXe",
                columns: table => new
                {
                    MauDongXeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MauXeId = table.Column<int>(nullable: false),
                    DongXeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MauDongXe", x => x.MauDongXeId);
                    table.ForeignKey(
                        name: "FK_MauDongXe_DongXe_DongXeId",
                        column: x => x.DongXeId,
                        principalTable: "DongXe",
                        principalColumn: "DongXeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MauDongXe_MauXe_MauXeId",
                        column: x => x.MauXeId,
                        principalTable: "MauXe",
                        principalColumn: "MauXeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Xe",
                columns: table => new
                {
                    XeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tittle = table.Column<string>(nullable: true),
                    NamSx = table.Column<int>(nullable: false),
                    GiaTheoGio = table.Column<decimal>(nullable: false),
                    GiaTheoNgay = table.Column<decimal>(nullable: false),
                    TinhId = table.Column<int>(nullable: false),
                    DaThue = table.Column<bool>(nullable: false),
                    DangKinhDoanh = table.Column<bool>(nullable: false),
                    Rate = table.Column<float>(nullable: false),
                    Mau = table.Column<string>(nullable: true),
                    LoaiXeId = table.Column<int>(nullable: false),
                    DongXeId = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Xe", x => x.XeId);
                    table.ForeignKey(
                        name: "FK_Xe_DongXe_DongXeId",
                        column: x => x.DongXeId,
                        principalTable: "DongXe",
                        principalColumn: "DongXeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Xe_LoaiXe_LoaiXeId",
                        column: x => x.LoaiXeId,
                        principalTable: "LoaiXe",
                        principalColumn: "LoaiXeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Xe_Tinh_TinhId",
                        column: x => x.TinhId,
                        principalTable: "Tinh",
                        principalColumn: "TinhId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DongXe_NhaSanXuatId",
                table: "DongXe",
                column: "NhaSanXuatId");

            migrationBuilder.CreateIndex(
                name: "IX_MauDongXe_DongXeId",
                table: "MauDongXe",
                column: "DongXeId");

            migrationBuilder.CreateIndex(
                name: "IX_MauDongXe_MauXeId",
                table: "MauDongXe",
                column: "MauXeId");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_DongXeId",
                table: "Xe",
                column: "DongXeId");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_LoaiXeId",
                table: "Xe",
                column: "LoaiXeId");

            migrationBuilder.CreateIndex(
                name: "IX_Xe_TinhId",
                table: "Xe",
                column: "TinhId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Banner");

            migrationBuilder.DropTable(
                name: "MauDongXe");

            migrationBuilder.DropTable(
                name: "Xe");

            migrationBuilder.DropTable(
                name: "MauXe");

            migrationBuilder.DropTable(
                name: "DongXe");

            migrationBuilder.DropTable(
                name: "LoaiXe");

            migrationBuilder.DropTable(
                name: "Tinh");

            migrationBuilder.DropTable(
                name: "NhaSanXuat");
        }
    }
}
