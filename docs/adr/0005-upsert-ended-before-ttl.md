# 0005. Upsert `status: "ended"` before TTL expiry

- Status: accepted
- Date: 2026-10-23

## Context

Short-lived entities, such as marches, rallies, and temporary map objects, expire through Cosmos per-item TTL.
The Cosmos change feed in "latest version" mode doesn't emit deletes, and TTL expirations are deletes. If we
relied on TTL alone, live clients would never learn that a march ended. It would stay on the Live Map.

## Decision

When the game reports that an entity ended, or when we infer it, we upsert the document with
`status: "ended"` and a short `ttl`. The change feed delivers that upsert, the Live processor broadcasts it, and
clients remove the entity. TTL then cleans up the document.

## Consequences

- Every TTL-managed document carries `status` (`active` / `ended`). Readers filter on `status = "active"`.
- Entities that vanish without an end event need a sweeper that marks stale documents `ended`. TODO: design it
  with the ingest worker.
- If we move to the "all versions and deletes" change feed mode later, we can revisit this decision.
