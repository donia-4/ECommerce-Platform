import React from "react";
import { RouterProvider, createBrowserRouter } from "react-router-dom";
import Layout from "./components/layout/layout";
import ProductsPage from "./pages/products/product";
import ProductProvider from "./context/ProductContext";
import ProductDetail from "./pages/productDetail/productDetail";

export default function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout />,
      children: [
        { path: "/product", element: <ProductsPage /> },
        { path: "/product/:id", element: <ProductDetail /> },
      ],
    },
  ]);

  return (
    <ProductProvider>
      <RouterProvider router={router} />
    </ProductProvider>
  );
}
