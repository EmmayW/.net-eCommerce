using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MedGearMart.Migrations
{
    public partial class updategearimageurl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "imageUrl",
                table: "Gears",
                newName: "ImageUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Gears",
                newName: "imageUrl");
        }
    }
}
