

import "./signup.css";
import register_img from "../../assets/images/register-img.png";
import { FcGoogle } from "react-icons/fc";
import { useRef } from "react";
import SignUp_Api from "../../services/APIs/signup";
import { Link, useNavigate } from "react-router-dom";
export default function Signup()
{
    const inputRef=useRef([]);
    const errorRef=useRef();
const navigate=useNavigate(null);

async   function register()
    {
        let data={
  "email": inputRef.current["email"].value,
  "password": inputRef.current["password"].value,
  "phoneNumber":inputRef.current["phone"].value,
  "fullName": inputRef.current["name"].value,
  "birthDate": inputRef.current["birthDate"].value
}

console.log(data);


let res = await SignUp_Api(data);
console.log(res);

if(res.statusCode==200)navigate("/");
else{
errorRef.current.innerHTML=res.message;
errorRef.current.style.opacity=1;
}

    }

    
 
    return <div className="Signup formContainer"> 

 <main className="main-content">
        <div className="image-section">
            <img src={register_img} alt="An illustration of a person creating an account."/>
        </div>
        <div className="form-section">
            <h2>Create an account</h2>
            <p>Enter your details below</p>
            <div className="form" >

{/*========= name input========*/} 
               <input type="text" placeholder="FullName"
                ref={input=>{inputRef.current["name"]=input}} 
                name="fullName" required/>
                {/*========= email input========*/} 

                <input type="email" ref={input=>{inputRef.current["email"]=input}}   placeholder="Email or Phone Number" name="email_or_phone" required/>
{/*========= password input========*/} 

                <input type="password" placeholder="Password" ref={input=>{inputRef.current["password"]=input}}  name="password" required/>

           {/*========= birth input========*/} 
     <input type="text" placeholder="BirthDate (e.g. 2004-09-18)" 
                ref={input=>{inputRef.current["birthDate"]=input}} 
                name="birthDate"  pattern="^\d{4}-\d{2}-\d{2}$" required/>
{/*========= phone input========*/} 

                <input type="text" placeholder="Phone Number"
                ref={input=>{inputRef.current["phone"]=input}} 
                name="phoneNumber" required/>
{/*========= submit========*/} 

                <button type="submit" 
                
                onClick={()=>{register(),errorRef.current.style.opacity=0}}
                className="create-account-btn">Create Account</button>
                <button className="google-btn"> <span><FcGoogle/></span>  Sign up with Google</button>
            </div>
            <p className="login-link">Already have an account? <Link to={"/login"}>Log in</Link></p>

            <p ref={errorRef} className="error" style={{color:"red",marginTop:"20px",fontSize:"13px",opacity:0}}>Invalid username or password</p>
        </div>
    </main>
    </div>
    
}
 