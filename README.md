# .Net Domain-Driven Design Template

Template of how to build applications with ASP.NET Core and DDD concepts.

## Note

This pattern is not an example of efficient code execution or optimal algorithms. The pattern is an example of building an architecture using DDD concepts.

## Basic Concepts

![Basic concepts of DDD architecture](/.github/images/concepts.png)

## Layers

1. DDD.API:
   The entry point for interaction with the application. This layer is where input data is checked and authorization is performed.
2. DDD.Business:
   The layer responsible for the application logic. This layer can be logically divided into two sublayers: processes and services.

   Processes are responsible for additional data verification, additional permissions/rights check and transformation of DTOs into domain models.
   Services are responsible for additional data processing and transformation of domain models into database entities.

3. DDD.DataAccess
   The layer responsible for interaction with the database.
4. DDD.Core
   Public layer containing various extensions.

## Technologies implemented:

- ASP.NET
- .NET Core Native DI
- Entity Framework Core (with PostgreSQL)
- AutoMapper
- Swagger
