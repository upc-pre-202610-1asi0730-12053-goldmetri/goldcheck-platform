# GoldCheck Platform
 
## Project Overview
**GoldCheck** is a robust and scalable backend platform for **mineral and jewelry traceability**, tracking gold from extraction in the field through processing, jewelry manufacturing, and final delivery to the consumer. Built with **.NET 10**, it follows **Domain-Driven Design (DDD)** principles and implements **Command Query Responsibility Segregation (CQRS)**, promoting modularity, maintainability, and testability across a large, multi-context industrial domain.
 
The platform is organized around **eleven Bounded Contexts**, each owning a specific slice of the traceability chain — from fleet hauling cycles and safety incidents in the mine, through material classification, jewelry manufacturing, consumer-facing certificates, and subscription billing.
 
## Table of Contents
- [Architecture Overview](#architecture-overview)
- [Domain-Driven Design (DDD) Concepts](#domain-driven-design-ddd-concepts)
- [Key Features & Best Practices Implemented](#key-features--best-practices-implemented)
- [Bounded Contexts](#bounded-contexts)
  - [IAM (Identity and Access Management)](#iam-identity-and-access-management)
  - [Fleet Operations](#fleet-operations)
  - [Material Operations](#material-operations)
  - [Asset Maintenance](#asset-maintenance)
  - [Incident Management](#incident-management)
  - [Monitoring & Telemetry](#monitoring--telemetry)
  - [Jewelry Inventory](#jewelry-inventory)
  - [Consumer Traceability](#consumer-traceability)
  - [Analytics](#analytics)
  - [Reporting & Notifications](#reporting--notifications)
  - [Subscriptions & Billing](#subscriptions--billing)
- [Technologies Used](#technologies-used)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Setup Instructions](#setup-instructions)
- [Project Structure](#project-structure)
- [Documentation](#documentation)
- [License](#license)
## Architecture Overview
The project's architecture is driven by **Domain-Driven Design (DDD)** principles and implements **CQRS**. This approach organizes the codebase to align closely with the business domain, separating operations that change state (Commands) from operations that read state (Queries).
 
- **Domain Layer**: Contains the core business logic, aggregates, entities, value objects, domain events, and repository interfaces. It is the heart of each bounded context and has no dependencies on other layers.
- **Application Layer**: Orchestrates domain objects to fulfill use cases via command and query services/handlers. It also hosts the **Anti-Corruption Layer (ACL)** facades that each context exposes for cross-context communication, keeping bounded contexts decoupled from one another's internal models.
- **Infrastructure Layer**: Provides implementations for interfaces defined in the Domain and Application layers — EF Core repositories, the shared `AppDbContext`, and persistence configuration.
- **Interfaces Layer (Presentation)**: Handles external communication through REST controllers, resources (DTOs), and assemblers that transform between API models and application/domain models.
## Domain-Driven Design (DDD) Concepts
- **Bounded Contexts**: The application is explicitly divided into eleven contexts (IAM, Fleet Operations, Material Operations, Asset Maintenance, Incident Management, Monitoring & Telemetry, Jewelry Inventory, Consumer Traceability, Analytics, Reporting & Notifications, and Subscriptions & Billing), each with its own ubiquitous language and domain model.
- **Aggregates**: Key domain objects such as `Vehicle`, `HaulingCycle`, `Material`, `Machinery`, `SafetyRecord`, `Jewelry`, `JewelryProduct`, `TraceabilityJourney`, `Report`, `Notification`, and `UserSubscription` are defined as aggregate roots, encapsulating clusters of entities and value objects and ensuring transactional consistency within their boundaries.
- **Audit Aggregates**: Most bounded contexts pair their main aggregate with a dedicated `*Audit` aggregate (e.g. `VehicleAudit`, `MaterialAudit`, `JewelryAudit`), used to persist an immutable trail of state changes — central to a traceability platform where provenance must be verifiable end-to-end.
- **Value Objects**: Immutable objects that measure, quantify, or describe a concept in the domain, compared by value rather than identity.
- **Domain Services**: Operations that don't naturally fit within an entity or value object (e.g. `ITokenService`, `IHashingService` in IAM).
- **Domain Events**: Significant occurrences within an aggregate's lifecycle (e.g. a hauling cycle completing, a material batch being classified) are published as domain events to enable loose coupling between contexts.
- **Anti-Corruption Layer (ACL)**: Each context defines a `ContextFacade` interface in its Application layer, consumed by other contexts that need cross-context data — preventing a context's internal domain model from leaking into another's.
## Key Features & Best Practices Implemented
- **Command Query Responsibility Segregation (CQRS)**: Commands update data; Queries retrieve it, allowing independent evolution of read and write models.
- **Cortex.Mediator**: Used for dispatching commands/queries and publishing domain events, including a custom `LoggingCommandBehavior` pipeline behavior for cross-cutting command logging.
- **Cancellation Tokens**: Integrated across asynchronous operations (application services, repositories, controllers) to support graceful cancellation of long-running tasks.
- **Refined Error Management**:
  - **`Result<T>` Pattern**: Used in application services to explicitly represent success or failure without relying on exceptions for control flow.
  - **Domain-Specific Error Enums**: Strongly-typed error enums per context (e.g. `IamError`, `SubscriptionsAndBillingError`) provide clear, categorized business-rule violations.
  - **Localized Error Messages**: Externalized into `.resx` files per context and retrieved via `IStringLocalizer`, supporting English and Spanish.
  - **RFC 7807 Problem Details**: All API error responses follow this standard via a shared `ProblemDetailsFactory`.
  - **Global Exception Handling Middleware**: Catches unhandled exceptions and returns standardized `ProblemDetails` responses, avoiding leakage of sensitive information.
- **Internationalization (i18n)**: Decentralized `.resx` resources per bounded context (`en` and `es`), with culture negotiation configured via `RequestLocalizationOptions`.
- **Clean API Design with Action Result Assemblers**: Thin controllers delegate response formatting to dedicated, per-context static assembler classes that map `Result<T>` to `IActionResult`, including error-to-HTTP-status mapping.
- **Persistence**: Entity Framework Core 10 with the MySQL provider, automatic database creation, and migrations applied on startup.
- **Messaging/Mediation**: Cortex.Mediator for command handling and domain event publication, promoting loose coupling between aggregates and contexts.
- **Authentication & Authorization**: JWT-based authentication (`System.IdentityModel.Tokens.Jwt`, `Microsoft.AspNetCore.Authentication.JwtBearer`) with `BCrypt.Net-Next` for password hashing.
- **API Documentation**: Swagger/OpenAPI via Swashbuckle, with a configured Bearer security scheme and XML annotations enabled for richer endpoint descriptions.
- **Kebab-case routing**: A custom `KebabCaseRouteNamingConvention` and lowercase URL routing keep the API surface consistent.
## Bounded Contexts
 
### IAM (Identity and Access Management)
Responsible for user identity, authentication, and access control across the platform.
- **Scope**: User registration, sign-in, and user management.
- **Aggregates**: `User`, `UserAudit`
- **Domain Services**: `ITokenService` (JWT issuance/validation), `IHashingService` (BCrypt password hashing)
- **API Endpoints**: `/api/v1/authentication` (`sign-up`, `sign-in`), `/api/v1/users`
### Fleet Operations
Manages the haul-truck fleet and hauling cycles that move material from extraction points to processing.
- **Scope**: Vehicle registry and hauling-cycle lifecycle (load → in-transit → complete).
- **Aggregates**: `Vehicle`, `VehicleAudit`, `HaulingCycle`, `HaulingCycleAudit`
- **API Endpoints**: `/api/v1/vehicles`, `/api/v1/hauling-cycles` (`load`, `complete`)
### Material Operations
Tracks raw material batches as they move through extraction, classification, and the custody chain.
- **Scope**: Material batch registration, classification, and tracking.
- **Aggregates**: `Material`, `MaterialAudit`
- **API Endpoints**: `/api/v1/materials` (`classify`, `download`, `track`)
### Asset Maintenance
Manages mining machinery and its components throughout their operational and maintenance lifecycle.
- **Scope**: Machinery registration, maintenance scheduling, and component/machinery discharge.
- **Aggregates**: `Machinery`, `MachineryAudit`
- **API Endpoints**: `/api/v1/machinery` (`schedule-maintenance`, `discharge`, component-level `discharge`)
### Incident Management
Handles workplace safety incidents and their escalation workflow.
- **Scope**: Recording and escalating fatigue, smoke, and accident-related safety incidents; risk-level classification.
- **Aggregates**: `SafetyRecord`, `SafetyRecordAudit`
- **API Endpoints**: `/api/v1/incidents` (`fatigue`, `smoke`, `accidents`, `escalate`, `evaluate`, `alert`, by `type`/`risk-level`)
### Monitoring & Telemetry
Captures real-time sensor data from vehicles and equipment in the field.
- **Scope**: Telemetry ingestion for temperature, pressure, speed, GNSS position, and communication-channel status.
- **Aggregates**: `TelemetryData`, `TemperatureReading`, `PressureReading`, `SpeedReading`, `GNSSStatus`, `CommunicationChannel` (each with a paired `*Audit` aggregate)
- **API Endpoints**: `/api/v1/telemetry`, plus dedicated controllers for temperature, pressure, speed, GNSS, and communication channels
### Jewelry Inventory
Manages jewelry pieces and the certified materials used to manufacture them.
- **Scope**: Jewelry material registration and scanning, and finished jewelry inventory.
- **Aggregates**: `Jewelry`, `JewelryAudit`, `JewelryMaterial`, `JewelryMaterialAudit`
- **API Endpoints**: `/api/v1/jewelry-materials` (`scan`), `/api/v1/certificates` (`sign`)
### Consumer Traceability
The consumer-facing context that exposes the full provenance journey of a finished jewelry product.
- **Scope**: QR-code-based product lookup, traceability journey retrieval, and certificate downloads for end consumers.
- **Aggregates**: `JewelryProduct`, `JewelryProductAudit`, `TraceabilityJourney`, `TraceabilityJourneyAudit`
- **API Endpoints**: `/api/v1/consumer` (`scan`, `products/{qrCode}`, `products/{qrCode}/journey`, `certificates/{certificateId}`)
### Analytics
Provides operational insight derived from routes and production data across the platform.
- **Scope**: Route analytics and production dashboards/reports.
- **API Endpoints**: `/api/v1/analytics` (`routes/view`, `routes/{routeId}`, `production/dashboard`, `production/request`)
### Reporting & Notifications
Generates downloadable reports and delivers notifications to platform users.
- **Scope**: Report lifecycle (load data → generate → export) and notification dispatch.
- **Aggregates**: `Report`, `ReportAudit`, `Notification`, `NotificationAudit`
- **API Endpoints**: `/api/v1/reports` (`load-data`, `generate`, `request-export`, `export`, `download`), `/api/v1/notifications` (`send`, by `user`)
### Subscriptions & Billing
Manages user subscription plans, feature access control, and billing.
- **Scope**: Subscription lifecycle (confirm, downgrade), plan-based feature access checks, invoicing, and payment history.
- **Aggregates**: `UserSubscription`, `UserSubscriptionAudit`
- **API Endpoints**: `/api/v1/subscriptions` (`confirm`, `downgrade`, `access-check`, `invoices`, `payment-history`, `plans/{planType}/features`)
## Technologies Used
- **.NET 10**: Core framework for the application.
- **ASP.NET Core**: For building RESTful APIs.
- **Entity Framework Core 10**: ORM for database interactions (MySQL).
- **MySQL** (via `MySql.EntityFrameworkCore`): Relational database.
- **Cortex.Mediator**: For implementing the Mediator pattern (commands, queries, domain events).
- **BCrypt.Net-Next**: For secure password hashing.
- **System.IdentityModel.Tokens.Jwt** / **Microsoft.AspNetCore.Authentication.JwtBearer**: For JWT generation and validation.
- **Swashbuckle.AspNetCore** (+ Annotations): For OpenAPI/Swagger documentation.
- **Microsoft.Extensions.Localization**: For internationalization (i18n).
- **Humanizer**: For string manipulation (e.g. kebab-case routes, pluralized identifiers).
## Getting Started
 
### Prerequisites
- .NET 10 SDK
- MySQL Server (or Docker for local development)
- Git
### Setup Instructions
1. **Clone the repository:**
```bash
   git clone https://github.com/<your-org>/goldcheck-platform.git
   cd goldcheck-platform
```
 
2. **Navigate to the project directory:**
```bash
   cd GoldMetrics.GoldCheck.Platform
```
 
3. **Restore NuGet packages:**
```bash
   dotnet restore
```
 
4. **Configure environment variables / connection settings:**
   The app reads its configuration from `appsettings.json`, which expects the following placeholders to be supplied via environment variables or a local `appsettings.Development.json` override:
```json
   {
     "TokenSettings": {
       "Secret": "%TOKEN_SECRET%"
     },
     "ConnectionStrings": {
       "DefaultConnection": "server=%DATABASE_URL%;user=%DATABASE_USER%;password=%DATABASE_PASSWORD%;database=%DATABASE_SCHEMA%"
     }
   }
```
 
   - `TOKEN_SECRET`: secret key used to sign JWTs.
   - `DATABASE_URL`, `DATABASE_USER`, `DATABASE_PASSWORD`, `DATABASE_SCHEMA`: MySQL connection details.
   Ensure your MySQL server is running and reachable with these credentials.
 
5. **Run the application:**
```bash
   dotnet run
```
 
   On startup, the application automatically applies pending EF Core migrations (`context.Database.Migrate()`).
 
6. **Access Swagger UI:**
   In the Development environment, open your browser at the URL shown in the console output (typically `https://localhost:7000/swagger` or similar) to explore and test the API endpoints, including the Bearer token authentication flow.
## Project Structure
The project is organized by Bounded Contexts at the top level, with each context further decomposed into architectural layers:
 
```
GoldMetrics.GoldCheck.Platform/
├── Iam/                          # IAM Bounded Context
├── FleetOperations/               # Fleet Operations Bounded Context
├── MaterialOperations/            # Material Operations Bounded Context
├── AssetMaintenance/               # Asset Maintenance Bounded Context
├── IncidentManagement/            # Incident Management Bounded Context
├── MonitoringTelemetry/           # Monitoring & Telemetry Bounded Context
├── JewelryInventory/               # Jewelry Inventory Bounded Context
├── ConsumerTraceability/           # Consumer Traceability Bounded Context
├── Analytics/                      # Analytics Bounded Context
├── ReportingNotifications/         # Reporting & Notifications Bounded Context
├── SubscriptionsAndBilling/        # Subscriptions & Billing Bounded Context
│   ├── Application/                #   Command/Query Services, ACL Facades
│   ├── Domain/                     #   Aggregates, Entities, Value Objects, Events, Errors
│   ├── Infrastructure/             #   Repository Implementations
│   ├── Interfaces/                 #   REST Controllers, Resources, Assemblers
│   └── Resources/                  #   Context-specific localization files (en/es)
├── Shared/                          # Shared Bounded Context (cross-cutting concerns)
│   ├── Application/                #   Generic Result<T>, common application models
│   ├── Domain/                     #   Base Repository, Unit of Work interfaces
│   ├── Infrastructure/             #   Base Repository implementation, AppDbContext
│   ├── Interfaces/                 #   ProblemDetailsFactory, common REST interfaces
│   └── Resources/                  #   Shared & error localization files
├── Migrations/                      # EF Core migrations
├── Program.cs                       # Application startup and DI configuration
├── appsettings.json                 # Configuration file
└── GoldMetrics.GoldCheck.Platform.csproj
```
 
Each bounded context follows the same four-layer pattern (`Application` / `Domain` / `Infrastructure` / `Interfaces`), keeping the codebase predictable to navigate regardless of which context you're working in.
 
## Documentation
Comprehensive documentation, including C4 and class diagrams for every bounded context, can be found in the `docs/` directory:
 
- **C4 Diagrams**: `docs/c4-diagrams/`
- **Class Diagrams** (per bounded context, by layer): `docs/class-diagrams/<ContextName>/`
## License
This project is licensed under the MIT License. See the `LICENSE.md` file for details.
