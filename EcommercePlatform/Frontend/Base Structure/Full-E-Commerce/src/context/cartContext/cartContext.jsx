import { createContext, useState } from "react";

export const CartContext = createContext();

export default function CartProvider({ children }) {

 const [show, setShow] = useState(false);

  const handleClose = () => setShow(false);
  const handleShow = () => setShow(true);   
  
  const [cart,setCart]=useState([
  {
    "id": 1,
    "name": "ASUS FHD Gaming Laptop",
    "price": 700,
    "rating": 5,
    "reviews": 150,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 50,
    "count": 1
  },
  {
    "id": 2,
    "name": "Apple MacBook Pro",
    "price": 1200,
    "rating": 4,
    "reviews": 250,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 30,
    "count": 1
  },
  {
    "id": 3,
    "name": "Dell XPS 13",
    "price": 950,
    "rating": 5,
    "reviews": 180,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 75,
    "count": 1
  },
  {
    "id": 4,
    "name": "HP Spectre x360",
    "price": 1100,
    "rating": 4,
    "reviews": 120,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 40,
    "count": 1
  },
  {
    "id": 5,
    "name": "Lenovo ThinkPad X1 Carbon",
    "price": 1300,
    "rating": 5,
    "reviews": 210,
    "image": "https://m.media-amazon.com/images/I/71WuDXpTAlL._AC_SL1500_.jpg",
    "stock": 60,
    "count": 1
  }
]);
  

 function Quantity_Function( {type,payload})
    {
      
 switch(type)
    {
         
        case "add": ( cart[payload.index].count < payload.stock) && setCart(()=>{cart[payload.index].count++; return [...cart]}) ;
        break;
        case "minus": ( cart[payload.index].count > 1) && setCart(()=>{cart[payload.index].count-- ; 
            return [...cart];})  ;
               break;

    } 

    }


    function Delete_From_Cart(index)
    {
cart.splice(index,1);
setCart([...cart])
        
    }



  return (
    <CartContext.Provider value={{ cart,Quantity_Function ,Delete_From_Cart,show,handleClose,handleShow}}>
      {children}
    </CartContext.Provider>
  );
}