import './App.css'
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from './components/layout/layout';
import HomePage from './pages/homePage.jsx';
import Cart from './pages/cart/cart.jsx';
import Signup from './pages/signup/signup.jsx';
import Login from './pages/login/login.jsx';
import Dashboard from './dashboard/dashboardPage.jsx';
import DashboardLayout from './components/layout/dashboard-layout.jsx';
import ErrorPage from './Common/errorPage/errorPage.jsx';


 function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout/>,
            errorElement:<ErrorPage/>,

    children:[
      {path:"",element:<HomePage/>},
      {path:"cart",element:<Cart/>},
      {path:"signup",element:<Signup/>},
      {path:"login",element:<Login/>},
      {path:"productDetails/:id",element:<Login/>},
      { path: "*", element: <ErrorPage /> }

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