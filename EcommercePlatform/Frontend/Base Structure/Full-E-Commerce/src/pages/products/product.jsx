import React, { useContext } from "react";
import ProductCards from "../../components/ProductCards/ProductCards";
import { ProductContext } from "../../context/ProductContext";
import "./product.css";

export default function ProductsPage() {
  const { products } = useContext(ProductContext);

  return (
    <div>
      <h2>All Products</h2>
      <ProductCards products={products} />
    </div>
  );
}
