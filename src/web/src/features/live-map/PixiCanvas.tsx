import { Application } from 'pixi.js';
import { useEffect, useRef } from 'react';

/** Mounts an empty PixiJS stage that fills its container. */
export function PixiCanvas() {
  const hostRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    const host = hostRef.current;
    if (host === null) {
      return;
    }

    const app = new Application();
    let disposed = false;
    let initialized = false;

    void app.init({ resizeTo: host, background: '#10151c', antialias: true }).then(() => {
      initialized = true;
      if (disposed) {
        app.destroy(true);
        return;
      }
      host.appendChild(app.canvas);
      // TODO: render the kingdom map (tiles, map objects, marches) from live world state.
    });

    return () => {
      disposed = true;
      if (initialized) {
        app.destroy(true);
      }
    };
  }, []);

  return <div ref={hostRef} className="pixi-host" data-testid="live-map-canvas" />;
}
