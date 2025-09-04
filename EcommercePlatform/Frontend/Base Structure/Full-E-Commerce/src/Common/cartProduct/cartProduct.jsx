import React, { useContext, useState } from 'react';
import { Button, Modal } from 'react-bootstrap';
import { CartContext } from '../../context/cartContext/cartContext';

export default function CartProduct  ({ item , index })  {
    const [show, setShow] = useState(false);

    const  {Quantity_Function,Delete_From_Cart} = useContext(CartContext);
    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);

    return (
        <tr >
            <td className="product-cell">
                <div className="product-info">
                    <img src={item.imageUrl} alt="product" className="product-image" />
                    <span className="product-name">{item.productName}</span>
                </div>
            </td>
            <td className="price-cell">{item.unitPrice}</td>
            <td className="quantity-cell">
                <div className="quantity-control">
                    <input type="text" value={item.quantity} className="quantity-input" readOnly />
                    <div className="quantity-arrows">
                        <div className="arrow-up" onClick={() => Quantity_Function({ type: "add", payload: { "quantity": item.quantity, "productId": item.productId, "index": index, "cartItemId": item.cartItemId } })}></div>
                        <div className="arrow-down" onClick={() => Quantity_Function({ type: "minus", payload: { "quantity": item.quantity, "productId": item.productId, "index": index, "cartItemId": item.cartItemId } })}></div>
                    </div>
                </div>
            </td>
            <td className="subtotal-cell">{(item.subtotal * item.quantity).toFixed(2)}</td>
            <td>
                <Button variant="primary" className="btn remove-icon" onClick={handleShow}>
                    ✖
                </Button>
                <Modal show={show} onHide={handleClose}>
                    <Modal.Header closeButton>
                        <Modal.Title style={{ fontSize: "22px" }}>Delete</Modal.Title>
                    </Modal.Header>
                    <Modal.Body style={{ fontSize: "12px" }}>Are you sure you want to delete <span style={{ fontWeight: "bold" }}>{item.productName}</span></Modal.Body>
                    <Modal.Footer>
                        <Button style={{ fontSize: "12px", background: "var(--red-color)", outline: "none", border: "none", padding: "8px 20px" }} variant="primary" onClick={() => { handleClose(); Delete_From_Cart(index, item.cartItemId); }}>
                            Delete
                        </Button>
                    </Modal.Footer>
                </Modal>
            </td>
        </tr>
    );
};

