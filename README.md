# Backend Template

This repository hosts a reusable ASP.NET Core backend template (clean, layered, .NET 10). Use it to scaffold new services, and contribute improvements here so every generated project benefits.

## Getting Started (Repo)
- Prereqs: .NET SDK 10.0+, Docker (for compose), and `dotnet new` CLI for template testing.
- Clone, then `dotnet restore src/Mayonyies.sln` and `dotnet build src/Mayonyies.sln`.
- Run locally with `dotnet run --project src/Mayonyies.Api/Mayonyies.Api.csproj`.
- Use `docker compose up --build` to run the API with Postgres; set `.env` values for `POSTGRES_*` and `MayonyiesDbUrl`.

## Packaging & Installing the Template
- Pack: `dotnet pack Mayonyies.Backend.Template.csproj -o ./nupkg` (creates `Mayonyies.Backend.Template.*.nupkg`).
- Install locally for testing: `dotnet new install ./nupkg/Mayonyies.Backend.Template.*.nupkg`.
- Generate a project: `dotnet new backend -n MyAppName` (uses `sourceName` replacement to rename namespaces/identifiers).
- Uninstall when finished testing: `dotnet new uninstall backend`.

## Project Structure
- `src/Core`: domain contracts/entities.
- `src/Application`: use cases, validation, JWT/auth, domain events, DI helpers.
- `src/Infrastructure` + `src/Infrastructure/Mayonyies.Repository.EfCore`: persistence, interceptors, repository abstractions, and DI.
- `src/Mayonyies.Api`: host, endpoints, middleware, Serilog config, `appsettings.*`.
- Support: `Directory.Packages.props` (centralized package versions), `global.json` (SDK pin), `.template.config/template.json` (template metadata), `compose.yaml` (API + Postgres).

## Contributing
- Follow coding style in `AGENTS.md` (nullable enabled, implicit usings, 4-space indent, PascalCase/camelCase, async suffix).
- Keep changes scoped; commit messages should be short and imperative (e.g., “Update publish_template.yml”).
- Add or adjust tests where applicable (create under `tests/` with `*Tests.cs` naming).
- Document behavior changes in `README_Package.md` if they affect generated projects; keep this `README.md` focused on repo installation/contribution.
