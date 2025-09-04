import { useParams } from "react-router-dom";
import { useContext, useState } from "react";
import { ProductContext } from "../../context/ProductContext";
import RatingStars from "../../components/RatingStars/RatingStars";
import { FaHeart } from "react-icons/fa";
import "./ProductDetail.css";

export default function ProductDetail() {
  const { id } = useParams();
  const { products } = useContext(ProductContext);

  const [color, setColor] = useState("blue");
  const [size, setSize] = useState("M");
  const [quantity, setQuantity] = useState(1);

  const handleIncrease = () => setQuantity((q) => q + 1);
  const handleDecrease = () => setQuantity((q) => (q > 1 ? q - 1 : 1));

  const product = products.find((p) => p.id === parseInt(id));

  if (!product) {
    return <h2>Product not found</h2>;
  }

  return (
    <div className="product-detail">
      <div className="image-section">
        <img src={product.image} alt={product.name} />
      </div>

      <div className="info-section">
        <h2>{product.name}</h2>

        <div className="stars">
          <RatingStars rating={product.rating} />
          <span className="reviews">({product.reviews} reviews)</span>
        </div>

        <p className="price">${product.price}</p>

        <p className="description">{product.description}</p>

        <div className="section">
          <span>Colours:</span>
          <div className="colors">
            <button
              className={`circle ${color === "blue" ? "active" : ""}`}
              style={{ background: "lightblue" }}
              onClick={() => setColor("blue")}
            ></button>
            <button
              className={`circle ${color === "red" ? "active" : ""}`}
              style={{ background: "red" }}
              onClick={() => setColor("red")}
            ></button>
          </div>
        </div>

        <div className="section">
          <span>Size:</span>
          <div className="sizes">
            {["XS", "S", "M", "L", "XL"].map((s) => (
              <button
                key={s}
                className={`size-btn ${size === s ? "active" : ""}`}
                onClick={() => setSize(s)}
              >
                {s}
              </button>
            ))}
          </div>
        </div>

        <div className="actions">
          <div className="qty">
            <button onClick={handleDecrease}>-</button>
            <span>{quantity}</span>
            <button onClick={handleIncrease}>+</button>
          </div>
          <button className="buy-btn">Buy Now</button>
          <button className="heart">
            <FaHeart />
          </button>
        </div>

      </div>
    </div>
  );
}
