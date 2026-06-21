workspace "GoldMetrics GoldCheck Platform" "Component Diagram - FleetOperations Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      fleet = container "FleetOperations API" "Vehicle assignment and hauling-cycle lifecycle (start, load, complete)." "ASP.NET Core" {
        tags "Context"

        vehController   = component "VehiclesController" "AssignVehicle, GetById/All." "REST Controller" "REST Controller"
        cycleController = component "HaulingCyclesController" "Start, LoadMaterial, Complete, GetById/All." "REST Controller" "REST Controller"
        vehCmd  = component "VehicleCommandService" "Vehicle commands." "Application Service" "Application Service"
        vehQry  = component "VehicleQueryService" "Vehicle queries." "Application Service" "Application Service"
        cycCmd  = component "HaulingCycleCommandService" "Hauling-cycle commands." "Application Service" "Application Service"
        cycQry  = component "HaulingCycleQueryService" "Hauling-cycle queries." "Application Service" "Application Service"
        vehRepo = component "VehicleRepository" "Persists Vehicle." "Repository" "Repository"
        cycRepo = component "HaulingCycleRepository" "Persists HaulingCycle." "Repository" "Repository"
        vehAgg  = component "Vehicle" "Vehicle aggregate root." "Domain Aggregate" "Domain Aggregate"
        cycAgg  = component "HaulingCycle" "HaulingCycle aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm  = component "FleetOperationsActionResultAssembler" "Results to IActionResult." "Assembler" "Assembler"
        cmdAsm = component "Command Assemblers" "AssignVehicle / StartHaulingCycle from resource." "Assembler" "Assembler"
        entAsm = component "Entity Assemblers" "VehicleResourceFromEntity, HaulingCycleResourceFromEntity." "Assembler" "Assembler"
        res = component "FleetOperations Resources" "CreateVehicleResource, StartHaulingCycleResource, LoadMaterialResource, CompleteHaulingCycleResource, VehicleResource, HaulingCycleResource." "Resources (DTOs)" "Resources (DTOs)"

        vehController -> vehCmd "Sends commands to"
        vehController -> vehQry "Runs queries on"
        cycleController -> cycCmd "Sends commands to"
        cycleController -> cycQry "Runs queries on"
        vehController -> arAsm "Uses"
        vehController -> cmdAsm "Uses"
        vehController -> entAsm "Uses"
        vehController -> res "Reads/writes"
        cycleController -> arAsm "Uses"
        cycleController -> cmdAsm "Uses"
        cycleController -> entAsm "Uses"
        cycleController -> res "Reads/writes"
        vehCmd -> vehRepo "Saves via"
        vehQry -> vehRepo "Reads via"
        cycCmd -> cycRepo "Saves via"
        cycQry -> cycRepo "Reads via"
        vehRepo -> vehAgg "Returns / persists"
        cycRepo -> cycAgg "Returns / persists"

        vehCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        cycCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        vehRepo -> db "Reads/writes" "EF Core"
        cycRepo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component fleet "FleetOperations_Components" {
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
