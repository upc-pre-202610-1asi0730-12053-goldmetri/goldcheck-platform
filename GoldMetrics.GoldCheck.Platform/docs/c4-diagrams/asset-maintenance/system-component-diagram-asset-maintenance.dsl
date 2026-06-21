workspace "GoldMetrics GoldCheck Platform" "Component Diagram - AssetMaintenance Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      asset = container "AssetMaintenance API" "Machinery registration, engine-hours tracking, maintenance scheduling and discharge." "ASP.NET Core" {
        tags "Context"

        controller = component "MachineryController" "RegisterMachinery, UpdateData, ScheduleMaintenance, Discharge(Machinery/Component), GetById/All." "REST Controller" "REST Controller"
        cmd  = component "AssetMaintenanceCommandService" "Machinery commands." "Application Service" "Application Service"
        qry  = component "AssetMaintenanceQueryService" "Machinery queries." "Application Service" "Application Service"
        repo = component "MachineryRepository" "Persists Machinery." "Repository" "Repository"
        agg  = component "Machinery" "Machinery aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm  = component "AssetMaintenanceActionResultAssembler" "Results to IActionResult." "Assembler" "Assembler"
        entAsm = component "MachineryResourceFromEntityAssembler" "Entity to MachineryResource." "Assembler" "Assembler"
        res = component "AssetMaintenance Resources" "RegisterMachineryResource, UpdateMachineryDataResource, ScheduleMaintenanceResource, DischargeMachineryResource, DischargeComponentResource, MachineryResource." "Resources (DTOs)" "Resources (DTOs)"

        controller -> cmd "Sends commands to"
        controller -> qry "Runs queries on"
        controller -> arAsm "Uses"
        controller -> entAsm "Uses"
        controller -> res "Reads/writes"
        cmd -> repo "Saves via"
        qry -> repo "Reads via"
        repo -> agg "Returns / persists"

        cmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        repo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component asset "AssetMaintenance_Components" {
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
