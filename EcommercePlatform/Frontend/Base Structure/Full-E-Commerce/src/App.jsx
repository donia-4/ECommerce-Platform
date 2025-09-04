import "./App.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout/layout";
import AboutUs from "./pages/aboutUs/AboutUs"; 

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [{ path: "about", element: <AboutUs /> }],
    },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
