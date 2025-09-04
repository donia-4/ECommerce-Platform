import "./totalDetails.css";


export default function TotalDetails({total, subTotal})
{
 
    return <div className="TotalDetails"> 
<p>Subtotal <span>{subTotal}</span></p>
<p>Shipping <span>Free</span></p>
<p>Total<span>{total}</span></p>

    </div>
    
}