import { NavLink, Outlet } from 'react-router';
import { TABS } from './tabs';

export function AppLayout() {
  return (
    <div className="app">
      <header className="app__header">
        <span className="app__brand">Kingshot Battle Tools</span>
        <nav aria-label="Main">
          <ul className="tabs">
            {TABS.map((tab) => (
              <li key={tab.path}>
                <NavLink to={tab.path} className="tabs__link">
                  {tab.label}
                </NavLink>
              </li>
            ))}
          </ul>
        </nav>
      </header>
      <main className="app__main">
        <Outlet />
      </main>
    </div>
  );
}
