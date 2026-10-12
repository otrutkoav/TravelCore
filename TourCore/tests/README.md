# Tests

## Unit tests

Unit tests do not require external services:

```powershell
dotnet test tests/TourCore.Catalog.UnitTests/TourCore.Catalog.UnitTests.csproj
```

The suite covers domain rules in Geography, Hotels, Finance, Avia, Bus,
Railway, Transportation, Transfers, Seating and Services, plus the Country
application handler's persistence, conflict and cache-notification behavior.

## PostgreSQL integration tests

Integration tests run migrations against a dedicated database and wrap each test
in a rolled-back transaction. For safety, the database name must contain `test`.
Tests also validate the complete active Catalog EF model without connecting to a
database: all 31 Catalog `DbSet` entities, table mappings, primary keys and core
foreign keys. The live PostgreSQL suite verifies that migrations create every
Catalog table, repository round-tripping and database FK enforcement.

```powershell
$env:TOURCORE_TEST_POSTGRES = "Host=localhost;Port=5432;Database=tourcore_catalog_test;Username=tourcore_admin;Password=..."
dotnet test tests/TourCore.Catalog.IntegrationTests/TourCore.Catalog.IntegrationTests.csproj
```

Never point `TOURCORE_TEST_POSTGRES` at a development or production database.
