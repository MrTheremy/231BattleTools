const DEFAULT_API_BASE_URL = 'http://localhost:5080';

/** Strips trailing slashes so callers can append paths like `/hubs/live`. */
export function normalizeBaseUrl(url: string | undefined): string {
  const value = url?.trim();
  return (value === undefined || value === '' ? DEFAULT_API_BASE_URL : value).replace(/\/+$/, '');
}

export const config = {
  apiBaseUrl: normalizeBaseUrl(import.meta.env.VITE_API_BASE_URL),
} as const;
