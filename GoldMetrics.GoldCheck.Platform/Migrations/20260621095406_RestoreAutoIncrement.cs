using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class RestoreAutoIncrement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE `machineries` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `hauling_cycles` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `material_operations_materials` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `safety_records` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `speed_readings` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `pressure_readings` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `user_subscriptions` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
            migrationBuilder.Sql("ALTER TABLE `invoices` MODIFY `id` INT NOT NULL AUTO_INCREMENT;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
