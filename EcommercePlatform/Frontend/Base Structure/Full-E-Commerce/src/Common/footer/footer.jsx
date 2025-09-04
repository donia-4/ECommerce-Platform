import "./css/footer.css";

import { SlSocialFacebook } from "react-icons/sl";
import { CiTwitter } from "react-icons/ci";
import { CiLinkedin } from "react-icons/ci";
import { FaInstagram } from "react-icons/fa";

import appstore from "../../assets/images/appstore.svg"
import GooglePlay from "../../assets/images/GooglePlay.svg"
import Qr from "../../assets/images/Qr Code.svg"
export default function Footer()
{
 
    return (
  
    <footer className="footer">
        <div className="container">
            <div className="footer-grid">

                <div>
                    <h2 className="footer-heading">Exclusive</h2>
                    <h3 className="footer-sub-heading">Subscribe</h3>
                    <p className="footer-text">Get 10% off your first order</p>
                    <div className="subscribe-input-container">
                        <input type="email" placeholder="Enter your email" className="subscribe-input"/>
                        <i className="fa-solid fa-arrow-right arrow-icon"></i>
                    </div>
                </div>

                <div>
                    <h2 className="footer-heading">Support</h2>
                    <address className="footer-text address">
                        111 Bijoy sarani, Dhaka,<br/>
                        DH 1515, Bangladesh.<br/><br/>
                        <a href="mailto:exclusive@gmail.com" className="hover:underline">exclusive@gmail.com</a><br/>
                        <a href="tel:+88015888889999" className="hover:underline">+88015-88888-9999</a>
                    </address>
                </div>

                <div>
                    <h2 className="footer-heading">Account</h2>
                    <ul className="footer-link-list">
                        <li><a href="#">My Account</a></li>
                        <li><a href="#">Login / Register</a></li>
                        <li><a href="#">Cart</a></li>
                        <li><a href="#">Wishlist</a></li>
                        <li><a href="#">Shop</a></li>
                    </ul>
                </div>

                <div>
                    <h2 className="footer-heading">Quick Link</h2>
                    <ul className="footer-link-list">
                        <li><a href="#">Privacy Policy</a></li>
                        <li><a href="#">Terms Of Use</a></li>
                        <li><a href="#">FAQ</a></li>
                        <li><a href="#">Contact</a></li>
                    </ul>
                </div>

                <div>
                    <h2 className="footer-heading">Download App</h2>
                    <p className="download-app-text">Save $3 with App New User Only</p>
                    <div className="download-app-row">
                        <div className="qr-code-placeholder">
                            <img src={Qr} alt="QR Code"/>
                        </div>
                        <div className="app-store-links">
                            <a href="#" className="app-store-link">
                                <img src={GooglePlay} alt="Google Play"/>
                            </a>
                            <a href="#" className="app-store-link">
                                <img src={appstore}alt="App Store"/>
                            </a>
                        </div>
                    </div>
                    <div className="social-icons">
                        <a href="#"><i className="fab fa-facebook-f">
                            <SlSocialFacebook/> 
</i></a>
                        <a href="#"><i className="fab fa-twitter">
                            < CiTwitter/>

                            </i></a>
                        <a href="#"><i className="fab fa-instagram">
                            < FaInstagram />

                            </i></a>
                        <a href="#"><i className="fab fa-linkedin-in">
                            <CiLinkedin /> 

                            </i></a>
                    </div>
                </div>

            </div>
            
            <div className="copyright">
                &copy; Copyright Rimel 2022. All right reserved
            </div>
        </div>
    </footer>);
    
}