workspace "GoldMetrics GoldCheck Platform" "Component Diagram - SubscriptionsAndBilling Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      billing = container "SubscriptionsAndBilling API" "Plan selection, subscription lifecycle, feature access, invoices and payment history." "ASP.NET Core" {
        tags "Context"

        controller = component "SubscriptionsController" "SelectPlan, Confirm, Downgrade, Feature access, Invoices, Plan features." "REST Controller" "REST Controller"
        cmd  = component "SubscriptionsBillingCommandService" "Subscription/billing commands." "Application Service" "Application Service"
        qry  = component "SubscriptionsBillingQueryService" "Subscription/billing queries." "Application Service" "Application Service"
        repo = component "UserSubscriptionRepository" "Persists UserSubscription." "Repository" "Repository"
        subAgg = component "UserSubscription" "Subscription aggregate root." "Domain Aggregate" "Domain Aggregate"
        invAgg = component "Invoice" "Invoice entity." "Domain Aggregate" "Domain Aggregate"
        arAsm  = component "SubscriptionsBillingActionResultAssembler" "Results / invoices to IActionResult." "Assembler" "Assembler"
        entAsm = component "UserSubscriptionResourceFromEntityAssembler" "Entity to UserSubscriptionResponse." "Assembler" "Assembler"
        res = component "SubscriptionsAndBilling Resources" "SelectPlanResource, ConfirmSubscriptionResource, RequestDowngradeResource, CheckFeatureAccessResource, RequestAccessResource, CheckUserPlanResource, AssignFeaturesResource, UserSubscriptionResponse, InvoiceResponse." "Resources (DTOs)" "Resources (DTOs)"

        controller -> cmd "Sends commands to"
        controller -> qry "Runs queries on"
        controller -> arAsm "Uses"
        controller -> entAsm "Uses"
        controller -> res "Reads/writes"
        cmd -> repo "Saves via"
        qry -> repo "Reads via"
        repo -> subAgg "Returns / persists"
        repo -> invAgg "Returns / persists"

        cmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        repo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component billing "SubscriptionsAndBilling_Components" {
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
