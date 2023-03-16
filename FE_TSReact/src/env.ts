/// <reference types="vite/client" />

interface ImportMetaEnv {
    readonly VITE_APP_APIHOST: string;
  }

  interface ImportMeta {
    readonly env: ImportMetaEnv;
  }