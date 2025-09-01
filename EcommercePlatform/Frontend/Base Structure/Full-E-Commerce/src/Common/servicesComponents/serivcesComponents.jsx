import "./css/servicesComponents.css";
import { FaTruckFast } from "react-icons/fa6";
import { TfiHeadphoneAlt } from "react-icons/tfi";
import { AiFillSafetyCertificate } from "react-icons/ai";

export default function ServicesComponents()
{
 
const services=[
    {
        title:"FREE AND FAST DELIVERY",
        subtitle:"Free delivery for all orders over $140",
        icon:<FaTruckFast/>
    },
    {
        title:"24/7 CUSTOMER SERVICE",
        subtitle:"Friendly 24/7 customer support",
        icon:<TfiHeadphoneAlt/>
    },
    {
        title:"MONEY BACK GUARANTEE",
        subtitle:"We reurn money within 30 days",
        icon:<AiFillSafetyCertificate/>
    },
]

    return <div className="ServicesComponents"> 

    {
        services.map((item , key )=>{
            return(<div key={key} class="card">
        <div class="icon-container">
            <span>

           {item.icon}
            </span>
        </div>
        <h2 class="title">{item.title}</h2>
        <p class="subtitle">{item.subtitle}</p>
    </div>);
        })
    }
 
        </div>
    
}