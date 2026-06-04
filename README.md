**ERP Purchase Order Management System**

T**Technology Stack**
Backend: ASP.NET Core 9, MediatR, FluentValidation, JWT
Frontend: Angular 20
Database: MongoDB

**Key Architectural Decisions**
Clean Architecture, CQRS, MediatR, JWT Authentication, Role-Based Access Control.

**Project Structure**
ERP-> ERP.API, ERP.Application, ERP.Domain, ERP.Infrastructure, ERP.Tests **(Clean Architecture)**
ERP-> FrontEnd/ERP-PurchaseOrderClient

**How To Run Locally**
Install -> .NET 9 SDK, Node.js, Angular CLI, MongoDB. 
Run backend using dotnet run and frontend using ng serve.

**Test Credentials**
**Procurement Officer**: procurement / Procurement@123 **-> Role:** ProcurementOfficer
**Finance Manager**: finance / Finance@123 **-> Role:** FinanceManager

**Purchase Order Workflow**
Draft -> Submitted -> Approved/Rejected

**MongoDB Configuration**
Database: ERPDb
Connection String: mongodb://localhost:27017

**Why MongoDB Instead of SQL Server**
Flexible schema, embedded line items, rapid development, fewer joins.

**Database Index**
Created ascending index on PoNumber because PO lookups and searches are commonly performed using PO Number.

**Validation**
Angular Reactive Forms + FluentValidation + MediatR Validation Pipeline.

**Error Handling**
Frontend -> Angular HTTP Interceptor.
Backend -> Global Exception Middleware

**Testing**
xUnit backend tests **(Done)**, Angular service tests **(Done)**, Angular component tests **(not performed)**.

**API Versioning**
Implemented URL versioning: /api/v1/purchase-orders

**What I Would Do Differently With More Time**
Add audit logs, pagination, Angular Material UI, Docker, CI/CD, caching, refresh tokens.

