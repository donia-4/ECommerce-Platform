
import { Link, NavLink } from "react-router-dom";
import "./css/nav.css";
import { IoIosArrowDown } from "react-icons/io";
import { AiOutlineHeart } from "react-icons/ai";
import { PiShoppingCartLight } from "react-icons/pi";
import { IoSearchOutline } from "react-icons/io5";

import { MdMenu } from "react-icons/md";
import { useContext, useState } from "react";
import { UserContext } from "../../context/userContext/userContext";
import Logout from "../../services/APIs/logout";
import { ProductContext } from "../../context/productContext/productContext";
export default function Nav()
{
const {isLogin} =useContext(UserContext);  
 const {products} =useContext(ProductContext)
 const [openSearch ,setOpenSearch]=useState(false);
    return <nav>
<div className="top">


    <p>Summer Sale For All Swim Suits And Free Express Delivery - OFF 50%!
        <Link to="" >ShopNow</Link ></p>
        <p>English <span><IoIosArrowDown/></span></p>


</div>
<div className="bottom">
<div>
    <p style={{margin:"0px"}}>Exclusive</p>
</div>
<div className="nav_links">
   <NavLink to="">Home</NavLink>
   <NavLink to="/contact">Contact</NavLink>
   <NavLink to="/about">About</NavLink>
      {
isLogin()?
<NavLink  onClick={()=>{ console.log(
Logout()
);
 }} to="/login">Logout   </NavLink>
       :
<NavLink  to="/signup">Sign Up   </NavLink>
    }
    
</div>

<div>
    <div className="search">
        <input  onFocus={()=>{setOpenSearch(true)}} 
        onBlur={()=>{setOpenSearch(false)}}
        type="search" placeholder="What are u looking for?" />
      <span>  <IoSearchOutline/></span>
        <div className={`blankSearch ${openSearch?"active":""}`}  >

            {
                
                products.map((item ,index)=>{
                    return(
                        <Link key={index} to={`product/${item.id}`}>{item.name}</Link>

                    )
                })
            }

        </div>

        </div>
<div className="links">

    {
        isLogin()?
        <>
         <Link to="/whishlist"  className="wishList"><AiOutlineHeart/></Link>
    <Link to="/cart" className="Cart"><PiShoppingCartLight/></Link>
    </>
    :
    <>
    <Link to="/signup" className="wishList"><AiOutlineHeart/></Link>
    <Link to="/signup" className="Cart"><PiShoppingCartLight/></Link>  
    </>   
    }
   
    <div class="nav-item dropdown" style={{position:"unset",display:"none"}}>
        <a class="nav-link dropdown-toggle" href="#" id="navbarDropdownMenuLink" role="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
<MdMenu style={{fontSize:"20px"}}/>
        </a>
        <div class="dropdown-menu" style={{position:"absolute",zIndex:555}} aria-labelledby="navbarDropdownMenuLink">
         
            <NavLink className="dropdown-item" to="">Home</NavLink>
   <NavLink className="dropdown-item" to="/contact">Contact</NavLink>
   <NavLink className="dropdown-item" to="/about">About</NavLink>
   {
isLogin()?
<NavLink className="dropdown-item" onClick={()=>{Logout()}} to="/login">Logout   </NavLink>
       :
<NavLink className="dropdown-item" to="/signup">Sign Up   </NavLink>
    }

        </div>
      </div>
</div>
    

</div>

</div>
    </nav>
    
}