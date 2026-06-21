workspace "GoldMetrics GoldCheck Platform" "Component Diagram - MonitoringTelemetry Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      monitoring = container "MonitoringTelemetry API" "Ingests and analyses asset telemetry: GNSS, speed, pressure, temperature, communication and raw telemetry." "ASP.NET Core" {
        tags "Context"

        telController  = component "TelemetryController" "Process/Validate/Get telemetry data." "REST Controller" "REST Controller"
        gnssController = component "GNSSController" "Monitor, DetectAnomaly, Restart, LogRestart, Get." "REST Controller" "REST Controller"
        spdController  = component "SpeedController" "Monitor, DetectExcess, LogExcess, Get(violations)." "REST Controller" "REST Controller"
        prsController  = component "PressureController" "Monitor, Analyse, DetectAnomaly, LogAnomaly, Get." "REST Controller" "REST Controller"
        tmpController  = component "TemperatureController" "Monitor, Analyse, DetectAnomaly, LogAnomaly, Get(All)." "REST Controller" "REST Controller"
        comController  = component "CommunicationController" "Monitor, Analyse, DetectAnomaly, LogAnomaly, Get." "REST Controller" "REST Controller"

        telSvc  = component "Telemetry Services" "ITelemetryCommandService / ITelemetryQueryService." "Application Service" "Application Service"
        gnssSvc = component "GNSS Services" "IGNSSCommandService / IGNSSQueryService." "Application Service" "Application Service"
        spdSvc  = component "Speed Services" "ISpeedCommandService / ISpeedQueryService." "Application Service" "Application Service"
        prsSvc  = component "Pressure Services" "IPressureCommandService / IPressureQueryService." "Application Service" "Application Service"
        tmpSvc  = component "Temperature Services" "ITemperatureCommandService / ITemperatureQueryService." "Application Service" "Application Service"
        comSvc  = component "Communication Services" "ICommunicationCommandService / ICommunicationQueryService." "Application Service" "Application Service"

        telRepo  = component "TelemetryDataRepository" "Persists TelemetryData." "Repository" "Repository"
        gnssRepo = component "GNSSStatusRepository" "Persists GNSSStatus." "Repository" "Repository"
        spdRepo  = component "SpeedReadingRepository" "Persists SpeedReading." "Repository" "Repository"
        prsRepo  = component "PressureReadingRepository" "Persists PressureReading." "Repository" "Repository"
        tmpRepo  = component "TemperatureReadingRepository" "Persists TemperatureReading." "Repository" "Repository"
        comRepo  = component "CommunicationChannelRepository" "Persists CommunicationChannel." "Repository" "Repository"

        aggs = component "Telemetry Aggregates" "TelemetryData, GNSSStatus, SpeedReading, PressureReading, TemperatureReading, CommunicationChannel." "Domain Aggregate" "Domain Aggregate"
        arAsm = component "MonitoringTelemetryActionResultAssembler" "Maps every reading result to IActionResult." "Assembler" "Assembler"
        entAsm = component "Reading Assemblers" "Telemetry/GNSS/Speed/Pressure/Temperature/Communication entity assemblers." "Assembler" "Assembler"
        res = component "MonitoringTelemetry Resources" "Monitor*/Analyse*/Detect*/Log* resources and *Reading/*Status/*Channel resources." "Resources (DTOs)" "Resources (DTOs)"

        telController -> telSvc "Uses"
        gnssController -> gnssSvc "Uses"
        spdController -> spdSvc "Uses"
        prsController -> prsSvc "Uses"
        tmpController -> tmpSvc "Uses"
        comController -> comSvc "Uses"
        telController -> arAsm "Uses"
        gnssController -> arAsm "Uses"
        spdController -> arAsm "Uses"
        prsController -> arAsm "Uses"
        tmpController -> arAsm "Uses"
        comController -> arAsm "Uses"
        telController -> entAsm "Uses"
        telController -> res "Reads/writes"
        gnssController -> entAsm "Uses"
        gnssController -> res "Reads/writes"
        spdController -> entAsm "Uses"
        spdController -> res "Reads/writes"
        prsController -> entAsm "Uses"
        prsController -> res "Reads/writes"
        tmpController -> entAsm "Uses"
        tmpController -> res "Reads/writes"
        comController -> entAsm "Uses"
        comController -> res "Reads/writes"
        telSvc -> telRepo "Uses"
        gnssSvc -> gnssRepo "Uses"
        spdSvc -> spdRepo "Uses"
        prsSvc -> prsRepo "Uses"
        tmpSvc -> tmpRepo "Uses"
        comSvc -> comRepo "Uses"
        telRepo -> aggs "Returns / persists"
        gnssRepo -> aggs "Returns / persists"
        spdRepo -> aggs "Returns / persists"
        prsRepo -> aggs "Returns / persists"
        tmpRepo -> aggs "Returns / persists"
        comRepo -> aggs "Returns / persists"

        telSvc -> sharedKernel "Commits via UnitOfWork / returns Result"
        telRepo -> db "Reads/writes" "EF Core"
        gnssRepo -> db "Reads/writes" "EF Core"
        spdRepo -> db "Reads/writes" "EF Core"
        prsRepo -> db "Reads/writes" "EF Core"
        tmpRepo -> db "Reads/writes" "EF Core"
        comRepo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component monitoring "MonitoringTelemetry_Components" {
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
