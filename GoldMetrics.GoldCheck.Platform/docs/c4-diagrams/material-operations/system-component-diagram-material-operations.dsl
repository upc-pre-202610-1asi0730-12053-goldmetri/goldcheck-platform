workspace "GoldMetrics GoldCheck Platform" "Component Diagram - MaterialOperations Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      material = container "MaterialOperations API" "Mineral identification, classification, download and movement tracking of material batches." "ASP.NET Core" {
        tags "Context"

        controller = component "MaterialsController" "IdentifyMineralType, Classify, Download, TrackMovement, GetById/All." "REST Controller" "REST Controller"
        cmd  = component "MaterialCommandService" "Handles material batch commands." "Application Service" "Application Service"
        qry  = component "MaterialQueryService" "Handles material batch queries." "Application Service" "Application Service"
        repo = component "MaterialRepository" "Persists Material aggregates (IMaterialRepository)." "Repository" "Repository"
        agg  = component "Material" "Material batch aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm   = component "MaterialOperationsActionResultAssembler" "Maps results to IActionResult." "Assembler" "Assembler"
        cmdAsm  = component "IdentifyMineralTypeCommandFromResourceAssembler" "Resource to command." "Assembler" "Assembler"
        entAsm  = component "MaterialResourceFromEntityAssembler" "Entity to MaterialResource." "Assembler" "Assembler"
        res = component "MaterialOperations Resources" "CreateMaterialResource, ClassifyMaterialResource, DownloadMaterialResource, TrackMaterialMovementResource, MaterialResource." "Resources (DTOs)" "Resources (DTOs)"

        controller -> cmd "Sends commands to"
        controller -> qry "Runs queries on"
        controller -> arAsm "Uses"
        controller -> cmdAsm "Uses"
        controller -> entAsm "Uses"
        controller -> res "Reads/writes"
        cmd -> repo "Loads & saves via"
        qry -> repo "Reads via"
        repo -> agg "Returns / persists"

        cmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        repo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component material "MaterialOperations_Components" {
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
