using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddSpeedReadingSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AbsoluteEngineOilBar",
                table: "PressureReading",
                type: "decimal(8,4)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnomalyDescription",
                table: "PressureReading",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AnomalyPressureType",
                table: "PressureReading",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OilFilterBar",
                table: "PressureReading",
                type: "decimal(8,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OilFilterDifferenceBar",
                table: "PressureReading",
                type: "decimal(8,4)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OilPanBar",
                table: "PressureReading",
                type: "decimal(8,4)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SpeedReading",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    AssetId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeedReading", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeedReading");

            migrationBuilder.DropColumn(
                name: "AbsoluteEngineOilBar",
                table: "PressureReading");

            migrationBuilder.DropColumn(
                name: "AnomalyDescription",
                table: "PressureReading");

            migrationBuilder.DropColumn(
                name: "AnomalyPressureType",
                table: "PressureReading");

            migrationBuilder.DropColumn(
                name: "OilFilterBar",
                table: "PressureReading");

            migrationBuilder.DropColumn(
                name: "OilFilterDifferenceBar",
                table: "PressureReading");

            migrationBuilder.DropColumn(
                name: "OilPanBar",
                table: "PressureReading");
        }
    }
}
