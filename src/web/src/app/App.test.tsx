import { render, screen } from '@testing-library/react';
import userEvent from '@testing-library/user-event';
import { createMemoryRouter, RouterProvider } from 'react-router';
import { describe, expect, it } from 'vitest';
import { routes } from './routes';
import { TABS } from './tabs';

function renderAt(path: string) {
  const router = createMemoryRouter(routes, { initialEntries: [path] });
  render(<RouterProvider router={router} />);
  return router;
}

describe('app shell', () => {
  it('shows the four tabs', async () => {
    renderAt('/live-activity');

    const nav = await screen.findByRole('navigation', { name: 'Main' });
    expect(nav).toBeInTheDocument();
    for (const tab of TABS) {
      expect(screen.getByRole('link', { name: tab.label })).toHaveAttribute('href', tab.path);
    }
  });

  it('redirects / to the live map with its canvas host', async () => {
    const router = renderAt('/');

    expect(await screen.findByRole('heading', { name: 'Live Map' })).toBeInTheDocument();
    expect(screen.getByTestId('live-map-canvas')).toBeInTheDocument();
    expect(router.state.location.pathname).toBe('/live-map');
  });

  it.each([
    ['Live Activity', 'Live Activity'],
    ['Battle Comparison', 'Battle Comparison'],
    ['Analysis & Playback', 'Analysis & Playback'],
  ])('navigates to %s', async (label, heading) => {
    renderAt('/live-map');
    await screen.findByRole('heading', { name: 'Live Map' });

    await userEvent.click(screen.getByRole('link', { name: label }));

    expect(await screen.findByRole('heading', { name: heading })).toBeInTheDocument();
  });
});
