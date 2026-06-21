workspace "GoldMetrics GoldCheck Platform" "Container Diagram - Bounded Context APIs Overview" {

  model {
    platform = softwareSystem "GoldCheck Platform" "Mineral & jewelry traceability modular monolith." {

      client       = container "Web / Mobile Client" "Consumes the REST API over HTTPS." "SPA / Mobile" {
        tags "Client"
      }
      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, IBaseRepository, ProblemDetails factory, exception middleware." "ASP.NET Core" {
        tags "Shared"
      }
      db           = container "Platform Database" "Single MySQL database (one DbContext, modular configuration)." "MySQL 8 / EF Core" {
        tags "Database"
      }

      iam         = container "IAM API" "Registration, authentication (JWT) and user profiles." "ASP.NET Core" {
        tags "Context"
      }
      material    = container "MaterialOperations API" "Mineral identification, classification and movement tracking." "ASP.NET Core" {
        tags "Context"
      }
      jewelry     = container "JewelryInventory API" "Material registration, QR scanning, certificate generation & signing." "ASP.NET Core" {
        tags "Context"
      }
      reporting   = container "ReportingNotifications API" "Accident-data reports and user notifications." "ASP.NET Core" {
        tags "Context"
      }
      analytics   = container "Analytics API" "Route-progress and production dashboards." "ASP.NET Core" {
        tags "Context"
      }
      asset       = container "AssetMaintenance API" "Machinery tracking, maintenance scheduling and discharge." "ASP.NET Core" {
        tags "Context"
      }
      fleet       = container "FleetOperations API" "Vehicle assignment and hauling-cycle lifecycle." "ASP.NET Core" {
        tags "Context"
      }
      consumerCtx = container "ConsumerTraceability API" "Public QR scan, traceability journey and certificate download." "ASP.NET Core" {
        tags "Context"
      }
      billing     = container "SubscriptionsAndBilling API" "Plans, subscriptions, feature access and invoices." "ASP.NET Core" {
        tags "Context"
      }
      incident    = container "IncidentManagement API" "Fatigue/smoke/accident detection, risk evaluation and alerts." "ASP.NET Core" {
        tags "Context"
      }
      monitoring  = container "MonitoringTelemetry API" "GNSS, speed, pressure, temperature and communication telemetry." "ASP.NET Core" {
        tags "Context"
      }

      // Client -> APIs
      client -> iam         "REST/JSON" "HTTPS"
      client -> material    "REST/JSON" "HTTPS"
      client -> jewelry     "REST/JSON" "HTTPS"
      client -> reporting   "REST/JSON" "HTTPS"
      client -> analytics   "REST/JSON" "HTTPS"
      client -> asset       "REST/JSON" "HTTPS"
      client -> fleet       "REST/JSON" "HTTPS"
      client -> consumerCtx "REST/JSON" "HTTPS"
      client -> billing     "REST/JSON" "HTTPS"
      client -> incident    "REST/JSON" "HTTPS"
      client -> monitoring  "REST/JSON" "HTTPS"

      // APIs -> Shared Kernel
      iam -> sharedKernel "Builds on"
      material -> sharedKernel "Builds on"
      jewelry -> sharedKernel "Builds on"
      reporting -> sharedKernel "Builds on"
      analytics -> sharedKernel "Builds on"
      asset -> sharedKernel "Builds on"
      fleet -> sharedKernel "Builds on"
      consumerCtx -> sharedKernel "Builds on"
      billing -> sharedKernel "Builds on"
      incident -> sharedKernel "Builds on"
      monitoring -> sharedKernel "Builds on"

      // APIs -> Database
      iam -> db "Reads/writes" "EF Core"
      material -> db "Reads/writes" "EF Core"
      jewelry -> db "Reads/writes" "EF Core"
      reporting -> db "Reads/writes" "EF Core"
      analytics -> db "Reads" "EF Core"
      asset -> db "Reads/writes" "EF Core"
      fleet -> db "Reads/writes" "EF Core"
      consumerCtx -> db "Reads/writes" "EF Core"
      billing -> db "Reads/writes" "EF Core"
      incident -> db "Reads/writes" "EF Core"
      monitoring -> db "Reads/writes" "EF Core"
    }
  }

  views {
    container platform "Containers" {
      include *
      autoLayout lr
    }

    styles {
      element "Container" {
        background #d4a017
        color #1a1a1a
      }
      element "Context" {
        background #d4a017
        color #1a1a1a
      }
      element "Client" {
        background #4a7fb5
        color #ffffff
        shape WebBrowser
      }
      element "Shared" {
        background #8a8a8a
        color #ffffff
      }
      element "Database" {
        shape Cylinder
        background #6b6b6b
        color #ffffff
      }
    }
  }
}
