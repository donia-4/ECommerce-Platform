import "./App.css";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout/layout";
import Account from "./pages/account/account";

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [{ path: "account", element: <Account /> }],
    },
  ]);

  return <RouterProvider router={router} />;
}

export default App;
