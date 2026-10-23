import { HubConnectionBuilder, LogLevel, type HubConnection } from '@microsoft/signalr';
import { config } from '../config';

/** Hub route; keep in sync with LiveHub.Route in KingshotTools.Live. */
export const LIVE_HUB_PATH = '/hubs/live';

/** Server -> client callbacks; keep in sync with ILiveClient in KingshotTools.Live. */
export interface LiveClientEvents {
  WorldStateChanged: (documents: unknown[]) => void;
}

/** Typed wrapper around the SignalR connection to LiveHub. */
export class LiveConnection {
  private readonly connection: HubConnection;

  constructor(connection?: HubConnection) {
    this.connection =
      connection ??
      new HubConnectionBuilder()
        .withUrl(`${config.apiBaseUrl}${LIVE_HUB_PATH}`)
        .withAutomaticReconnect()
        .configureLogging(LogLevel.Warning)
        .build();
  }

  start(): Promise<void> {
    return this.connection.start();
  }

  stop(): Promise<void> {
    return this.connection.stop();
  }

  joinKingdom(mapKid: number): Promise<void> {
    return this.connection.invoke('JoinKingdom', mapKid);
  }

  leaveKingdom(mapKid: number): Promise<void> {
    return this.connection.invoke('LeaveKingdom', mapKid);
  }

  /** Subscribes to a server event and returns an unsubscribe function. */
  on<TEvent extends keyof LiveClientEvents>(
    event: TEvent,
    handler: LiveClientEvents[TEvent],
  ): () => void {
    this.connection.on(event, handler);
    return () => {
      this.connection.off(event, handler);
    };
  }

  // TODO: expose connection state and rejoin kingdom groups after automatic reconnects.
}
