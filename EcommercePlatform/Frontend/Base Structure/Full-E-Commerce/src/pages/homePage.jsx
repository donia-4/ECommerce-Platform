import HomePart1 from "../components/homeParts/home-part1/homePart1"
import "bootstrap/dist/css/bootstrap.min.css"
import HomePart2 from "../components/homeParts/homePart2/homePart2"
import HomePart3 from "../components/homeParts/home-part3/homePart3"
import HomePart4 from "../components/homeParts/home-part4/homePart4"
import ServicesComponents from "../Common/servicesComponents/serivcesComponents"
import { useContext, useEffect } from "react"
import { UserContext } from "../context/userContext/userContext.jsx"
export default function HomePage()
{

    const{UserDataSetting}=useContext(UserContext);
window.dispatchEvent(new Event('localStorageChange'));
    useEffect(()=>{


  window.addEventListener('localStorageChange',()=>{window.location.reload()} );
  return () => window.removeEventListener('localStorageChange', UserDataSetting());
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