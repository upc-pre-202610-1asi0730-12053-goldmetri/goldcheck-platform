using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddReportFormatToReports : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "downloaded_by_user_id",
                table: "reports",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "report_format",
                table: "reports",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "downloaded_by_user_id",
                table: "reports");

            migrationBuilder.DropColumn(
                name: "report_format",
                table: "reports");
        }
    }
}
