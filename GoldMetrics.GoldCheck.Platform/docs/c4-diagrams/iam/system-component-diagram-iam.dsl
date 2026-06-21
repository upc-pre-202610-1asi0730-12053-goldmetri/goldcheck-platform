workspace "GoldMetrics GoldCheck Platform" "Component Diagram - IAM Context" {

  model {
    platform = softwareSystem "GoldCheck Platform" {

      sharedKernel = container "Shared Kernel" "Result<T>, IUnitOfWork, ProblemDetails factory." "ASP.NET Core" {
        tags "Shared"
      }
      db = container "Platform Database" "MySQL via EF Core." "MySQL 8 / EF Core" {
        tags "Database"
      }

      iam = container "IAM API" "Registration, authentication (JWT) and user profile management." "ASP.NET Core" {
        tags "Context"

        authController  = component "AuthenticationController" "RegisterUser, AuthenticateUser." "REST Controller" "REST Controller"
        usersController = component "UsersController" "GetUserById, GetAllUsers, UpdateProfile." "REST Controller" "REST Controller"
        cmd  = component "IamCommandService" "Handles register / authenticate / update-profile commands." "Application Service" "Application Service"
        qry  = component "IamQueryService" "Handles user queries." "Application Service" "Application Service"
        repo = component "UserRepository" "Persists User aggregates (IUserRepository)." "Repository" "Repository"
        user = component "User" "User aggregate root." "Domain Aggregate" "Domain Aggregate"
        arAsm  = component "IamActionResultAssembler" "Maps Result<User> / token to IActionResult." "Assembler" "Assembler"
        entAsm = component "UserResourceFromEntityAssembler" "Maps User to UserResponse." "Assembler" "Assembler"
        res = component "IAM Resources" "RegisterUserResource, AuthenticateUserResource, UpdateProfileResource, UserResponse, AuthTokenResponse." "Resources (DTOs)" "Resources (DTOs)"

        authController -> cmd "Sends commands to"
        usersController -> cmd "Sends commands to"
        usersController -> qry "Runs queries on"
        authController -> arAsm "Uses"
        authController -> entAsm "Uses"
        authController -> res "Reads/writes"
        usersController -> arAsm "Uses"
        usersController -> entAsm "Uses"
        usersController -> res "Reads/writes"
        cmd -> repo "Loads & saves via"
        qry -> repo "Reads via"
        repo -> user "Returns / persists"

        cmd -> sharedKernel "Commits via UnitOfWork / returns Result"
        repo -> db "Reads/writes" "EF Core"
      }
    }
  }

  views {
    component iam "IAM_Components" {
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
