# Backend Template

This repo hosts a reusable ASP.NET Core backend template (clean, layered, .NET 10). Contribute improvements here so new projects generated from the template inherit them.

## Getting Started (Repo)
- Prereqs: .NET SDK 10.0+, Docker (for compose), and `dotnet new` for template testing.
- Clone, then `dotnet restore src/Mayonyies.Api/Mayonyies.Api.csproj` and `dotnet build src/Mayonyies.Api/Mayonyies.Api.csproj`.
- Run locally: `dotnet run --project src/Mayonyies.Api/Mayonyies.Api.csproj`.
- Compose: `docker compose up --build` (provide `.env` with `POSTGRES_*` and `MayonyiesDbUrl`).
- Health checks: `/health/live` (self) and `/health/ready` (includes Postgres via `MayonyiesDbContext`); readiness requires a configured connection string.

## Packaging & Installing the Template
- Pack: `dotnet pack Mayonyies.Backend.Template.csproj -o ./nupkg` (creates `Mayonyies.Backend.Template.*.nupkg`).
- Install locally for testing: `dotnet new install ./nupkg/Mayonyies.Backend.Template.*.nupkg`.
- Generate a project: `dotnet new backend -n MyAppName` (uses `sourceName` replacement to rename namespaces/identifiers).
- Uninstall after testing: `dotnet new uninstall backend`.

## Project Structure
- `src/Core`: domain contracts/entities.
- `src/Application`: use cases, validation, JWT/auth, domain events, DI helpers (extension methods declared with `this IServiceCollection` to keep params nullability clean).
- `src/Infrastructure` + `src/Infrastructure/Mayonyies.Repository.EfCore`: persistence, interceptors, repository abstractions, DI.
- `src/Mayonyies.Api`: host, endpoints (including health checks), middleware, Serilog config, `appsettings.*` (uses `InternalsVisibleTo` to access the internal `MayonyiesDbContext` for readiness checks).
- Support: `Directory.Packages.props` (centralized package versions), `global.json` (SDK pin), `.template.config/template.json` (template metadata), `compose.yaml` (API + Postgres).

## Contributing
- Follow coding style in `AGENTS.md` (nullable enabled, implicit usings, 4-space indent, PascalCase/camelCase, async suffix).
- Keep changes scoped; commit messages should be short and imperative (e.g., “Update publish_template.yml”).
- Add or adjust tests where applicable (create under `tests/` with `*Tests.cs` naming).
- Document behavior changes in `README_Package.md` if they affect generated projects; keep this `README.md` focused on repo installation/contribution.
