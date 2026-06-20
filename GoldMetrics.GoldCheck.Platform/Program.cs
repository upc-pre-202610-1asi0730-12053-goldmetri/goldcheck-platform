using Cortex.Mediator.Commands;
using Cortex.Mediator.DependencyInjection;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Interfaces.AspNetCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Mediator.Cortex.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Pipeline.Middleware.Extensions;
using GoldMetrics.GoldCheck.Platform.Shared.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.OpenApi.Models;
using ProblemDetailsFactory = GoldMetrics.GoldCheck.Platform.Shared.Interfaces.Rest.ProblemDetails.ProblemDetailsFactory;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Resources;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Resources;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Resources;
using JewelryCommandService = GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Internal.CommandServices.JewelryCommandService;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.Analytics.Resources;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Application.QueryServices;
using MaterialRepository = GoldMetrics.GoldCheck.Platform.MaterialOperations.Infrastructure.Persistence.EntityFrameworkCore.Repositories.MaterialRepository;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Resources;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Resources;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Transform;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Resources;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Resources;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Resources;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.Internal.CommandServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Resources;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddControllers(options => options.Conventions.Add(new KebabCaseRouteNamingConvention()))
    .AddDataAnnotationsLocalization();

builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy",
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var connectionStringTemplate = builder.Configuration.GetConnectionString("DefaultConnection");
    if (string.IsNullOrWhiteSpace(connectionStringTemplate))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    var connectionString = Environment.ExpandEnvironmentVariables(connectionStringTemplate);
    if (string.IsNullOrWhiteSpace(connectionString))
        throw new InvalidOperationException("Database connection string is not set in the configuration.");

    options.UseMySQL(connectionString)
        .UseLoggerFactory(serviceProvider.GetRequiredService<ILoggerFactory>())
        .EnableDetailedErrors();

    if (builder.Environment.IsDevelopment())
        options.EnableSensitiveDataLogging();
});

builder.Services.AddLocalization(options => options.ResourcesPath = "Shared/Resources");

builder.Services.AddSingleton<IStringLocalizer<ErrorMessages>, StringLocalizer<ErrorMessages>>();
builder.Services.AddSingleton<IStringLocalizer<CommonMessages>, StringLocalizer<CommonMessages>>();

builder.Services.AddSingleton<ProblemDetailsFactory>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GoldMetrics.GoldCheck.Platform",
        Version = "v1",
        Description = "GoldCheck Mineral Traceability Platform API",
        TermsOfService = new Uri("https://goldmetrics.com/tos"),
        Contact = new OpenApiContact
        {
            Name = "GoldMetrics",
            Email = "contact@goldmetrics.com"
        },
        License = new OpenApiLicense
        {
            Name = "Apache 2.0",
            Url = new Uri("https://www.apache.org/licenses/LICENSE-2.0.html")
        }
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    options.EnableAnnotations();
    options.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

// Dependency Injection

// Shared Bounded Context
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Iam Bounded Context
builder.Services.AddSingleton<IStringLocalizer<GoldMetrics.GoldCheck.Platform.Iam.Resources.IamMessages>, StringLocalizer<GoldMetrics.GoldCheck.Platform.Iam.Resources.IamMessages>>();
builder.Services.AddScoped<GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories.IUserRepository, GoldMetrics.GoldCheck.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories.UserRepository>();
builder.Services.AddScoped<GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices.IIamCommandService, GoldMetrics.GoldCheck.Platform.Iam.Application.Internal.CommandServices.IamCommandService>();
builder.Services.AddScoped<GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Transform.IamActionResultAssembler>();
builder.Services.AddScoped<GoldMetrics.GoldCheck.Platform.Iam.Application.QueryServices.IIamQueryService, GoldMetrics.GoldCheck.Platform.Iam.Application.Internal.QueryServices.IamQueryService>();

// MaterialOperations Bounded Context
builder.Services.AddSingleton<IStringLocalizer<MaterialOperationsMessages>, StringLocalizer<MaterialOperationsMessages>>();
builder.Services.AddScoped<GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Repositories.IMaterialRepository, MaterialRepository>();
builder.Services.AddScoped<IMaterialCommandService, MaterialCommandService>();
builder.Services.AddScoped<IMaterialQueryService, MaterialQueryService>();

// JewelryInventory Bounded Context
builder.Services.AddSingleton<IStringLocalizer<JewelryInventoryMessages>, StringLocalizer<JewelryInventoryMessages>>();
builder.Services.AddScoped<IJewelryMaterialRepository, JewelryMaterialRepository>();
builder.Services.AddScoped<IJewelryMaterialCommandService, JewelryMaterialCommandService>();
builder.Services.AddScoped<IJewelryMaterialQueryService, JewelryMaterialQueryService>();
builder.Services.AddScoped<IJewelryRepository, JewelryRepository>();
builder.Services.AddScoped<IJewelryCommandService, JewelryCommandService>();
builder.Services.AddScoped<IJewelryQueryService, JewelryQueryService>();

// ReportingNotifications Bounded Context
builder.Services.AddSingleton<IStringLocalizer<ReportingNotificationsMessages>, StringLocalizer<ReportingNotificationsMessages>>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IReportCommandService, ReportCommandService>();
builder.Services.AddScoped<IReportQueryService, ReportQueryService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<INotificationCommandService, NotificationCommandService>();
builder.Services.AddScoped<INotificationQueryService, NotificationQueryService>();

