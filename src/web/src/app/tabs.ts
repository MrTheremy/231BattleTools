export interface TabDefinition {
  path: string;
  label: string;
}

export const TABS: readonly TabDefinition[] = [
  { path: '/live-map', label: 'Live Map' },
  { path: '/live-activity', label: 'Live Activity' },
  { path: '/battle-comparison', label: 'Battle Comparison' },
  { path: '/analysis', label: 'Analysis & Playback' },
];
