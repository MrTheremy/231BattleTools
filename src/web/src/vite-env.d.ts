/// <reference types="vite/client" />

interface ImportMetaEnv {
  /** Base URL of the backend, e.g. http://localhost:5080. */
  readonly VITE_API_BASE_URL?: string;
}

interface ImportMeta {
  readonly env: ImportMetaEnv;
}
