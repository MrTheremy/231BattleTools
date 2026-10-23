# tools/

| Script | What it does |
| --- | --- |
| `verify-release-agent.ps1` | Builds the agent in Release and fails if `KingshotTools.Decoder` ends up in the output or `.deps.json` ([ADR 0003](../docs/adr/0003-decoder-excluded-from-release-agent.md)). Run with `pwsh tools/verify-release-agent.ps1`. |

## Cosmos container bootstrap

Containers are created by the API itself on startup **in the Development environment only**
(`StorageBootstrapHostedService`, driven by `Cosmos:EnsureCreatedOnStartup` and `Blob:EnsureCreatedOnStartup`).
The container list and partition keys live in one place:
`src/backend/KingshotTools.Data/Cosmos/CosmosContainerCatalog.cs`.

| Container | Partition key | TTL |
| --- | --- | --- |
| `messages` | `/mapKid`, `/day` (hierarchical) | per item |
| `worldState` | `/mapKid` | per item |
| `players` | `/mapKid` | off |
| `battles` | `/mapKid` | off |
| `snapshots` | `/mapKid` | per item |
| `leases` | `/id` | off |

TODO: provision the Azure account, database and containers with infrastructure-as-code (Bicep) for non-local environments.
