# RestaurantReviewApi

Controller-based ASP.NET Core Web API for the Restaurant Review Application.

## Run

```powershell
dotnet restore
dotnet run
```

Swagger UI: `http://localhost:5052/swagger`

The API stores records in `restaurants.db` using Entity Framework Core and SQLite. The database is created and seeded on first run.
