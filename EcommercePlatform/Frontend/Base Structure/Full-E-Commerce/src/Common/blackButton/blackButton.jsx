import "./blackButton.css";


export default function BlackButton({text,btn_Function})
{


 
    return <div className="BlackButton"> 
 <button className="    rounded-md  " onClick={()=>{btn_Function}}>
{text} 

   </button>    </div>
    
}


  
