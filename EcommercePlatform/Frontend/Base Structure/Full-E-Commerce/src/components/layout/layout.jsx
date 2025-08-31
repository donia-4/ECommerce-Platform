import {Outlet} from "react-router-dom"

export default function Layout()
{

    return <div className="layout">
{/* <Nav/>
 */}


{/*  Children*/}

<Outlet/>

{/*  <Footer/> */} 
   </div>
}