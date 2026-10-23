# 0002. Cosmos DB for JSON game data

- Status: accepted
- Date: 2026-10-23

## Context

Decoded game messages are deeply nested and loosely typed, and their schema changes with game updates. We want
to store the raw decoded object, query by kingdom and time, expire short-lived entities, and push changes to
browsers in near real time.

## Decision

We use Azure Cosmos DB (NoSQL API). Documents keep the raw game object under `data`, next to a few indexed
envelope fields (`id`, `mapKid`, `day`, `type`, `status`, `ttl`). Every container is partitioned by kingdom
(`/mapKid`). `messages` uses a hierarchical key (`/mapKid`, `/day`) so that one kingdom's history doesn't grow
into a single hot logical partition. The change feed drives live updates.

## Consequences

- Schema changes in the game don't need migrations. Mapping code reads `data` defensively.
- Per-item TTL handles cleanup. Because the change feed doesn't report deletes, see ADR 0005.
- Local development uses the Cosmos DB Linux emulator (vNext) through `docker compose`.
