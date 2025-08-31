import {Outlet} from "react-router-dom"
import Nav from "../../Common/nav/nav";
export default function Layout()
{

    return <div className="layout">
 <Nav/>
 


{/*  Children*/}

<Outlet/>

{/*  <Footer/> */} 
   </div>
}