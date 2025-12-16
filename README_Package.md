# Backend API Template (Generated Project)

This project was created from the `backend` template and gives you a ready-to-run ASP.NET Core solution with layered architecture and Serilog logging.

## Quickstart
- Prereqs: .NET SDK 10.0+ and Docker (optional for Postgres).
- Restore/build: `dotnet restore src/<YourApp>.sln` then `dotnet build src/<YourApp>.sln`.
- Run API: `dotnet run --project src/<YourApp>.Api/<YourApp>.Api.csproj`.
- Compose: `docker compose up --build` to launch the API and Postgres; set `.env` values for `POSTGRES_*` and `<YourApp>DbUrl`.

## Project Layout
- `src/Core`: domain contracts/entities.
- `src/Application`: application services, validation, auth/JWT helpers, domain events, DI.
- `src/Infrastructure` + `src/Infrastructure/<YourApp>.Repository.EfCore`: EF Core persistence, interceptors, repository abstractions, DI wiring.
- `src/<YourApp>.Api`: host, endpoints, middleware, Serilog setup, `appsettings.*`.

## Configuration
- App settings: update `src/<YourApp>.Api/appsettings.json` and `.Development.json` for Serilog, hosts, and any new options you add.
- Secrets: keep secrets out of source control; use environment variables or `dotnet user-secrets` locally (`UserSecretsId` is already set in the API project).
- Connection strings: provide `<YourApp>Db` via appsettings or `ConnectionStrings__<YourApp>Db` env var; align with `docker compose` variables.

## Development Workflow
- Add tests under a `tests/` folder mirroring source namespaces (name files `*Tests.cs`); run with `dotnet test src/<YourApp>.sln`.
- Register services in each `DependencyInjection.cs`; prefer constructor injection and interface-driven design.
- Follow nullable-enabled, implicit-using style with 4-space indentation; suffix async methods with `Async` and keep commit messages imperative.

## Next Steps
- Update this README with domain-specific details.
- Add database migrations and seed data if needed.
- Wire any new external dependencies into `compose.yaml` and environment variables.
