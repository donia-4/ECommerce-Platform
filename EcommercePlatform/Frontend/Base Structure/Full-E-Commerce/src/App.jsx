import React from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from './components/layout/layout';
import HomePage from './pages/homePage.jsx';
import Cart from './pages/cart/cart.jsx';
import Signup from './pages/signup/signup.jsx';
import Login from './pages/login/login.jsx';
import ErrorPage from './Common/errorPage/errorPage.jsx';
import ProductsPage from "./pages/products/product.jsx";
import ProductDetail from "./pages/productDetail/productDetail.jsx";

export default function App() {
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
      { path: "product", element: <ProductsPage /> },
      { path: "product/:id", element: <ProductDetail /> },
      { path: "*", element: <ErrorPage /> },
      
     

    ]
    },

  
    ,
{ future: {
    v7_fetcherPersist: true,
  }}
  
  ],
);

return <RouterProvider router={router}>

</RouterProvider>
}
