import { describe, expect, it } from 'vitest';
import { normalizeBaseUrl } from './config';

describe('normalizeBaseUrl', () => {
  it('falls back to the local backend', () => {
    expect(normalizeBaseUrl(undefined)).toBe('http://localhost:5080');
    expect(normalizeBaseUrl('  ')).toBe('http://localhost:5080');
  });

  it('strips trailing slashes', () => {
    expect(normalizeBaseUrl('https://api.example.test//')).toBe('https://api.example.test');
  });
});
