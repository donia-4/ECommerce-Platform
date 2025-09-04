import React from "react";
import "./DynamicIndex.css"
export default function DynamicIndex({page})
{




    return (
<div className="DynamicIndex">
<p>
{
    page.map((item ,index)=>
        {
        if(index!=page.length-1){ 
        return (
            < span key={index}> 
            
                    {item } /
        
        
            
                         </span>


        )
    }
    })


}




<span>{page[page.length-1]}</span> </p>
</div>

    )
}
 
