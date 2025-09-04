
import Button from 'react-bootstrap/Button';
import Modal from 'react-bootstrap/Modal';
import { useContext } from "react";
import { CartContext } from "../../context/cartContext/cartContext";

export default function AlertModal({heading,text,option,Alert_Function})
{

    const {show,handleClose }=useContext(CartContext);
    return (
   <Modal   show={show} onHide={handleClose}>
  

        <Modal.Header closeButton>
          <Modal.Title style={{fontSize:"22px"}}>{heading}</Modal.Title>
        </Modal.Header>
        <Modal.Body style={{fontSize:"12px"}}>{text}</Modal.Body>
        <Modal.Footer>

          <Button style={{fontSize:"10px"}} variant="primary" onClick={()=>{Alert_Function}}>
           {option}
          </Button>
        </Modal.Footer>
      </Modal>
    );

    
}


