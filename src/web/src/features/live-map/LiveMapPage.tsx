import { PixiCanvas } from './PixiCanvas';

export function LiveMapPage() {
  return (
    <section className="page page--fill" aria-labelledby="live-map-title">
      <h1 id="live-map-title">Live Map</h1>
      <PixiCanvas />
    </section>
  );
}
