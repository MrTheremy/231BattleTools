import { Navigate, type RouteObject } from 'react-router';
import { AnalysisPlaybackPage } from '../features/analysis-playback/AnalysisPlaybackPage';
import { BattleComparisonPage } from '../features/battle-comparison/BattleComparisonPage';
import { LiveActivityPage } from '../features/live-activity/LiveActivityPage';
import { AppLayout } from './AppLayout';

export const routes: RouteObject[] = [
  {
    path: '/',
    element: <AppLayout />,
    children: [
      { index: true, element: <Navigate to="/live-map" replace /> },
      {
        // Lazy so PixiJS is only downloaded when the map is opened.
        path: 'live-map',
        lazy: async () => ({
          Component: (await import('../features/live-map/LiveMapPage')).LiveMapPage,
        }),
      },
      { path: 'live-activity', element: <LiveActivityPage /> },
      { path: 'battle-comparison', element: <BattleComparisonPage /> },
      { path: 'analysis', element: <AnalysisPlaybackPage /> },
      { path: '*', element: <Navigate to="/live-map" replace /> },
    ],
  },
];
