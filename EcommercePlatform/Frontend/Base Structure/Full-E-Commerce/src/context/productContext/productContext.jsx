import { createContext, useState } from "react";

export const ProductContext = createContext();

export default function ProductProvider({ children }) {
  const [products,setProducts] =  useState([
  {
    "id": 1,
    "name": "ASUS FHD Gaming Laptop",
    "price": 700,
    "rating": 5,
    "reviews": 150,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 50,
    "count": 0
  },
  {
    "id": 2,
    "name": "Apple MacBook Pro",
    "price": 1200,
    "rating": 4,
    "reviews": 250,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 30,
    "count": 0
  },
  {
    "id": 3,
    "name": "Dell XPS 13",
    "price": 950,
    "rating": 5,
    "reviews": 180,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 75,
    "count": 0
  },
  {
    "id": 4,
    "name": "HP Spectre x360",
    "price": 1100,
    "rating": 4,
    "reviews": 120,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 40,
    "count": 0
  },
  {
    "id": 5,
    "name": "Lenovo ThinkPad X1 Carbon",
    "price": 1300,
    "rating": 5,
    "reviews": 210,
    "image":"https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 60,
    "count": 0
  }
]);

 




  return (
    <ProductContext.Provider value={{products ,setProducts}}>
      {children}
    </ProductContext.Provider>
  );
}