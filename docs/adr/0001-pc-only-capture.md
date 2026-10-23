# 0001. Capture only on PC

- Status: accepted
- Date: 2026-10-23

## Context

Kingshot runs on phones and on PC through Android emulators such as BlueStacks. To build live tools we need the
game's network traffic. On a PC we can sniff it passively with Npcap, without touching the game client, the
emulator, or the device. Capturing on phones would need a VPN-based interceptor or a rooted device. Both are
fragile and intrusive, and they're much harder to support.

## Decision

The capture agent is a Windows desktop app that sniffs emulator traffic with SharpPcap over Npcap. We don't
support capture on mobile devices.

## Consequences

- Contributors need Windows plus Npcap to run capture. The backend and web app stay cross-platform.
- The agent targets `net10.0-windows` (WPF). Capture and upload logic live in `net10.0` libraries, so they can be
  tested on any OS.
- Coverage depends on players who run the game on PC with the agent installed.
