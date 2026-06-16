using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddQRCodeToJewelryMaterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "q_r_code",
                table: "jewelry_materials",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "q_r_code",
                table: "jewelry_materials");
        }
    }
}
