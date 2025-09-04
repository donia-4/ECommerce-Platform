import './App.css'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from './components/layout/layout';

 function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout/>,
    },
  
  ]);

  return (
  <RouterProvider router={router} />
  );
}

export default App;