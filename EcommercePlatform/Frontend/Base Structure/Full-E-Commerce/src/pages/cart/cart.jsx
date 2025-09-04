import "./css/cart.css";
import DynamicIndex from  "../../Common/DynamicIndex/DynamicIndex"
import BlackButton from "../../Common/blackButton/blackButton";
import { useContext, useReducer, useRef, useState } from "react";
import { CartContext } from "../../context/cartContext/cartContext";
import TotalDetails from "../../Common/totalDetails/totalDetails";
import RedButton from "../../Common/redButton/redButton";

import CartProduct from "../../Common/cartProduct/cartProduct";
import empty from "../../assets/images/icons/icons8-empty-100.png";
import { Link } from "react-router-dom";
export default function Cart()
{
  const {cartItems,cartInfo } = useContext(CartContext);


  

   

    return <div className="Cart-container "> 
    

     <DynamicIndex page={["Home","Cart"]} />
     {
!cartItems.length?
<div style={{marginLeft:"50%"}}>
<img src={empty}  alt="empty" />
<p style={{marginTop:"20px"}}>Go To <Link style={{color:"red"}} to={"/"}>Home</Link></p>
</div>
:

<div className="container cart-container ">
<div className="cart-wrapper">
        <table className="cart-table">
            <thead>
                <tr>
                    <th>Product</th>
                    <th>Price</th>
                    <th>Quantity</th>
                    <th>Subtotal</th>
                </tr>
            </thead>
            <tbody>

                {





cartItems.map((item,index)=>{

return(<CartProduct
item={item}
key={index}
index={index}


/>);

})
                }
                
            </tbody>
        </table>
        <div className="buttons">

<BlackButton text={"Return to shop"}/>


    </div>

    </div>
    
    <div className="right">
    <div className="total">
        <p>Cart total</p>

<TotalDetails total={cartInfo.total} subTotal={cartInfo.subtotal} />
<RedButton text="Processed to check"/>
    </div>

    <div className="coupoun">
        <input type="text" placeholder="Coupoun"  />
        <RedButton text="Apply Coupoun"/>
    </div>
    </div>

        </div>
}
        </div>
}