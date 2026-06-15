using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddClassificationToMaterials : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "classification",
                table: "materials",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "classification",
                table: "materials");
        }
    }
}
