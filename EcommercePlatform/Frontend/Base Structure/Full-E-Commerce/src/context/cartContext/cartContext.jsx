import { createContext, useEffect, useState } from "react";
import ViewCart from "../../services/APIs/viewCart";
import UpdateQunatityCart from "../../services/APIs/update_Quantity_Cart";
import ProductById from "../../services/APIs/get_Product_Id";
import DeleteCart from "../../services/APIs/deleteCart";

export const CartContext = createContext();

export default function CartProvider({ children }) {

 
  
  const [cartItems,setCartItem]=useState([]);
  const [cartInfo,setCartInfo]=useState([]);


   useEffect(()=>{

 (async()=>{
    let dataCart = await ViewCart();
    console.log("data", dataCart);
    
    setCartItem(dataCart.data.items);
    setCartInfo(dataCart.data);
    
    

  
 })()

   },[]) 


async function Quantity_Function( {type,payload})
    {

      
      console.log(payload);
      
   let res= await ProductById(payload.productId);
   console.log(res);
   
   let stock=res.data.stock;
   
   let affected =false;



 switch(type)
    {
         
        case "add": ( payload.quantity < stock) && (
          
          affected=true,
          
          setCartItem(()=>{
           cartItems[payload.index].quantity= payload.quantity+1; return [...cartItems]}) );
        break;
        case "minus": ( cartItems[payload.index].quantity > 1) &&( affected=true, 
          setCartItem(()=>{cartItems[payload.index].quantity= payload.quantity-1 ; 
            return [...cartItems];})  );
               break;

    } 

    if(affected)
    {
      let res =await UpdateQunatityCart({
  "cartItemId":payload.cartItemId,
  "quantity":  payload.quantity+1
});
    let dataCart = await ViewCart();
      
    setCartInfo(dataCart.data);


    }

    }


  async  function Delete_From_Cart(index ,cartId)
    {
      console.log(index);
      console.log(cartId);
      
 cartItems.splice(index,1);
  setCartItem([...cartItems]);
 console.log(cartItems);

let res = await DeleteCart(cartId);
console.log(res); 

``
        
    }



  return (
    <CartContext.Provider value={{cartInfo, cartItems,Quantity_Function ,Delete_From_Cart}}>
      {children}
    </CartContext.Provider>
  );
}