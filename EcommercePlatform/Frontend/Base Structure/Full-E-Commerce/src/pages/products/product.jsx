import React, { useContext } from "react";
import ProductCards from "../../components/ProductCards/ProductCards";
import "./product.css";
/* import {ProductProvider_2} from "../../context/ProductContext";
 */
export default function ProductsPage() {
/*   const { products } = useContext(ProductProvider_2
 */  
let products=[];
  return (
    <div>
      <h2>All Products</h2>
      <ProductCards products={products} />
    </div>
  );
}
