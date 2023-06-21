import { Outlet, NavLink } from "react-router-dom";
import "./stye.css";

export const Nav = () => {
  return (
    <>
      <div className="topNav">
        <div>
          <div></div>
          <div>HachiShop</div>
        </div>
      </div>
      <div className="row">
        <div className="nav" style={{ height: 100 }}>
          <NavLink to="/" className="nav-item">Home</NavLink>
          <NavLink to="show-products" className="nav-item">Product</NavLink>
          <div className="nav-item">Manage</div>
          <div className="nav-item">Dashboard</div>
        </div>
      </div>

      <Outlet />
    </>
  );
};
