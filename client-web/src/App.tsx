import "./App.css";
import HomePage from "./pages/HomePage";
import {
  Route,
  RouterProvider,
  createBrowserRouter,
  createRoutesFromElements,
} from "react-router-dom";
import { Nav } from "./layout/Nav";
import { ShowProduct } from "./pages/ShowProduct";

export default function App() {
  return (
    <>
      <RouterProvider router={Routers} />
    </>
  );
}

export const Routers = createBrowserRouter(
  createRoutesFromElements(
    <>
      <Route path="/" element={<Nav />}>
        <Route path="" element={<HomePage />} />
        <Route path="show-products" element={<ShowProduct />} />
      </Route>

      {/* <Route path="*" element={<Notfound />} />
      <Route path="/login" element={<Login />} />
      <Route path="/register" element={<Register />} /> */}
    </>
  )
);
