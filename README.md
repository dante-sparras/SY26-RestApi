# Ads API

A controller-based ASP.NET Core Web API for managing advertisements, built with Entity Framework Core, SQL Server, and Code First migrations.

## Requirements

- .NET 10 SDK
- SQL Server

The default connection string in `AdsApi/appsettings.json` uses the local default SQL Server instance with Windows authentication:

```text
Server=localhost;Database=Sy26RestApi;Trusted_Connection=True;TrustServerCertificate=True
```

If you use SQL Server Express, change `localhost` to `localhost\SQLEXPRESS`.

## Setup

```powershell
dotnet tool restore
dotnet restore AdsApi
dotnet run --project AdsApi
```

On startup the API creates the database if it does not exist, applies the Code First migrations, and inserts three sample ads when the `Ads` table is empty.

If you prefer to apply the migration manually before starting:

```powershell
dotnet ef database update --project AdsApi
```

The SQL Server user must be allowed to create the database and change its schema.

Open Swagger UI at the URL shown in the console, normally:

```text
http://localhost:5168/swagger
```

## Endpoints

| Method | Route | Description | Success | Errors |
|---|---|---|---|---|
| GET | `/Ads` | List all advertisements | 200 | |
| GET | `/Ads/{id}` | Get one advertisement | 200 | 404 |
| POST | `/Ads` | Create an advertisement | 201 | 400 |
| PUT | `/Ads/{id}` | Replace an advertisement | 204 | 400, 404 |
| PATCH | `/Ads/{id}` | Partially update an advertisement | 204 | 400, 404 |
| DELETE | `/Ads/{id}` | Delete an advertisement | 204 | 404 |

Invalid request data returns `400 Bad Request` with validation details.

## Sample data

When the `Ads` table is empty, startup adds three sample ads: a mountain bike, an office chair, and a gaming console. Seeding is skipped when ads already exist. If all ads are deleted, the samples are restored on the next restart.

## PATCH example

PATCH uses JSON Patch and requires the `application/json-patch+json` content type. Use an `id` from `GET /Ads` or the `Location` header returned by `POST /Ads`:

```http
PATCH /Ads/{id}
Content-Type: application/json-patch+json

[
  { "op": "replace", "path": "/title", "value": "Updated title" }
]
```
