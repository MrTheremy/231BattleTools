import '@testing-library/jest-dom/vitest';
import { cleanup } from '@testing-library/react';
import { afterEach, vi } from 'vitest';

// jsdom has no WebGL; replace PixiJS with a no-op Application for component tests.
vi.mock('pixi.js', () => ({
  Application: class {
    canvas = document.createElement('canvas');
    init() {
      return Promise.resolve();
    }
    destroy() {
      // no-op
    }
  },
}));

afterEach(() => {
  cleanup();
});
