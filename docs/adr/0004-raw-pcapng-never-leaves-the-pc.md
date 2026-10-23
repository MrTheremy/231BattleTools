# 0004. Raw .pcapng never leaves the PC

- Status: accepted
- Date: 2026-10-23

## Context

A full packet capture holds everything the PC sent and received on the captured interface. That can include
account and session tokens, private chat (FPNN on port 13321), and unrelated traffic.

## Decision

- `.pcapng` files are recorded and loaded only by the dev console, on the local machine. They're never uploaded,
  attached to issues, or committed.
- The agent uploads only reassembled frames from known game ports, as decided by `IUploadPolicy`. Game data
  (sproto, 30101) is uploaded. FPNN chat bodies are never uploaded. Everything else, including 443, is dropped.
- `.gitignore` excludes `*.pcap`/`*.pcapng` everywhere, and everything under `samples/` except its README.

## Consequences

- Reproducing a decoder bug from a capture means sharing a minimal, scrubbed frame dump, not the capture itself.
- The backend archives uploaded frame batches in Blob Storage, never raw captures.
