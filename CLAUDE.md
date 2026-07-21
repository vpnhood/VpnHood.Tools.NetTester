# VpnHood.Tools.NetTester

Network performance testing tool for TCP, HTTP, HTTPS and QUIC, shipped as a .NET tool
(`nettester`) so other machines can consume it without building from source.

Onboarding and release conventions for this kind of repo live in the monorepo:
`vpnhood/VpnHood` → `pub/TOOL-REPOS.md`.

## Layout

```text
src/VpnHood.Tools.NetTester/
  Clients/    client-side app and options
  Servers/    server host and config
  Testers/    per-protocol testers (Http, Quic, Tcp)
  Utils/      argument parsing, logging
  Program.cs  entry point
pub/PubVersion.json   version state, stamped by CI
```

## Build

```bash
dotnet build VpnHood.Tools.NetTester.slnx
dotnet pack  src/VpnHood.Tools.NetTester/VpnHood.Tools.NetTester.csproj -o ./artifacts
```

- Package versions are centrally managed in `Directory.Packages.props`. Do not put `Version=` on a
  `PackageReference`.
- `Directory.Build.props` holds the single `<Version>` every project inherits. It must stay the
  first version element in the file — the publish script regex-replaces the first match. Never add
  a version to a csproj; it silently overrides the stamp.
- There is no test project yet. If you add one, set `<IsPackable>false</IsPackable>` on it (plain,
  no `Condition`) or it will be published, and add `global.json` with
  `"test": { "runner": "Microsoft.Testing.Platform" }` if it uses MSTest.Sdk 4+.

## Releasing

```powershell
./_publish.ps1
```

Dispatches `publish_nugets.yml`, which runs the monorepo's shared
`pub/lib/Publish-ModuleNugetPackages.ps1` with `-independentVersion`.

- **CI owns the bump**, not your machine. It happens only on a publish dispatch, before packing, and
  is committed back to the branch as `Publish vX.Y.Z` — so `git pull` before your next work.
- Only the **build number** self-bumps (`1.0.5 → 1.0.6`). A minor/major is a hand edit of
  `pub/PubVersion.json` **and** `Directory.Build.props` in one commit.
- The monorepo's `8.0.x` version is never adopted; this tool keeps its own `1.x` line.
- Publishing uses **Trusted Publishing** (OIDC) — no long-lived API key. The only repository secret
  is `NUGET_USER` (the nuget.org profile name).
- The nuget.org policy is bound to the workflow **file name**. Renaming `publish_nugets.yml` breaks
  publishing until the policy is updated; the failure says
  `Workflow mismatch for policy ...: expected X, actual Y`.
