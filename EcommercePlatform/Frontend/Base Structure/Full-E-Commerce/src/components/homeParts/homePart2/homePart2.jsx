import "./css/HomePart2.css";
import HomeHeader from "../../../Common/homeHeader/homeHeader";
import { FaArrowRight } from "react-icons/fa";
import { FaArrowLeft } from "react-icons/fa";
import { IoPhonePortraitOutline } from "react-icons/io5";
import { IoTvOutline } from "react-icons/io5";
import { IoWatchOutline } from "react-icons/io5";
import { PiGameControllerThin } from "react-icons/pi";
import { CiHeadphones } from "react-icons/ci";
import { CiCamera } from "react-icons/ci";

//swiper
import { Swiper, SwiperSlide } from 'swiper/react';

// Import Swiper styles
import 'swiper/css';
import 'swiper/css/navigation';


import { Navigation } from 'swiper/modules';
import { useState } from "react";


export default function HomePart2()
{
const [actCateg,setActCateg]=useState("headPhones")

const categories=[
    {
        name:"phones",
        icon:<IoPhonePortraitOutline/>
    },
    {
        name:"computers",
        icon:<IoTvOutline/>
    },
    {
        name:"smartWatch",
        icon:<IoWatchOutline/>
    },
     {
        name:"headPhones",
        icon:<CiHeadphones/>
    },
    {
        name:"gaming",
        icon:<PiGameControllerThin/>
    },
    {
        name:"camera",
        icon:<CiCamera/>
    },
    {
        name:"phones",
        icon:<IoPhonePortraitOutline/>
    },
    {
        name:"computers",
        icon:<IoTvOutline/>
    },
    {
        name:"smartWatch",
        icon:<IoWatchOutline/>
    },
     {
        name:"headPhones",
        icon:<CiHeadphones/>
    },
    {
        name:"gaming",
        icon:<PiGameControllerThin/>
    },
    {
        name:"camera",
        icon:<CiCamera/>
    },
   
] 
    return <div className="HomePart2"> 

    <HomeHeader note={"Categories"} title={"Browse By Category"}  >
          <div className="navigation-arrows">
        <div className="arrow arrow-left   "><FaArrowLeft/>  </div>
        <div className="arrow arrow-right"><FaArrowRight/></div>
    </div> 
        </HomeHeader>

<Swiper
  navigation ={{
    nextEl: '.arrow-right', 
    prevEl: '.arrow-left', 
  }}
 modules={[Navigation]} className="mySwiper"
   slidesPerView={6}
   breakpoints={{

    "200":{
        slidesPerView:1
    },
    "455":{
        slidesPerView:2
    },
    "630":{
        slidesPerView:3
    },
    "823":{
        slidesPerView:4
    }
   }}
>
<div className="categories-container">
    {
        categories.map((item,index)=>{
            return( 
             <SwiperSlide key={index}>
   
                <div key={index}  
                onClick={()=>{setActCateg(item.name)}}
                className={`category-item ${actCateg==item.name&&'active'}`}>
<div className={`icon ${actCateg==item.name&&'active'}`}>{item.icon}</div>
        <p>{item.name}</p>
        </div>
            </SwiperSlide>

        );
        })
    }
  
    </div>
    </Swiper>
    </div>
    
}