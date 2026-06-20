using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddIncidentManagementSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SafetyRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IncidentType = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    OperatorId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    AssetId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    RiskLevel = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SafetyRecord", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SafetyRecord");
        }
    }
}
