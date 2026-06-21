workspace "GoldMetrics GoldCheck Platform" "Component Diagram - JewelryInventory Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      jewelry = container "JewelryInventory API" "Registers non-certified materials, scans QR, generates and signs certificates." "ASP.NET Core" {
        tags "Context"

        matController  = component "JewelryMaterialsController" "RegisterMaterial, GetById/All, ScanQR." "REST Controller" "REST Controller"
        certController = component "CertificatesController" "GenerateCertificate, SignCertificate, GetById." "REST Controller" "REST Controller"
        matCmd  = component "JewelryMaterialCommandService" "Material commands." "Application Service" "Application Service"
        matQry  = component "JewelryMaterialQueryService" "Material queries." "Application Service" "Application Service"
        jewCmd  = component "JewelryCommandService" "Certificate commands." "Application Service" "Application Service"
        jewQry  = component "JewelryQueryService" "Certificate queries." "Application Service" "Application Service"
        matRepo = component "JewelryMaterialRepository" "Persists JewelryMaterial." "Repository" "Repository"
        jewRepo = component "JewelryRepository" "Persists Jewelry (certificates)." "Repository" "Repository"
        matAgg  = component "JewelryMaterial" "Material aggregate root." "Domain Aggregate" "Domain Aggregate"
        jewAgg  = component "Jewelry" "Certificate aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm   = component "JewelryInventoryActionResultAssembler" "Results to IActionResult." "Assembler" "Assembler"
        cmdAsm  = component "Command Assemblers" "RegisterNonCertifiedMaterial / ScanQRMaterial / GenerateCertificate / SignCertificate from resource." "Assembler" "Assembler"
        entAsm  = component "Entity Assemblers" "JewelryMaterialResourceFromEntity, CertificateResourceFromEntity." "Assembler" "Assembler"
        res = component "JewelryInventory Resources" "CreateMaterialResource, ScanQRResource, GenerateCertificateResource, SignCertificateResource, JewelryMaterialResource, CertificateResource." "Resources (DTOs)" "Resources (DTOs)"

        matController -> matCmd "Sends commands to"
        matController -> matQry "Runs queries on"
        certController -> matCmd "Sends commands to"
        certController -> jewCmd "Sends commands to"
        certController -> jewQry "Runs queries on"
        matController -> arAsm "Uses"
        matController -> cmdAsm "Uses"
        matController -> entAsm "Uses"
        matController -> res "Reads/writes"
        certController -> arAsm "Uses"
        certController -> cmdAsm "Uses"
        certController -> entAsm "Uses"
        certController -> res "Reads/writes"
        matCmd -> matRepo "Saves via"
        matQry -> matRepo "Reads via"
        jewCmd -> jewRepo "Saves via"
        jewQry -> jewRepo "Reads via"
        matRepo -> matAgg "Returns / persists"
        jewRepo -> jewAgg "Returns / persists"

        matCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        jewCmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        matRepo -> db "Reads/writes" "EF Core"
        jewRepo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component jewelry "JewelryInventory_Components" {
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
