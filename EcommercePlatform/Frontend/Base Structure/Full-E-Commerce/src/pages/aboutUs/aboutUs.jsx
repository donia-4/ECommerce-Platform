import React from "react";
import {
  FaShoppingBag,
  FaUsers,
  FaChartLine,
  FaShoppingCart,
  FaTruck,
  FaHeadset,
  FaUndo,
  FaTwitter,
  FaInstagram,
  FaLinkedin,
} from "react-icons/fa";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./AboutUs.css";

export default function AboutUs() {
  const stats = [
    {
      number: "10.5k",
      text: "Sellers active on our site",
      icon: <FaShoppingBag />,
    },
    {
      number: "33k",
      text: "Monthly Product Sales",
      icon: <FaShoppingCart />,
      highlight: true,
    },
    {
      number: "45.5k",
      text: "Customers active on our site",
      icon: <FaUsers />,
    },
    {
      number: "25k",
      text: "Annual gross sale on our site",
      icon: <FaChartLine />,
    },
  ];

  const team = [
    {
      name: "Tom Cruise",
      role: "Founder & Chairman",
      img: "/image1.png",
    },
    {
      name: "Emma Watson",
      role: "Managing Director",
      img: "/image2.png",
    },
    {
      name: "Will Smith",
      role: "Product Designer",
      img: "/image3.png",
    },
    {
      name: "Tom Cruise",
      role: "Founder & Chairman",
      img: "/image1.png",
    },
    {
      name: "Emma Watson",
      role: "Managing Director",
      img: "/image2.png",
    },
  ];

  const features = [
    {
      icon: <FaTruck />,
      title: "FREE AND FAST DELIVERY",
      text: "Free delivery for all orders over $140",
    },
    {
      icon: <FaHeadset />,
      title: "24/7 CUSTOMER SERVICE",
      text: "Friendly 24/7 customer support",
    },
    {
      icon: <FaUndo />,
      title: "MONEY BACK GUARANTEE",
      text: "We return money within 30 days",
    },
  ];

  const sliderSettings = {
    dots: true,
    infinite: true,
    speed: 500,
    slidesToShow: 3,
    slidesToScroll: 1,
    responsive: [{ breakpoint: 768, settings: { slidesToShow: 1 } }],
  };

  return (
    <div className="about-container">
      <div className="story-section">
        <div className="story-text">
          <h2>Our Story</h2>
          <p>
            Launced in 2015, Exclusive is South Asia’s premier online shopping
            makterplace with an active presense in Bangladesh. Supported by wide
            range of tailored marketing, data and service solutions, Exclusive
            has 10,500 sallers and 300 brands and serves 3 millioons customers
            across the region.
          </p>
          <p>
            Exclusive has more than 1 Million products to offer, growing at a
            very fast. Exclusive offers a diverse assotment in categories
            ranging from consumer
          </p>
        </div>
        <div className="story-img">
          <img src="/aboutimg1.jpg" alt="shopping" />
        </div>
      </div>

      <div className="stats-container">
        {stats.map((item, i) => (
          <div
            key={i}
            className={`stat-card ${item.highlight ? "highlight" : ""}`}
          >
            <div className="stat-icon">{item.icon}</div>
            <h3>{item.number}</h3>
            <p>{item.text}</p>
          </div>
        ))}
      </div>

      <div className="team-section">
        <Slider {...sliderSettings}>
          {team.map((member, i) => (
            <div key={i} className="team-card">
              <img src={member.img} alt={member.name} />
              <h4>{member.name}</h4>
              <p>{member.role}</p>
              <div className="team-icons">
                <a href="#">
                  <FaTwitter />
                </a>
                <a href="#">
                  <FaInstagram />
                </a>
                <a href="#">
                  <FaLinkedin />
                </a>
              </div>
            </div>
          ))}
        </Slider>
      </div>

      <div className="features-container">
      {features.map((f, i) => (
        <div key={i} className="feature-card">
          <div className="feature-icon">{f.icon}</div>
          <h4>{f.title}</h4>
          <p>{f.text}</p>
        </div>
      ))}
    </div>
    </div>
  );
}
