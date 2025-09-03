import "./redButton.css";


export default function RedButton({text,btn_Function})
{


 
    return <div className="RedButton"> 
 <button className="    rounded-md  " onClick={()=>{btn_Function}}>
{text} 

   </button>    </div>
    
}


  
