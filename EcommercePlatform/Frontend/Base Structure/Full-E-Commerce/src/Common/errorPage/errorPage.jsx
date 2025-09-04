import { useNavigate } from "react-router-dom";
import DynamicIndex from "../DynamicIndex/DynamicIndex";
import RedButton from "../redButton/redButton";
import "./errorPage.css";


export default function ErrorPage()
{
 
    let Navigate =useNavigate();
    function navTohome() {
    Navigate("/")
}
    return <div replace={true} className="ErrorPage"> 
<DynamicIndex page={["home","error"] } />

<div  className="errorContainer">
    <h3>404 Not Found</h3>
    <p>Your visited page not found. You may go home page</p>
    <RedButton  text={"Back to home page "} btn_Function={navTohome} />

</div>




    </div>
    
}