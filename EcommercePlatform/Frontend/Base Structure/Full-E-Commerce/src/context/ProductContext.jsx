import { createContext } from "react";

export const ProductContext_2 = createContext();

export default function ProductProvider_2({ children }) {
  const products = [
    {
      id: 1,
      name: "ASUS FHD Gaming Laptop",
      price: 200,
      rating: 4,
      reviews: 40,
      image: "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
      description:
        " Lorem ipsum dolor, sit amet consectetur adipisicing elit. Aliquam nobis ea necessitatibus esse itaque eum nemo explicabo officia voluptates sed!",
    },
    {
      id: 2,
      name: "ASUS FHD Gaming Laptop",
      price: 400,
      rating: 3,
      reviews: 150,
      image: "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
      description:
        " Lorem ipsum dolor, sit amet consectetur adipisicing elit. Aliquam nobis ea necessitatibus esse itaque eum nemo explicabo officia voluptates sed!",
    },
    {
      id: 3,
      name: "ASUS FHD Gaming Laptop",
      price: 700,
      rating: 3.5,
      reviews: 150,
      image: "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
      description:
        " Lorem ipsum dolor, sit amet consectetur adipisicing elit. Aliquam nobis ea necessitatibus esse itaque eum nemo explicabo officia voluptates sed!",
    },
    {
      id: 4,
      name: "ASUS FHD Gaming Laptop",
      price: 700,
      rating: 5,
      reviews: 150,
      image: "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
      description:
        " Lorem ipsum dolor, sit amet consectetur adipisicing elit. Aliquam nobis ea necessitatibus esse itaque eum nemo explicabo officia voluptates sed!",
    },
    {
      id: 5,
      name: "ASUS FHD Gaming Laptop",
      price: 700,
      rating: 5,
      reviews: 150,
      image: "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
      description:
        "Lorem ipsum dolor, sit amet consectetur adipisicing elit. Aliquam nobis ea necessitatibus esse itaque eum nemo explicabo officia voluptates sed!",
    },
  ];

  return (
    <ProductContext_2.Provider value={{ products }}>
      {children}
    </ProductContext_2.Provider>
  );
}
