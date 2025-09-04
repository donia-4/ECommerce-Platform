import { FaStar, FaStarHalfAlt, FaRegStar } from "react-icons/fa";
import "./RatingStars.css";

export default function RatingStars({ rating }) {
  return (
    <div className="stars">
      {[...Array(5)].map((_, i) => {
        const starValue = i + 1;

        if (rating >= starValue) {
          return <FaStar key={i} className="star-filled" />;
        } else if (rating >= starValue - 0.5) {
          return <FaStarHalfAlt key={i} className="star-half" />;
        } else {
          return <FaRegStar key={i} className="star" />;
        }
      })}
    </div>
  );
}