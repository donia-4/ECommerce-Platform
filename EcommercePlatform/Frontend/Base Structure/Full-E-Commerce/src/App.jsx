import './App.css'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from './components/layout/layout';
import HomePage from './pages/HomePAge';

 function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout/>,
    children:[
      {path:"",element:<HomePage/>}
    ]
    },
  
  ]);

  return (
  <RouterProvider router={router} />
  );
}

export default App;