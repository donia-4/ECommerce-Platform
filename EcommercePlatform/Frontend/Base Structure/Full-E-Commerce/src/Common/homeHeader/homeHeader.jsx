import "./css/homeHeader.css";


export default function HomeHeader({note,title ,children})
{
 
    return <div className="HomeHeader"> 

    <div class="title-section">
        <div class="title-text">
            <p>{note}
<span className="title-icon"></span>
                
            </p>
            <h2>{title}</h2>
        </div>
    </div>


    {children}
   
    </div>
    
}