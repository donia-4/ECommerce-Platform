import { FaHeart } from "react-icons/fa";
import RatingStars from "../RatingStars/RatingStars";
import {Link} from "react-router-dom"
import "./ProductCards.css";

export default function ProductCards({ products }) {
  return (
    <div className="products-container">
      {products.map((product) => (
        <div key={product.id}>
          <div className="product-card">
            <div className="fav-icon">
              <FaHeart />
            </div>
            <Link to = {`/product/${product.id}`}>
            <img src={product.image} alt={product.name} />
            <button className="add-to-cart">Add to Cart</button>
            </Link>
          </div>

          <div className="product-info">
            <h3>{product.name}</h3>
            <div className="price-rate">
              <span className="price">${product.price}</span>
            </div>

            <div className="stars">
              <RatingStars rating={product.rating} />
              <span className="reviews">({product.reviews} reviews)</span>
            </div>
          </div>
        </div>
      ))}
    </div>
  );
}
