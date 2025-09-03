import HomePart1 from "../components/homeParts/home-part1/homePart1"
import "bootstrap/dist/css/bootstrap.min.css"
import HomePart2 from "../components/homeParts/homePart2/homePart2"
import HomePart3 from "../components/homeParts/home-part3/homePart3"
import HomePart4 from "../components/homeParts/home-part4/homePart4"
import ServicesComponents from "../Common/servicesComponents/serivcesComponents"
import {ProductContext} from "../context/productContext/productContext"
import { useContext, useEffect } from "react"
import SignUp_Api from "../services/APIs/signup.js";
import GenerateToken from "../services/APIs/generateToken.js"
export default function HomePage()
{




    useEffect(()=>{
    },[])


    return <div className="HomePage pe-2 ps-2"> 
{/* Slider-Swiper*/}
 <HomePart1/>
 {/* Categories*/}

<HomePart2/>
 {/* Music poster*/}

<HomePart3/>
 {/* New arrival poster*/}
<HomePart4/>

<ServicesComponents/>

    </div>
    
}