workspace "GoldMetrics GoldCheck Platform" "Component Diagram - ConsumerTraceability Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      consumerCtx = container "ConsumerTraceability API" "Public QR scan, traceability journey and certificate download for end consumers." "ASP.NET Core" {
        tags "Context"

        controller = component "ConsumerController" "ScanProductQR, GetProductByQR, GetTraceabilityJourney, Download/GetCertificate." "REST Controller" "REST Controller"
        prodCmd  = component "JewelryProductCommandService" "Product commands." "Application Service" "Application Service"
        prodQry  = component "JewelryProductQueryService" "Product queries." "Application Service" "Application Service"
        jrnCmd   = component "TraceabilityJourneyCommandService" "Journey commands." "Application Service" "Application Service"
        jrnQry   = component "TraceabilityJourneyQueryService" "Journey queries." "Application Service" "Application Service"
        prodRepo = component "JewelryProductRepository" "Persists JewelryProduct." "Repository" "Repository"
        jrnRepo  = component "TraceabilityJourneyRepository" "Persists TraceabilityJourney." "Repository" "Repository"
        prodAgg  = component "JewelryProduct" "Product aggregate root." "Domain Aggregate" "Domain Aggregate"
        jrnAgg   = component "TraceabilityJourney" "Journey aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm  = component "ConsumerTraceabilityActionResultAssembler" "Results to IActionResult." "Assembler" "Assembler"
        cmdAsm = component "Command Assemblers" "ScanProductQR / DownloadCertificate from resource." "Assembler" "Assembler"
        entAsm = component "Entity Assemblers" "JewelryProductResourceFromEntity, TraceabilityJourneyResourceFromEntity." "Assembler" "Assembler"
        res = component "ConsumerTraceability Resources" "ScanProductQRResource, DownloadCertificateResource, JewelryProductResource, TraceabilityJourneyResource." "Resources (DTOs)" "Resources (DTOs)"

        controller -> prodCmd "Sends commands to"
        controller -> prodQry "Runs queries on"
        controller -> jrnQry "Runs queries on"
        controller -> arAsm "Uses"
        controller -> cmdAsm "Uses"
        controller -> entAsm "Uses"
        controller -> res "Reads/writes"
        prodCmd -> prodRepo "Saves via"
        prodQry -> prodRepo "Reads via"
        jrnCmd -> jrnRepo "Saves via"
        jrnQry -> jrnRepo "Reads via"
        prodRepo -> prodAgg "Returns / persists"
        jrnRepo -> jrnAgg "Returns / persists"

        prodCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        prodRepo -> db "Reads/writes" "EF Core"
        jrnRepo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component consumerCtx "ConsumerTraceability_Components" {
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
