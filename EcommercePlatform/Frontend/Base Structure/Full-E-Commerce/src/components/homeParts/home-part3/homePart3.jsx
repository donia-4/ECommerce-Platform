import "./css/HomePart3.css";
import blur from "../../../assets/images/Ellipse 23.png"
import item from "../../../assets/images/item-img.png"
export default function HomePart3()
{
 
    return <div className="HomePart3"> 
    <div className="container">
<div className="text">
 <div class="category-text">Categories</div>
    <h1 class="title">Enhance Your Music Experience</h1>
    <div class="countdown-container">
        <div class="countdown-item">
            <span class="countdown-number">23</span>
            <span class="countdown-label">Hours</span>
        </div>
        <div class="countdown-item">
            <span class="countdown-number">05</span>
            <span class="countdown-label">Days</span>
        </div>
        <div class="countdown-item">
            <span class="countdown-number">59</span>
            <span class="countdown-label">Minutes</span>
        </div>
        <div class="countdown-item">
            <span class="countdown-number">35</span>
            <span class="countdown-label">Seconds</span>
        </div>
    </div>
    <div class="buy-button">
        <a href="#">Buy Now!</a>
    </div>
</div>

<div className="image">
    <div className="blur">
    </div>
    <div className="item-img">
        <img src={item} alt="Music Experience" />
    </div>
</div>
    </div>

 </div>

}