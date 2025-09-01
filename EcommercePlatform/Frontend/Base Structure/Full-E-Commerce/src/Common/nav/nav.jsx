
import { Link, NavLink } from "react-router-dom";
import "./css/nav.css";
import { IoIosArrowDown } from "react-icons/io";
import { AiOutlineHeart } from "react-icons/ai";
import { PiShoppingCartLight } from "react-icons/pi";
import { IoSearchOutline } from "react-icons/io5";
export default function Nav()
{
 
    return <nav>
<div className="top">


    <p>Summer Sale For All Swim Suits And Free Express Delivery - OFF 50%!
        <Link to="" >ShopNow</Link ></p>
        <p>English <span><IoIosArrowDown/></span></p>


</div>
<div className="bottom">
<div>
    <p>Exclusive</p>
</div>
<div>
   <NavLink to="">Home</NavLink>
   <NavLink to="/contact">Contact</NavLink>
   <NavLink to="/about">About</NavLink>
   <NavLink to="/signup">Sign Up   </NavLink>
    
</div>

<div>
    <div className="search">
        <input type="search" placeholder="What are u looking for?" />
      <span>  <IoSearchOutline/></span>
        
        </div>
<div className="links">
    <Link to="/whishlist" className="wishList"><AiOutlineHeart/></Link>
    <Link to="/cart" className="Cart"><PiShoppingCartLight/></Link>
</div>
</div>

</div>
    </nav>
    
}