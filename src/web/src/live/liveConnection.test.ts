import type { HubConnection } from '@microsoft/signalr';
import { describe, expect, it, vi } from 'vitest';
import { LiveConnection } from './liveConnection';

function fakeHub() {
  return {
    start: vi.fn(() => Promise.resolve()),
    stop: vi.fn(() => Promise.resolve()),
    invoke: vi.fn(() => Promise.resolve()),
    on: vi.fn(),
    off: vi.fn(),
  };
}

describe('LiveConnection', () => {
  it('invokes the hub group methods', async () => {
    const hub = fakeHub();
    const live = new LiveConnection(hub as unknown as HubConnection);

    await live.joinKingdom(231);
    await live.leaveKingdom(231);

    expect(hub.invoke).toHaveBeenNthCalledWith(1, 'JoinKingdom', 231);
    expect(hub.invoke).toHaveBeenNthCalledWith(2, 'LeaveKingdom', 231);
  });

  it('returns an unsubscribe function for events', () => {
    const hub = fakeHub();
    const live = new LiveConnection(hub as unknown as HubConnection);
    const handler = vi.fn();

    const unsubscribe = live.on('WorldStateChanged', handler);
    unsubscribe();

    expect(hub.on).toHaveBeenCalledWith('WorldStateChanged', handler);
    expect(hub.off).toHaveBeenCalledWith('WorldStateChanged', handler);
  });
});
