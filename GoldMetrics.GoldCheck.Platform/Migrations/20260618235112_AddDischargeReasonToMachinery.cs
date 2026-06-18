using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddDischargeReasonToMachinery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DischargeReason",
                table: "Machinery",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "MaintenanceScheduledAtHours",
                table: "Machinery",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DischargeReason",
                table: "Machinery");

            migrationBuilder.DropColumn(
                name: "MaintenanceScheduledAtHours",
                table: "Machinery");
        }
    }
}
