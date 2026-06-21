workspace "GoldMetrics GoldCheck Platform" "Component Diagram - Analytics Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      analytics = container "Analytics API" "Route-progress and production dashboards built from material production data." "ASP.NET Core" {
        tags "Context"

        controller = component "AnalyticsController" "ViewRouteProgress, ViewProductionDashboard, RequestProductionData, GetById/All/ByPeriod." "REST Controller" "REST Controller"
        cmd  = component "AnalyticsCommandService" "Analytics commands." "Application Service" "Application Service"
        qry  = component "AnalyticsQueryService" "Analytics queries." "Application Service" "Application Service"
        repo = component "MaterialRepository" "Reads Material production data (Analytics' own IMaterialRepository)." "Repository" "Repository"
        agg  = component "Material" "Material read/analytics model." "Domain Aggregate" "Domain Aggregate"
        arAsm  = component "AnalyticsActionResultAssembler" "Results to IActionResult." "Assembler" "Assembler"
        entAsm = component "MaterialResourceFromEntityAssembler" "Entity to MaterialResource." "Assembler" "Assembler"
        res = component "Analytics Resources" "ViewRouteProgressResource, ViewProductionDashboardResource, RequestProductionDataResource, MaterialResource." "Resources (DTOs)" "Resources (DTOs)"

        controller -> cmd "Sends commands to"
        controller -> qry "Runs queries on"
        controller -> arAsm "Uses"
        controller -> entAsm "Uses"
        controller -> res "Reads/writes"
        cmd -> repo "Reads via"
        qry -> repo "Reads via"
        repo -> agg "Returns"

        cmd -> sharedKernel "Returns Result"
        repo -> db "Reads" "EF Core"
      }
    }
  }

  views {
    component analytics "Analytics_Components" {
      include *
      autoLayout lr
    }

    styles {
      element "Context" {
        background #d4a017
        color #1a1a1a
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
      element "Component" {
        background #f5d76e
        color #1a1a1a
      }
      element "REST Controller" {
        background #e0992e
        color #1a1a1a
      }
      element "Application Service" {
        background #f5d76e
        color #1a1a1a
      }
      element "Assembler" {
        background #fbe9a0
        color #1a1a1a
      }
      element "Repository" {
        background #c9a227
        color #1a1a1a
      }
      element "Domain Aggregate" {
        background #efc75e
        color #1a1a1a
        shape RoundedBox
      }
      element "Resources (DTOs)" {
        background #fff3cc
        color #1a1a1a
        shape Folder
      }
    }
  }
}