// Analytics Bounded Context
builder.Services.AddSingleton<IStringLocalizer<AnalyticsMessages>, StringLocalizer<AnalyticsMessages>>();
builder.Services.AddScoped<
    GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories.IMaterialRepository,
    GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories.MaterialRepository>();
builder.Services.AddScoped<IAnalyticsCommandService, AnalyticsCommandService>();
builder.Services.AddScoped<IAnalyticsQueryService, AnalyticsQueryService>();

// AssetMaintenance Bounded Context
builder.Services.AddSingleton<IStringLocalizer<AssetMaintenanceMessages>, StringLocalizer<AssetMaintenanceMessages>>();
builder.Services.AddScoped<IMachineryRepository, MachineryRepository>();
builder.Services.AddScoped<IAssetMaintenanceCommandService, AssetMaintenanceCommandService>();
builder.Services.AddScoped<IAssetMaintenanceQueryService, AssetMaintenanceQueryService>();

// FleetOperations Bounded Context
builder.Services.AddSingleton<IStringLocalizer<FleetOperationsMessages>, StringLocalizer<FleetOperationsMessages>>();
builder.Services.AddScoped<IVehicleRepository, VehicleRepository>();
builder.Services.AddScoped<IVehicleCommandService, VehicleCommandService>();
builder.Services.AddScoped<IVehicleQueryService, VehicleQueryService>();
builder.Services.AddScoped<IHaulingCycleRepository, HaulingCycleRepository>();
builder.Services.AddScoped<IHaulingCycleCommandService, HaulingCycleCommandService>();
builder.Services.AddScoped<IHaulingCycleQueryService, HaulingCycleQueryService>();

// ConsumerTraceability Bounded Context
builder.Services.AddSingleton<IStringLocalizer<ConsumerTraceabilityMessages>, StringLocalizer<ConsumerTraceabilityMessages>>();
builder.Services.AddScoped<IJewelryProductRepository, JewelryProductRepository>();
builder.Services.AddScoped<ITraceabilityJourneyRepository, TraceabilityJourneyRepository>();
builder.Services.AddScoped<IJewelryProductCommandService, JewelryProductCommandService>();
builder.Services.AddScoped<IJewelryProductQueryService, JewelryProductQueryService>();
builder.Services.AddScoped<ITraceabilityJourneyCommandService, TraceabilityJourneyCommandService>();
builder.Services.AddScoped<ITraceabilityJourneyQueryService, TraceabilityJourneyQueryService>();


builder.Services.AddScoped<IJewelryProductQueryService, JewelryProductQueryService>();

// Mediator Configuration
builder.Services.AddScoped(typeof(ICommandPipelineBehavior<>), typeof(LoggingCommandBehavior<>));
builder.Services.AddCortexMediator([typeof(Program)]);

// SubscriptionsAndBilling Bounded Context
builder.Services.AddSingleton<IStringLocalizer<SubscriptionsBillingMessages>, StringLocalizer<SubscriptionsBillingMessages>>();
builder.Services.AddScoped<IUserSubscriptionRepository, UserSubscriptionRepository>();
builder.Services.AddScoped<ISubscriptionsBillingCommandService, SubscriptionsBillingCommandService>();
builder.Services.AddScoped<SubscriptionsBillingActionResultAssembler>();
builder.Services.AddScoped<ISubscriptionsBillingQueryService, SubscriptionsBillingQueryService>();

// IncidentManagement Bounded Context
builder.Services.AddSingleton<IStringLocalizer<IncidentManagementMessages>, StringLocalizer<IncidentManagementMessages>>();
builder.Services.AddScoped<ISafetyRecordRepository, SafetyRecordRepository>();
builder.Services.AddScoped<IIncidentManagementCommandService, IncidentManagementCommandService>();
builder.Services.AddScoped<IIncidentManagementQueryService, IncidentManagementQueryService>();

// MonitoringTelemetry Bounded Context
builder.Services.AddSingleton<IStringLocalizer<MonitoringTelemetryMessages>, StringLocalizer<MonitoringTelemetryMessages>>();
builder.Services.AddScoped<ITelemetryDataRepository, TelemetryDataRepository>();
builder.Services.AddScoped<ITelemetryCommandService, TelemetryCommandService>();
builder.Services.AddScoped<ITelemetryQueryService, TelemetryQueryService>();
builder.Services.AddScoped<IGNSSStatusRepository, GNSSStatusRepository>();
builder.Services.AddScoped<IGNSSCommandService, GNSSCommandService>();
builder.Services.AddScoped<IGNSSQueryService, GNSSQueryService>();
builder.Services.AddScoped<IPressureReadingRepository, PressureReadingRepository>();
builder.Services.AddScoped<IPressureCommandService, PressureCommandService>();
builder.Services.AddScoped<IPressureQueryService, PressureQueryService>();
builder.Services.AddScoped<ISpeedReadingRepository, SpeedReadingRepository>();
builder.Services.AddScoped<ISpeedCommandService, SpeedCommandService>();
builder.Services.AddScoped<ISpeedQueryService, SpeedQueryService>();
builder.Services.AddScoped<ITemperatureReadingRepository, TemperatureReadingRepository>();
builder.Services.AddScoped<ITemperatureCommandService, TemperatureCommandService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}


app.UseGlobalExceptionHandler();

var supportedCultures = new[] { "en", "es" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture(supportedCultures[0])
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);

app.UseRequestLocalization(localizationOptions);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAllPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();