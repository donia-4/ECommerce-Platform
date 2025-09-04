import HomeHeader from "../../../Common/homeHeader/homeHeader";
import "./css/homePart4.css";
import playstation from "../../../assets/images/playstation.png"
import sub from "../../../assets/images/sub.png"
import perfum from "../../../assets/images/perfum.png"
import women from "../../../assets/images/women.png"

export default function HomePart4()
{
 
    return <div className="HomePart4"> 
<HomeHeader note={"Feature"} title={"Bew Arrival"}/>

<div className="container">

<div className="left ">

    <div className="image">

    <img src={playstation} alt=""/>

    <div className="text">
            <div class="space-y-4 max-w-lg">
        <h3 class="text-6xl font-bold tracking-tight md:text-8xl">
            PlayStation 5
        </h3>
        <p class="text-lg md:text-2xl font-light">
            Black and White version of the PS5 coming out on sale.
        </p>
        <p className="link">

        <a href="#" class="inline-block mt-8 text-xl md:text-3xl font-normal border-b-2 border-white pb-1">
            Shop Now
        </a>
        </p>
    </div>
    </div>
    </div>
</div>

<div className="right">

<div className="top image">
    <img src={women} alt=""/>
     <div className="text">
            <div class="space-y-4 max-w-lg">
        <h3 class="text-6xl font-bold tracking-tight md:text-8xl">
           Women’s Collections
        </h3>
        <p class="text-lg md:text-2xl font-light">
Featured woman collections that give you another vibe.        </p>
        <p className="link">

        <a href="#" class="inline-block mt-8 text-xl md:text-3xl font-normal border-b-2 border-white pb-1">
            Shop Now
        </a>
        </p>
    </div>
    </div>
</div>

<div className="down">

    <div className="one image">
                <div className="blur"></div>
        <div className="img-item">
        <img src={sub} alt=""/>
      
</div>
   <div className="text">
            <div class="space-y-4 max-w-lg">
        <h3 class="text-6xl font-bold tracking-tight md:text-8xl">
          Speakers
        </h3>
        <p class="text-lg md:text-2xl font-light">
Amazon wireless speakers        </p>
        <p className="link">

        <a href="#" class="inline-block mt-8 text-xl md:text-3xl font-normal border-b-2 border-white pb-1">
            Shop Now
        </a>
        </p>
    </div>
    </div>
    </div>
    <div className="two image">
        <div className="blur"></div>
        <div className="img-item">

                <img src={perfum} alt=""/>
        </div>

     <div className="text">
            <div class="space-y-4 max-w-lg">
        <h3 class="text-6xl font-bold tracking-tight md:text-8xl">
         Perfume
        </h3>
        <p class="text-lg md:text-2xl font-light">
GUCCI INTENSE OUD EDP     </p>
        <p className="link">

        <a href="#" class="inline-block mt-8 text-xl md:text-3xl font-normal border-b-2 border-white pb-1">
            Shop Now
        </a>
        </p>
    </div>
    
    </div>


    </div>
</div>
</div>
</div>
 </div>

}