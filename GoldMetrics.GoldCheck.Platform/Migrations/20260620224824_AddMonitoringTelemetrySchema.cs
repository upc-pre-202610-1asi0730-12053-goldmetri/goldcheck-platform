using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace GoldMetrics.GoldCheck.Platform.Migrations
{
    /// <inheritdoc />
    public partial class AddMonitoringTelemetrySchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_UserSubscription_UserSubscriptionId",
                table: "Invoice");

            migrationBuilder.DropPrimaryKey(
                name: "PK_telemetry_data",
                table: "telemetry_data");

            migrationBuilder.DropPrimaryKey(
                name: "PK_gnss_statuses",
                table: "gnss_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSubscription",
                table: "UserSubscription");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpeedReading",
                table: "SpeedReading");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SafetyRecord",
                table: "SafetyRecord");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PressureReading",
                table: "PressureReading");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_materials",
                table: "materials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Material",
                table: "Material");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Machinery",
                table: "Machinery");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice");

            migrationBuilder.RenameTable(
                name: "UserSubscription",
                newName: "user_subscriptions");

            migrationBuilder.RenameTable(
                name: "SpeedReading",
                newName: "speed_readings");

            migrationBuilder.RenameTable(
                name: "SafetyRecord",
                newName: "safety_records");

            migrationBuilder.RenameTable(
                name: "PressureReading",
                newName: "pressure_readings");

            migrationBuilder.RenameTable(
                name: "materials",
                newName: "material_operations_materials");

            migrationBuilder.RenameTable(
                name: "Material",
                newName: "analytics_materials");

            migrationBuilder.RenameTable(
                name: "Machinery",
                newName: "machineries");

            migrationBuilder.RenameTable(
                name: "Invoice",
                newName: "invoices");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "telemetry_data",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "telemetry_data",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "telemetry_data",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "TelemetryDataId",
                table: "telemetry_data",
                newName: "telemetry_data_id");

            migrationBuilder.Sql("ALTER TABLE `telemetry_data` CHANGE `RawData` `raw_data` longtext NOT NULL;");

            migrationBuilder.RenameColumn(
                name: "IsValidated",
                table: "telemetry_data",
                newName: "is_validated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "telemetry_data",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "telemetry_data",
                newName: "asset_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "gnss_statuses",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "gnss_statuses",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "gnss_statuses",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RestartReason",
                table: "gnss_statuses",
                newName: "restart_reason");

            migrationBuilder.RenameColumn(
                name: "RestartCount",
                table: "gnss_statuses",
                newName: "restart_count");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "gnss_statuses",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "gnss_statuses",
                newName: "asset_id");

            migrationBuilder.Sql("ALTER TABLE `user_subscriptions` CHANGE `Status` `status` longtext NOT NULL;");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "user_subscriptions",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "user_subscriptions",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "RequestedDowngradePlan",
                table: "user_subscriptions",
                newName: "requested_downgrade_plan");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "user_subscriptions",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AccessGranted",
                table: "user_subscriptions",
                newName: "access_granted");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "speed_readings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "speed_readings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "speed_readings",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "speed_readings",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "speed_readings",
                newName: "asset_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "safety_records",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "safety_records",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "safety_records",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "safety_records",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "safety_records",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "RiskLevel",
                table: "safety_records",
                newName: "risk_level");

            migrationBuilder.RenameColumn(
                name: "OperatorId",
                table: "safety_records",
                newName: "operator_id");

            migrationBuilder.RenameColumn(
                name: "IncidentType",
                table: "safety_records",
                newName: "incident_type");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "safety_records",
                newName: "asset_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "pressure_readings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "pressure_readings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "pressure_readings",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "OilPanBar",
                table: "pressure_readings",
                newName: "oil_pan_bar");

            migrationBuilder.RenameColumn(
                name: "OilFilterDifferenceBar",
                table: "pressure_readings",
                newName: "oil_filter_difference_bar");

            migrationBuilder.RenameColumn(
                name: "OilFilterBar",
                table: "pressure_readings",
                newName: "oil_filter_bar");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "pressure_readings",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "AnomalyPressureType",
                table: "pressure_readings",
                newName: "anomaly_pressure_type");

            migrationBuilder.RenameColumn(
                name: "AnomalyDescription",
                table: "pressure_readings",
                newName: "anomaly_description");

            migrationBuilder.RenameColumn(
                name: "AbsoluteEngineOilBar",
                table: "pressure_readings",
                newName: "absolute_engine_oil_bar");

            migrationBuilder.RenameColumn(
                name: "AssetId",
                table: "pressure_readings",
                newName: "asset_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "analytics_materials",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "analytics_materials",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "analytics_materials",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "ProductionTons",
                table: "analytics_materials",
                newName: "production_tons");

            migrationBuilder.RenameColumn(
                name: "ProductionStart",
                table: "analytics_materials",
                newName: "production_start");

            migrationBuilder.RenameColumn(
                name: "ProductionEnd",
                table: "analytics_materials",
                newName: "production_end");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "analytics_materials",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "analytics_materials",
                newName: "user_id");

            migrationBuilder.RenameColumn(
                name: "SupervisorId",
                table: "analytics_materials",
                newName: "supervisor_id");

            migrationBuilder.RenameColumn(
                name: "RouteStatus",
                table: "analytics_materials",
                newName: "route_status");

            migrationBuilder.RenameColumn(
                name: "RouteId",
                table: "analytics_materials",
                newName: "route_id");

            migrationBuilder.RenameColumn(
                name: "MaterialId",
                table: "analytics_materials",
                newName: "material_id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "machineries",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "machineries",
                newName: "model");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "machineries",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "machineries",
                newName: "updated_at");

            migrationBuilder.RenameColumn(
                name: "OEM",
                table: "machineries",
                newName: "o_e_m");

            migrationBuilder.RenameColumn(
                name: "MaintenanceScheduledAtHours",
                table: "machineries",
                newName: "maintenance_scheduled_at_hours");

            migrationBuilder.RenameColumn(
                name: "DischargedComponentId",
                table: "machineries",
                newName: "discharged_component_id");

            migrationBuilder.RenameColumn(
                name: "DischargeReason",
                table: "machineries",
                newName: "discharge_reason");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "machineries",
                newName: "created_at");

            migrationBuilder.RenameColumn(
                name: "ComponentDischargeReason",
                table: "machineries",
                newName: "component_discharge_reason");

            migrationBuilder.RenameColumn(
                name: "MaintenanceStatus",
                table: "machineries",
                newName: "maintenance_status");

            migrationBuilder.RenameColumn(
                name: "MachineryId",
                table: "machineries",
                newName: "machinery_id");

            migrationBuilder.RenameColumn(
                name: "EngineHours",
                table: "machineries",
                newName: "engine_hours");

            migrationBuilder.Sql("ALTER TABLE `invoices` CHANGE `Status` `status` longtext NOT NULL;");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "invoices",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserSubscriptionId",
                table: "invoices",
                newName: "user_subscription_id");

            migrationBuilder.RenameIndex(
                name: "IX_Invoice_UserSubscriptionId",
                table: "invoices",
                newName: "i_x_invoices_user_subscription_id");

            migrationBuilder.AddColumn<decimal>(
                name: "current_speed_km_per_hour",
                table: "speed_readings",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_violation",
                table: "speed_readings",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "speed_limit_km_per_hour",
                table: "speed_readings",
                type: "decimal(8,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "violation_description",
                table: "speed_readings",
                type: "varchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "p_k_telemetry_data",
                table: "telemetry_data",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_gnss_statuses",
                table: "gnss_statuses",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_user_subscriptions",
                table: "user_subscriptions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_speed_readings",
                table: "speed_readings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_safety_records",
                table: "safety_records",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_pressure_readings",
                table: "pressure_readings",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_material_operations_materials",
                table: "material_operations_materials",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_analytics_materials",
                table: "analytics_materials",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_machineries",
                table: "machineries",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_invoices",
                table: "invoices",
                column: "id");

            migrationBuilder.CreateTable(
                name: "communication_channels",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    asset_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    last_analysed_protocol = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    anomaly_protocol = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    anomaly_description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_communication_channels", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "temperature_readings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    asset_id = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    status = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    exhaust_celsius = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    exhaust_limit_celsius = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    cylinder_number = table.Column<int>(type: "int", nullable: true),
                    refrigerant_celsius = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    oil_celsius = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    fuel_celsius = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    anomaly_type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    anomaly_celsius = table.Column<decimal>(type: "decimal(8,2)", nullable: true),
                    anomaly_description = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("p_k_temperature_readings", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "f_k_invoices_user_subscriptions_user_subscription_id",
                table: "invoices",
                column: "user_subscription_id",
                principalTable: "user_subscriptions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "f_k_invoices_user_subscriptions_user_subscription_id",
                table: "invoices");

            migrationBuilder.DropTable(
                name: "communication_channels");

            migrationBuilder.DropTable(
                name: "temperature_readings");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_telemetry_data",
                table: "telemetry_data");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_gnss_statuses",
                table: "gnss_statuses");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_user_subscriptions",
                table: "user_subscriptions");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_speed_readings",
                table: "speed_readings");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_safety_records",
                table: "safety_records");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_pressure_readings",
                table: "pressure_readings");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_material_operations_materials",
                table: "material_operations_materials");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_machineries",
                table: "machineries");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_invoices",
                table: "invoices");

            migrationBuilder.DropPrimaryKey(
                name: "p_k_analytics_materials",
                table: "analytics_materials");

            migrationBuilder.DropColumn(
                name: "current_speed_km_per_hour",
                table: "speed_readings");

            migrationBuilder.DropColumn(
                name: "is_violation",
                table: "speed_readings");

            migrationBuilder.DropColumn(
                name: "speed_limit_km_per_hour",
                table: "speed_readings");

            migrationBuilder.DropColumn(
                name: "violation_description",
                table: "speed_readings");

            migrationBuilder.RenameTable(
                name: "user_subscriptions",
                newName: "UserSubscription");

            migrationBuilder.RenameTable(
                name: "speed_readings",
                newName: "SpeedReading");

            migrationBuilder.RenameTable(
                name: "safety_records",
                newName: "SafetyRecord");

            migrationBuilder.RenameTable(
                name: "pressure_readings",
                newName: "PressureReading");

            migrationBuilder.RenameTable(
                name: "material_operations_materials",
                newName: "materials");

            migrationBuilder.RenameTable(
                name: "machineries",
                newName: "Machinery");

            migrationBuilder.RenameTable(
                name: "invoices",
                newName: "Invoice");

            migrationBuilder.RenameTable(
                name: "analytics_materials",
                newName: "Material");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "telemetry_data",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "telemetry_data",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "telemetry_data",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "telemetry_data_id",
                table: "telemetry_data",
                newName: "TelemetryDataId");

            migrationBuilder.Sql("ALTER TABLE `telemetry_data` CHANGE `raw_data` `RawData` longtext NOT NULL;");

            migrationBuilder.RenameColumn(
                name: "is_validated",
                table: "telemetry_data",
                newName: "IsValidated");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "telemetry_data",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "asset_id",
                table: "telemetry_data",
                newName: "AssetId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "gnss_statuses",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "gnss_statuses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "gnss_statuses",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "restart_reason",
                table: "gnss_statuses",
                newName: "RestartReason");

            migrationBuilder.RenameColumn(
                name: "restart_count",
                table: "gnss_statuses",
                newName: "RestartCount");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "gnss_statuses",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "asset_id",
                table: "gnss_statuses",
                newName: "AssetId");

            migrationBuilder.Sql("ALTER TABLE `UserSubscription` CHANGE `status` `Status` longtext NOT NULL;");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "UserSubscription",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "UserSubscription",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "requested_downgrade_plan",
                table: "UserSubscription",
                newName: "RequestedDowngradePlan");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "UserSubscription",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "access_granted",
                table: "UserSubscription",
                newName: "AccessGranted");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "SpeedReading",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SpeedReading",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "SpeedReading",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "SpeedReading",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "asset_id",
                table: "SpeedReading",
                newName: "AssetId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "SafetyRecord",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "SafetyRecord",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "SafetyRecord",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "SafetyRecord",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "SafetyRecord",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "risk_level",
                table: "SafetyRecord",
                newName: "RiskLevel");

            migrationBuilder.RenameColumn(
                name: "operator_id",
                table: "SafetyRecord",
                newName: "OperatorId");

            migrationBuilder.RenameColumn(
                name: "incident_type",
                table: "SafetyRecord",
                newName: "IncidentType");

            migrationBuilder.RenameColumn(
                name: "asset_id",
                table: "SafetyRecord",
                newName: "AssetId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "PressureReading",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PressureReading",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "PressureReading",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "oil_pan_bar",
                table: "PressureReading",
                newName: "OilPanBar");

            migrationBuilder.RenameColumn(
                name: "oil_filter_difference_bar",
                table: "PressureReading",
                newName: "OilFilterDifferenceBar");

            migrationBuilder.RenameColumn(
                name: "oil_filter_bar",
                table: "PressureReading",
                newName: "OilFilterBar");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "PressureReading",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "anomaly_pressure_type",
                table: "PressureReading",
                newName: "AnomalyPressureType");

            migrationBuilder.RenameColumn(
                name: "anomaly_description",
                table: "PressureReading",
                newName: "AnomalyDescription");

            migrationBuilder.RenameColumn(
                name: "absolute_engine_oil_bar",
                table: "PressureReading",
                newName: "AbsoluteEngineOilBar");

            migrationBuilder.RenameColumn(
                name: "asset_id",
                table: "PressureReading",
                newName: "AssetId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Machinery",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "Machinery",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Machinery",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Machinery",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "o_e_m",
                table: "Machinery",
                newName: "OEM");

            migrationBuilder.RenameColumn(
                name: "maintenance_scheduled_at_hours",
                table: "Machinery",
                newName: "MaintenanceScheduledAtHours");

            migrationBuilder.RenameColumn(
                name: "discharged_component_id",
                table: "Machinery",
                newName: "DischargedComponentId");

            migrationBuilder.RenameColumn(
                name: "discharge_reason",
                table: "Machinery",
                newName: "DischargeReason");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Machinery",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "component_discharge_reason",
                table: "Machinery",
                newName: "ComponentDischargeReason");

            migrationBuilder.RenameColumn(
                name: "maintenance_status",
                table: "Machinery",
                newName: "MaintenanceStatus");

            migrationBuilder.RenameColumn(
                name: "machinery_id",
                table: "Machinery",
                newName: "MachineryId");

            migrationBuilder.RenameColumn(
                name: "engine_hours",
                table: "Machinery",
                newName: "EngineHours");

            migrationBuilder.Sql("ALTER TABLE `Invoice` CHANGE `status` `Status` longtext NOT NULL;");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Invoice",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_subscription_id",
                table: "Invoice",
                newName: "UserSubscriptionId");

            migrationBuilder.RenameIndex(
                name: "i_x_invoices_user_subscription_id",
                table: "Invoice",
                newName: "IX_Invoice_UserSubscriptionId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Material",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Material",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "updated_at",
                table: "Material",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "production_tons",
                table: "Material",
                newName: "ProductionTons");

            migrationBuilder.RenameColumn(
                name: "production_start",
                table: "Material",
                newName: "ProductionStart");

            migrationBuilder.RenameColumn(
                name: "production_end",
                table: "Material",
                newName: "ProductionEnd");

            migrationBuilder.RenameColumn(
                name: "created_at",
                table: "Material",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "Material",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "supervisor_id",
                table: "Material",
                newName: "SupervisorId");

            migrationBuilder.RenameColumn(
                name: "route_status",
                table: "Material",
                newName: "RouteStatus");

            migrationBuilder.RenameColumn(
                name: "route_id",
                table: "Material",
                newName: "RouteId");

            migrationBuilder.RenameColumn(
                name: "material_id",
                table: "Material",
                newName: "MaterialId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_telemetry_data",
                table: "telemetry_data",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_gnss_statuses",
                table: "gnss_statuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSubscription",
                table: "UserSubscription",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpeedReading",
                table: "SpeedReading",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SafetyRecord",
                table: "SafetyRecord",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PressureReading",
                table: "PressureReading",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "p_k_materials",
                table: "materials",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Machinery",
                table: "Machinery",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Invoice",
                table: "Invoice",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Material",
                table: "Material",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_UserSubscription_UserSubscriptionId",
                table: "Invoice",
                column: "UserSubscriptionId",
                principalTable: "UserSubscription",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
