workspace "GoldMetrics GoldCheck Platform" "System Context Diagram" {

  model {
    supervisor = person "Mine Supervisor" "Monitors production, routes and accident reports."
    operator   = person "Vehicle Operator" "Drives haul trucks; subject to fatigue and incident detection."
    jeweler    = person "Jeweler" "Registers materials and signs jewelry certificates."
    consumer   = person "End Consumer" "Scans product QR codes to view traceability and certificates."
    admin      = person "Administrator" "Manages users, plans and feature access."

    platform = softwareSystem "GoldCheck Platform" "Mineral & jewelry traceability platform for mining operations (ASP.NET Core modular monolith + MySQL)."

    supervisor -> platform "Views dashboards & accident reports"
    operator   -> platform "Reports incidents & telemetry"
    jeweler    -> platform "Manages jewelry & certificates"
    consumer   -> platform "Scans product QR codes"
    admin      -> platform "Administers users, plans & access"
  }

  views {
    systemContext platform "SystemContext" {
      include *
      autoLayout lr
    }

    styles {
      element "Person" {
        shape Person
        background #6b4f2a
        color #ffffff
      }
      element "Software System" {
        background #b8860b
        color #ffffff
      }
    }
  }
}
