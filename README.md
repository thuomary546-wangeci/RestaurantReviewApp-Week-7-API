# Restaurant Review Application — RESTful Web API and Blazor UI

This submission builds on the Week 6 Blazor restaurant UI and follows the Week 7 course material on controller-based ASP.NET Core Web APIs, HTTP verbs, JSON responses, Swagger/OpenAPI, and `IHttpClientFactory`.

## Solution layout

- `RestaurantReviewApi/` — ASP.NET Core Web API using controller-based REST endpoints, Swagger, Entity Framework Core, and a SQLite database.
- `RestaurantReviewApp_Week6/` — the Week 6 Blazor UI updated to call the separate API through an `IHttpClientFactory`-configured `HttpClient`.
- `Screenshots/` — instructions and checklist for capturing your own evidence screenshots.

## Prerequisites

Install the .NET 10 SDK and open the extracted folder in Visual Studio Code. The first run requires internet access to restore NuGet packages.

## Run the application (two terminals)

### Terminal 1 — Start the API

```powershell
cd RestaurantReviewApi
dotnet restore
dotnet run
```

The launch profile uses `http://localhost:5052`. Open `http://localhost:5052/swagger` to test the endpoints. The first run creates `restaurants.db` and seeds three sample restaurants if the database is empty.

### Terminal 2 — Start the Week 6 Blazor UI

```powershell
cd RestaurantReviewApp_Week6
dotnet restore
dotnet run
```

The UI uses `http://localhost:5255`. Open `http://localhost:5255/restaurants`. It retrieves and changes restaurant data by sending HTTP requests to the API at `http://localhost:5052`.

Keep the API terminal running while using the UI. If you change the API's port, update `ApiSettings:BaseUrl` in `RestaurantReviewApp_Week6/appsettings.json` to match.

## REST API endpoints

| HTTP method | Endpoint | Purpose | Expected response |
|---|---|---|---|
| GET | `/api/restaurants` | Retrieve all restaurants | `200 OK`, JSON array |
| GET | `/api/restaurants/{id}` | Retrieve one restaurant | `200 OK` or `404 Not Found` |
| POST | `/api/restaurants` | Add a restaurant | `201 Created`, JSON restaurant |
| PUT | `/api/restaurants/{id}` | Update a restaurant | `204 No Content` or `404 Not Found` |
| DELETE | `/api/restaurants/{id}` | Delete a restaurant | `204 No Content` or `404 Not Found` |
| POST | `/api/restaurants/{id}/reviews` | Add a review | `200 OK`, updated JSON restaurant |

The API uses `[ApiController]`, route attributes, action result types, model validation, and JSON serialization, consistent with the Week 7 lecture examples. Swagger UI is available at `/swagger` while the API is running in Development mode.

## How the UI consumes the API

`RestaurantReviewApp_Week6/Services/RestaurantApiClient.cs` uses `IHttpClientFactory` to create the named `RestaurantApi` client. The Blazor pages call the client for list, details, create, update, delete, and review operations. The UI no longer uses the Week 6 `FakeRestaurantService`; data is persisted by the API in SQLite.

## Database

The API uses Entity Framework Core with SQLite. `RestaurantDbContext` maps the restaurant model to the database. The list of review strings is serialized as JSON in a database text column. The local database file is created automatically and is ignored by Git; do not upload it as source code.

## Suggested screenshots for submission

Capture your own screenshots after running and testing the project. See `Screenshots/README.md` for the list. Do not use placeholder screenshots as evidence of a successful run.

## GitHub

Upload the source files and README to a GitHub repository. Do not upload `bin`, `obj`, `.vs`, or the local `.db` file. Include the repository URL in your assignment report after confirming the link opens correctly.
