# Repository Guidelines

This template scaffolds a backend for new apps; clone, rename namespaces, and plug in your domain.

## Project Structure & Module Organization
- `src/Core/Mayonyies.Core`: domain contracts and entities.
- `src/Application/Mayonyies.Application`: use cases, validation, JWT/auth, domain events, and DI helpers.
- `src/Infrastructure/Mayonyies.Infrastructure` and `Mayonyies.Repository.EfCore`: persistence (EF Core/Npgsql), interceptors, repository abstractions, and DI wiring.
- `src/Mayonyies.Api`: ASP.NET Core host, middleware, endpoints, Serilog configuration, and `appsettings.*`.
- Supporting files: `Directory.Packages.props` for central package versions, `global.json` locking .NET SDK 10.0, `compose.yaml` for API + Postgres, and `nupkg/` for packed templates.

## Build, Test, and Development Commands
- `dotnet restore src/Mayonyies.sln` then `dotnet build src/Mayonyies.sln` for a clean build.
- `dotnet run --project src/Mayonyies.Api/Mayonyies.Api.csproj` to launch the API locally.
- `dotnet test src/Mayonyies.sln` (once tests are present); add `--filter` to target suites.
- `dotnet pack Mayonyies.Backend.Template.csproj -o ./nupkg` to produce the distributable template `.nupkg`.
- `docker compose up --build` to run the API with Postgres; supply `.env` values for `POSTGRES_*` and `MayonyiesDbUrl`.
- If build/test commands fail because of restricted permissions (e.g., package restore), request the needed escalation before retrying.

## Coding Style & Naming Conventions
- Target `net10.0`, with `<Nullable>enable</Nullable>` and implicit usings; keep 4-space indentation.
- PascalCase for types/public members; camelCase for locals/fields; suffix async methods with `Async`.
- Register dependencies in each project’s `DependencyInjection.cs`; prefer constructor injection and interfaces from `Core`.
- Keep configuration in `appsettings.*` mapped via options; avoid hard-coded secrets or connection strings.

## Template Usage Notes
- Rename the solution/projects (`Mayonyies.*`) to your app name; update namespaces and default connection string names accordingly.
- Review `compose.yaml` to align image names and env vars with your app; add migrations and seeded data as needed.
- Uncomment and adapt `.github/workflows/publish_template.yml` if you want CI for restore/build/test/pack/push.
- Template packaging uses `Mayonyies.Backend.Template.csproj`; template metadata lives in `.template.config/template.json` (identity, shortName `backend`, `sourceName` replacements, and README rename on install).
- Root `README.md` should explain installation and contribution guidelines for this template repo; `README_Package.md` is the README shipped inside the packed template that end-users see after creating a project.

## Testing Guidelines
- Add test projects under a `tests/` folder mirroring source namespaces (e.g., `Application/Users/UserServiceTests.cs`).
- Use your .NET test framework of choice (xUnit/NUnit/MSTest); name files `*Tests.cs` and keep Arrange/Act/Assert structure.
- Run `dotnet test src/Mayonyies.sln`; for EF Core scenarios, favor in-memory or containerized databases over production connections.

## Commit & Pull Request Guidelines
- Commit messages in this repo are short and imperative (e.g., “Update publish_template.yml”); keep scopes small and focused.
- PRs should include a concise summary, linked issue, notes on API/DB/migration impacts, and steps to validate (commands or endpoint screenshots).
- Call out configuration or secret changes; ensure new services are wired via DI and reflected in `compose.yaml` if they add dependencies.

## Security & Configuration Tips
- Do not commit secrets; use User Secrets (`UserSecretsId` in `Mayonyies.Api`) or environment variables for local runs and CI.
- Configure `ConnectionStrings__MayonyiesDb` and JWT/signing secrets via env vars; ensure Serilog logs stay structured and avoid sensitive payloads.
