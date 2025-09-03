import "./css/cart.css";
import DynamicIndex from  "../../Common/DynamicIndex/DynamicIndex"
import BlackButton from "../../Common/blackButton/blackButton";
import { useContext, useReducer, useRef, useState } from "react";
import { CartContext } from "../../context/cartContext/cartContext";
import { Button } from "flowbite-react"
import Modal from 'react-bootstrap/Modal';
import TotalDetails from "../../Common/totalDetails/totalDetails";
import RedButton from "../../Common/redButton/redButton";
export default function Cart()
{
  const {cart ,Quantity_Function, handleShow,Delete_From_Cart,handleClose,show} = useContext(CartContext);

   

    return <div className="Cart"> 
    

     <DynamicIndex page={["Home","Cart"]} />

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
cart.map((item,index)=>{

return(<tr key={index}>
                    <td className="product-cell">
                        <div className="product-info">
                            <img src={item.image} alt="LCD Monitor" className="product-image"/>
                            <span className="product-name">{item.name}</span>
                        </div>
                    </td>
                    <td className="price-cell">{item.price}</td>
                    <td className="quantity-cell">
                        <div className="quantity-control">
                            <input type="text" value={item.count}   className="quantity-input"/>
                            <div className="quantity-arrows" >
                                <div className="arrow-up" onClick={()=>{Quantity_Function({type:"add" , payload:{"stock": item.stock ,"index": index } })}} ></div>
                                <div className="arrow-down" onClick={()=>{Quantity_Function({type:"minus" ,payload:{"stock": item.stock , "index": index }  })}}></div>
                            </div>
                        </div>
                    </td>
                    <td className="subtotal-cell">{ (item.price* item.count).toFixed(2)}</td>
                    <td >  
  <Button variant="primary" className="btn remove-icon" onClick={handleShow}>
✖      </Button>


   <Modal   show={show} onHide={handleClose}>
        <Modal.Header closeButton>
          <Modal.Title style={{fontSize:"22px"}}>Delete</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{fontSize:"12px"}}>Are u sure want to delete {item.name}</Modal.Body>
        <Modal.Footer>

          <Button style={{fontSize:"10px;",background:"var(--red-color)",outline:"none",border:"none",padding:"8px 0px"}}  variant="primary" onClick={()=>{handleClose(); Delete_From_Cart(index)}}>

Delete
          </Button>
        </Modal.Footer>
      </Modal>   



</td>
                </tr>);

})
                }
                
            </tbody>
        </table>
        <div className="buttons">

<BlackButton text={"Return to shop"}/>

<BlackButton text={"Update cart "}/>

    </div>

    </div>
    
    <div className="right">
    <div className="total">
        <p>Cart total</p>

<TotalDetails />
<RedButton text="Processed to check"/>
    </div>

    <div className="coupoun">
        <input type="text" placeholder="Coupoun"  />
        <RedButton text="Apply Coupoun"/>
    </div>
    </div>

        </div>
    
        </div>
}