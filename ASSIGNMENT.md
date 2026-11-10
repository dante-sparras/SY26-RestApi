## Purpose

The purpose of the assignment is for you to demonstrate that you can build a RESTful ASP.NET Core Web API that communicates with a SQL Server database using Entity Framework Core.

## Task

You are to create an API for managing advertisements, similar to the basic functionality of, for example, a buy-and-sell site.

An advertisement must be stored in a SQL Server database. You decide for yourself which properties an advertisement needs, but it must contain sufficient information to be able to create, view and update an advertisement.

The project must use:

- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- Code First

The assignment must be implemented as a controller-based Web API. Minimal API must not be used as the solution for the assignment.

## For a Pass (G)

To achieve a Pass, the API must implement the following endpoints:

- `/Ads` — HTTP GET = list all
- `/Ads/<id>` — HTTP GET = list one
- `/Ads` — HTTP POST = create one
- `/Ads/<id>` — HTTP DELETE = delete
- `/Ads/<id>` — HTTP PUT = update (full update)

The API must also:

- follow REST principles for endpoints and HTTP methods,
- return appropriate HTTP status codes depending on the outcome of the call.

## API Documentation and Testing

The API must be testable during development.

As of .NET 9, Swagger UI is not automatically included in all new project templates. You must research for yourself how Swagger/OpenAPI or equivalent tools can be configured for the project.

## For a Pass with Distinction (VG)

In addition to all requirements for a Pass, you must:

- implement HTTP PATCH for partial updating of an advertisement.
- implement validation of data sent to the API and return an appropriate HTTP status code when the data is invalid

You are expected to research for yourself how PATCH is implemented in ASP.NET Core.

## Submission

The project is submitted with a link to your repo on GitHub in the learning platform. NOTE! it must be public at least until grading is complete.

The application must be able to be started and tested.

The assignment must be kept simple and focus on the requirements above. Additional functionality is not required and does not affect the assessment.

## Assessment

The assignment is assessed as IG/G/VG.

For a Pass, all G requirements must be completed and the API must be able to be started and tested.

For a Pass with Distinction, all requirements for a Pass must be fulfilled as well as the VG requirement for partial updating with HTTP PATCH.
