workspace "GoldMetrics GoldCheck Platform" "Component Diagram - ReportingNotifications Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      reporting = container "ReportingNotifications API" "Accident-data reports (request/generate/export/download) and user notifications." "ASP.NET Core" {
        tags "Context"

        reportsController = component "ReportsController" "RequestAccidentData, LoadAccidentData, Generate, Export, Download, GetById/All." "REST Controller" "REST Controller"
        notifController   = component "NotificationsController" "RequestNotification, SendNotification, GetById, GetByUser." "REST Controller" "REST Controller"
        repCmd  = component "ReportCommandService" "Report commands." "Application Service" "Application Service"
        repQry  = component "ReportQueryService" "Report queries." "Application Service" "Application Service"
        notCmd  = component "NotificationCommandService" "Notification commands." "Application Service" "Application Service"
        notQry  = component "NotificationQueryService" "Notification queries." "Application Service" "Application Service"
        repRepo = component "ReportRepository" "Persists Report." "Repository" "Repository"
        notRepo = component "NotificationRepository" "Persists Notification." "Repository" "Repository"
        repAgg  = component "Report" "Report aggregate root." "Domain Aggregate" "Domain Aggregate"
        notAgg  = component "Notification" "Notification aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm   = component "ReportingNotificationsActionResultAssembler" "Results to IActionResult." "Assembler" "Assembler"
        entAsm  = component "Entity Assemblers" "ReportResourceFromEntity, NotificationResourceFromEntity." "Assembler" "Assembler"
        res = component "ReportingNotifications Resources" "RequestAccidentDataResource, RequestReportExportationResource, ReportResource, RequestNotificationResource, NotificationResource." "Resources (DTOs)" "Resources (DTOs)"

        reportsController -> repCmd "Sends commands to"
        reportsController -> repQry "Runs queries on"
        notifController -> notCmd "Sends commands to"
        notifController -> notQry "Runs queries on"
        reportsController -> arAsm "Uses"
        reportsController -> entAsm "Uses"
        reportsController -> res "Reads/writes"
        notifController -> arAsm "Uses"
        notifController -> entAsm "Uses"
        notifController -> res "Reads/writes"
        repCmd -> repRepo "Saves via"
        repQry -> repRepo "Reads via"
        notCmd -> notRepo "Saves via"
        notQry -> notRepo "Reads via"
        repRepo -> repAgg "Returns / persists"
        notRepo -> notAgg "Returns / persists"

        repCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        notCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        repRepo -> db "Reads/writes" "EF Core"
        notRepo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component reporting "ReportingNotifications_Components" {
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
