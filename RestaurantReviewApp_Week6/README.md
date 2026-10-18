# Restaurant Review App — Week 6 Blazor UI connected to Week 7 REST API

This is the Week 6 Blazor UI updated to call the separate `RestaurantReviewApi` project instead of using the in-memory `FakeRestaurantService`.

## Run

Start `RestaurantReviewApi` first on `http://localhost:5052`, then run this project on `http://localhost:5255`.

```powershell
dotnet restore
dotnet run
```

The UI uses `RestaurantApiClient` and `IHttpClientFactory` to call `/api/restaurants` endpoints. The API must remain running for list, details, add, edit, delete, and review operations.
