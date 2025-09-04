import "./css/HomePart1.css"

// Import Swiper React components
import { Swiper, SwiperSlide } from 'swiper/react';
import 'swiper/css';
import 'swiper/css/pagination';
import 'swiper/css/effect-fade';
import 'swiper/css/navigation';

// import required modules
import { Pagination ,Autoplay,EffectFade, } from 'swiper/modules';

// sales image
import drone from "../../../assets/images/swiperImages/drone.png"
import headphone from "../../../assets/images/swiperImages/headPhone.png"
import phone from "../../../assets/images/swiperImages/phone.png"
import labtop from "../../../assets/images/swiperImages/labtop.png"
import dji from "../../../assets/images/swiperImages/logo/dji.png"
import aur from "../../../assets/images/swiperImages/logo/aur.png"
import kodak from "../../../assets/images/swiperImages/logo/kodak.png"
import quantum from "../../../assets/images/swiperImages/logo/quantum.png"
import apple from "../../../assets/images/swiperImages/logo/apple.png"



import { FaArrowRight } from "react-icons/fa";
import { Link } from "react-router-dom";
export default function HomePart1()
{
    const avaCateg=["Woman's Fashion","Men's Fashion","Electronics","Home & Lifestyle","Medicine","Sports & Outdoor",
        "Baby's & Toys","Gorceries & Pets","Healty & Beauty"];
    
const saleImages=[

  {
    "product_name": "iPhone",
    "version": "Pro 14",
    "company_logo": apple,
    "image":phone,
    "estimated_sales":10
  },
  {
    "product_name": "Drone",
    "version": "v3.1",
    "company_logo": dji,
    "image":drone,
    "estimated_sales":20

  },
  {
    "product_name": "Headphones",
    "version": "Aura X",
    "company_logo": aur,
    "image":headphone,
     "estimated_sales":10

  },

  {
    "product_name": "Laptop",
    "version": "Infinity Pro",
    "company_logo": quantum,
    "image":labtop,
      "estimated_sales":15

  }
]



 
    return <div className="HomePart1"> 
<div className="left">
    <ul>

{/*         Avaliable Categ

 */}   
 <img src=" " />
 {
avaCateg.map((item,index)=>{

    return (<li key={index}>{item}</li>)
})
      
 }
        
    </ul>
</div>
<div className="right">

   <Swiper
   spaceBetween={30}
        effect={'fade'}
        loop={true}
        autoplay={{
     delay: 1500,
   
} }
   speed={1000}
        pagination={{
          clickable: true,
        }}
        modules={[EffectFade,  Pagination,Autoplay]}
        className="mySwiper"

  
      >
        {saleImages.map((item ,index)=>{return(

       <SwiperSlide key={index}>
<div className="container">
    <div className="text">
{/*         Start of 1 div
 */} 
        <div>
        <div className="logo">
<img src={item.company_logo} alt=""/>

        </div>
<p>{item.product_name}  {item.version} </p>
        </div>
{/*         End of 1 div
 */}

 {/*         Start of h1
 */}

<h1>Up to {item.estimated_sales}% off voucher </h1>

 {/*         End of h1
 */}

<div className="ShopNow">
    
<p > <Link to="/products">Shop Now </Link>  </p>
<span className="icon"><FaArrowRight/></span>
</div>

    </div>
    <div className="image">
<img src={item.image} alt={item.product_name}/>
    </div>
</div>
</SwiperSlide>
        );})
 
        }

</Swiper>


</div>

 </div>
}