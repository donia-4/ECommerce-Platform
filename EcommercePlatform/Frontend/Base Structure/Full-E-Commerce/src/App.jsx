import './App.css'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from './components/layout/layout';
import HomePage from './pages/homePage.jsx';
import Cart from './pages/cart/cart.jsx';

 function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout/>,
    children:[
      {path:"",element:<HomePage/>},
      {path:"cart",element:<Cart/>}
    ]
    },
    ,
{ future: {
    v7_fetcherPersist: true,
  }}
  
  ],
);

  return (
  <RouterProvider router={router} future={{
    v7_startTransition: true,
  }}/>
  );
}

export default App;