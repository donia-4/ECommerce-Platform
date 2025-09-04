import React,{useState,useEffect} from 'react';
import "./css/Part-2.css"
import axios from 'axios';
import { data } from 'react-router-dom';
export default function Part2()
{const [statistics,setStatistics]=useState([]);
/* async function getStatistics() {
  
  const {data} = await axios.get(import.meta.env.VITE_DASHBOARDMetrices_BASE);
  
 setStatistics(data);  
 }
useEffect(()=>{
getStatistics();

},[])
 */
    return <div className="Part2-Dashboard">

{
    statistics.map((item ,key)=>{
        return  (
        <div className="card-container" key={key}>
        <div className="text-section">
            <h6 className="product-title">{item.title}</h6>
            <p className="price-text">
                <span className="price-value">{item.value}</span>
            </p>
            <p className="sale-text">
                <span className="sale-value">{item.change} Off!</span>
            </p>
            <p className="day-text">
               {item.period}
            </p>
        </div>

        <div className="icon-section">
          <img src={item.image} className='fas fa-tag' alt=''/>
        </div>
    </div>)
    })
}
</div>;
}