import React, { useContext, useEffect, useState } from 'react';
import "./css/Part-4.css"
import { PieChart } from '@mui/x-charts/PieChart';
export default function Part4()
{

/* const {CategoriesApi} = useContext(FetchApIsContext);
 */ const[categories,setCategories]=useState([]);
 const[pieData,setPieData]=useState([]);
/*  
  async function  categoryFetch() {
    const data=await CategoriesApi()
    setCategories(data);
    

  } */

/*   useEffect(()=>{
categoryFetch();

  },[]); */

/*   useEffect(()=>{
console.log(pieData);
setData();


  },[categories]);
 */


/* 
function setData()
{

  let data=[];
categories.forEach(element => {
data.push({label:element.category_name,value:element.number_of_items});



});
setPieData(data);  

}
 */
const settings = {
  margin: { right: 5 },
  width:550,
  height: 300,
  hideLegend: true,
};
    return <div className="Part4">

<header>
  <h6>Products Overview</h6>
  <p>Currently Stock</p>
</header>
{
  (pieData.length!=0)
  ?
  <PieChart
  series={[
    {
      startAngle: -90,
      endAngle: 90,
      data:pieData,
outerRadius:120
    },
  ]}
  height={300}
  width={450}
/>  
    :
    ""
    }

    
    </div>;
}