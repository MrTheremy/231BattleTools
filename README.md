# Kingshot Battle Tools

Battle tools for the mobile game Kingshot, played on PC through BlueStacks or other emulators.

| App | Where | What it does |
| --- | --- | --- |
| **Capture agent** | `src/agent` | Windows tray app. Sniffs game traffic (SharpPcap + Npcap), reassembles TCP into timed frames, spools them locally, and uploads batches over WebSocket/TLS. `--dev` opens a dev console. |
| **Backend** | `src/backend` | One ASP.NET Core app: API, ingest WebSocket, decoder worker → Cosmos DB, change feed → SignalR (one group per kingdom), raw-frame archive in Blob Storage, and the Protocol Lab admin area. |
| **Web app** | `src/web` | React + TypeScript with four tabs: Live Map (PixiJS), Live Activity, Battle Comparison, and Analysis & Playback. |
| **Decoder** | `src/shared/KingshotTools.Decoder` | sproto/FPNN decoding shared by the backend and the agent dev console. **It isn't included in Release agent builds** ([ADR 0003](docs/adr/0003-decoder-excluded-from-release-agent.md)). |

> **Status:** this is a scaffold. Capture, reassembly, decoding and business logic are interfaces and stubs marked with `TODO`.

## Protocol facts

| Port | Protocol | Handling |
| --- | --- | --- |
| TCP 30101 | sproto: 2-byte big-endian length + sproto.pack body | Game data, uploaded |
| TCP 13321 | FPNN: 12-byte header + MessagePack | Chat. **Bodies are never uploaded** |
| TCP 443 | HTTPS | Ignored |

## Repository layout

```
src/agent/      KingshotTools.Agent (WPF tray + --dev console), .Agent.Capture, .Agent.Upload
src/shared/     KingshotTools.Contracts (frame/batch DTOs), KingshotTools.Decoder
src/backend/    KingshotTools.Api (host), .Ingest (decoder worker), .Live (change feed + LiveHub), .Data (Cosmos/Blob)
src/web/        Vite + React + TS
tests/          xUnit v3 projects, plus Architecture.Tests (Release agent has no decoder)
tools/          scripts (verify-release-agent.ps1), Cosmos bootstrap notes
samples/        local captures; git-ignored except the README
docs/           design doc link and ADRs
```

Solution filters: `KingshotTools.Backend.slnf` (shared + backend, runs anywhere) and `KingshotTools.Agent.slnf`
(agent + shared, runs on Windows). `KingshotTools.sln` contains everything.

## Getting started

### Prerequisites

- **.NET SDK 10.0** (pinned in `global.json`, which also rolls forward to newer 10.0 feature bands).
- **Node.js LTS** (22.12 or later).
- **Docker** (Docker Desktop on Windows/macOS) for the local Cosmos DB and Azurite emulators.
- **Npcap** (Windows, agent only): install it from <https://npcap.com>. Tick *"Install Npcap in WinPcap API-compatible
  mode"*. SharpPcap needs Npcap at runtime, and capturing usually needs the agent to run as an administrator.
- Windows is needed to *run* the agent (WPF). Everything else, including building the agent, works on Linux and macOS too.

### 1. Start the emulators

```sh
docker compose up -d
```

- Cosmos DB emulator (vNext): `http://localhost:8081`, Data Explorer at <http://localhost:1234>
- Azurite: Blob `http://127.0.0.1:10000`, Queue `:10001`, Table `:10002`

The well-known emulator connection strings are in `src/backend/KingshotTools.Api/appsettings.Development.json`.
They're public development keys, not secrets.

### 2. Run the backend

```sh
dotnet run --project src/backend/KingshotTools.Api
```

In Development, the API creates the `kingshot` database, the containers (`messages`, `worldState`, `players`,
`battles`, `snapshots`, `leases`) and the `raw-frames` blob container on startup.

- Health: <http://localhost:5080/health> (Cosmos + Blob), <http://localhost:5080/health/live> (process only)
- OpenAPI (Development only): <http://localhost:5080/openapi/v1.json>
- SignalR hub: `/hubs/live` (`JoinKingdom(mapKid)`, `LeaveKingdom(mapKid)`)
- Ingest WebSocket: `ws://localhost:5080/ingest/ws` (accepts JSON `FrameBatch` messages and logs them)

Use user-secrets for local overrides. Never commit them:

```sh
dotnet user-secrets --project src/backend/KingshotTools.Api set "Cosmos:ConnectionString" "<value>"
```

### 3. Run the web app

```sh
cd src/web
npm install
npm run dev        # http://localhost:5173
```

The backend URL comes from `VITE_API_BASE_URL` (default in `.env.development`). Put personal overrides in
`.env.development.local`. Other scripts: `npm run lint`, `npm test`, `npm run build`, `npm run format`.

### 4. Run the agent (Windows)

```sh
dotnet run --project src/agent/KingshotTools.Agent              # tray only
dotnet run --project src/agent/KingshotTools.Agent -- --dev     # tray + dev console
```

The agent starts in the notification area. Right-click the icon to open the dev console (only with `--dev`) or to
exit. Closing the dev console hides it; the agent keeps running in the tray.

### Build and test everything

```sh
dotnet build
dotnet test
pwsh tools/verify-release-agent.ps1   # proves the Release agent contains no decoder
```

`tests/KingshotTools.Agent.Tests` targets `net10.0-windows` and runs only on Windows. On other OSes it builds but `dotnet test` skips it.

## Conventions

- **Feature folders** inside each project (for example `DevConsole/Packets`, `Features/Ingest`, `Cosmos/`, `Blob/`).
- **An interface at every external boundary**: `ICaptureDevice`, `ITcpReassembler`, `IFrameSpool`,
  `IBatchUploader`, `IGameDocumentStore`, `IRawFrameArchive`, `ILiveBroadcaster`, `IIngestQueue`, and
  `TimeProvider` for the clock.
- Central settings in `Directory.Build.props` (nullable, implicit usings, warnings as errors, latest C#,
  deterministic) and package versions in `Directory.Packages.props`.
- **No secrets in the repo.** Use user-secrets locally and Key Vault / app settings in Azure.
- **Never commit captures.** `*.pcapng` is ignored everywhere ([ADR 0004](docs/adr/0004-raw-pcapng-never-leaves-the-pc.md)).
- Decisions go in [`docs/adr`](docs/adr).
