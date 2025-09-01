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
  
    <footer class="footer">
        <div class="container">
            <div class="footer-grid">

                <div>
                    <h2 class="footer-heading">Exclusive</h2>
                    <h3 class="footer-sub-heading">Subscribe</h3>
                    <p class="footer-text">Get 10% off your first order</p>
                    <div class="subscribe-input-container">
                        <input type="email" placeholder="Enter your email" class="subscribe-input"/>
                        <i class="fa-solid fa-arrow-right arrow-icon"></i>
                    </div>
                </div>

                <div>
                    <h2 class="footer-heading">Support</h2>
                    <address class="footer-text address">
                        111 Bijoy sarani, Dhaka,<br/>
                        DH 1515, Bangladesh.<br/><br/>
                        <a href="mailto:exclusive@gmail.com" class="hover:underline">exclusive@gmail.com</a><br/>
                        <a href="tel:+88015888889999" class="hover:underline">+88015-88888-9999</a>
                    </address>
                </div>

                <div>
                    <h2 class="footer-heading">Account</h2>
                    <ul class="footer-link-list">
                        <li><a href="#">My Account</a></li>
                        <li><a href="#">Login / Register</a></li>
                        <li><a href="#">Cart</a></li>
                        <li><a href="#">Wishlist</a></li>
                        <li><a href="#">Shop</a></li>
                    </ul>
                </div>

                <div>
                    <h2 class="footer-heading">Quick Link</h2>
                    <ul class="footer-link-list">
                        <li><a href="#">Privacy Policy</a></li>
                        <li><a href="#">Terms Of Use</a></li>
                        <li><a href="#">FAQ</a></li>
                        <li><a href="#">Contact</a></li>
                    </ul>
                </div>

                <div>
                    <h2 class="footer-heading">Download App</h2>
                    <p class="download-app-text">Save $3 with App New User Only</p>
                    <div class="download-app-row">
                        <div class="qr-code-placeholder">
                            <img src={Qr} alt="QR Code"/>
                        </div>
                        <div class="app-store-links">
                            <a href="#" class="app-store-link">
                                <img src={GooglePlay} alt="Google Play"/>
                            </a>
                            <a href="#" class="app-store-link">
                                <img src={appstore}alt="App Store"/>
                            </a>
                        </div>
                    </div>
                    <div class="social-icons">
                        <a href="#"><i class="fab fa-facebook-f">
                            <SlSocialFacebook/> 
</i></a>
                        <a href="#"><i class="fab fa-twitter">
                            < CiTwitter/>

                            </i></a>
                        <a href="#"><i class="fab fa-instagram">
                            < FaInstagram />

                            </i></a>
                        <a href="#"><i class="fab fa-linkedin-in">
                            <CiLinkedin /> 

                            </i></a>
                    </div>
                </div>

            </div>
            
            <div class="copyright">
                &copy; Copyright Rimel 2022. All right reserved
            </div>
        </div>
    </footer>);
    
}