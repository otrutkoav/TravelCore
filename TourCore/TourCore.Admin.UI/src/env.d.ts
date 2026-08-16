/// <reference types="vite/client" />

interface TourCoreRuntimeConfig {
  apiBaseUrl: string
  environmentName?: string
}

interface Window {
  __TOURCORE_CONFIG__?: TourCoreRuntimeConfig
}
