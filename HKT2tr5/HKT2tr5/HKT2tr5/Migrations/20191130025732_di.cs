using Microsoft.EntityFrameworkCore.Migrations;

namespace HKT2tr5.Migrations
{
    public partial class di : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Xe",
                newName: "ImageThanXe");

            migrationBuilder.AddColumn<string>(
                name: "ImageDauXe",
                table: "Xe",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageDuoiXe",
                table: "Xe",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageNoiThatXe",
                table: "Xe",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageDauXe",
                table: "Xe");

            migrationBuilder.DropColumn(
                name: "ImageDuoiXe",
                table: "Xe");

            migrationBuilder.DropColumn(
                name: "ImageNoiThatXe",
                table: "Xe");

            migrationBuilder.RenameColumn(
                name: "ImageThanXe",
                table: "Xe",
                newName: "Image");
        }
    }
}
