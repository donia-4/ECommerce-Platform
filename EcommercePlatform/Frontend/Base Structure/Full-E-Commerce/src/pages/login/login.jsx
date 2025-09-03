

import "../signup/signup.css";
import register_img from "../../assets/images/register-img.png";
import { FcGoogle } from "react-icons/fc";
import { useRef } from "react";
import GenerateToken from "../../services/APIs/generateToken";
import { Link, useNavigate } from "react-router-dom";
export default function Login()
{
    const inputRef=useRef([]);
    const errorRef=useRef();
const navigate=useNavigate(null);

async   function TokenGeneration()
    {
        let inputData={
  "email": inputRef.current["email"].value,
  "password": inputRef.current["password"].value,
}

let res = await GenerateToken (inputData);

if(res.succeeded){
localStorage.setItem("userData",JSON.stringify(res.data) );
navigate("/");}
else{
    
 errorRef.current.style.opacity=1;    
 }




    }

    
 
    return <div className="Signup formContainer"> 

 <main className="main-content">
        <div className="image-section">
            <img src={register_img} alt="An illustration of a person creating an account."/>
        </div>
        <div className="form-section">
            <h2>Login to Exclusive </h2>
            <p>Enter your details below</p>
            <div className="form" >
{/*========= email input========*/} 


                <input type="email" ref={input=>{inputRef.current["email"]=input}}   placeholder="Email or Phone Number" name="email_or_phone" required/>
{/*========= password input========*/} 

                <input type="password" placeholder="Password" ref={input=>{inputRef.current["password"]=input}}  name="password" required/>

{/*========= submit========*/} 
                    <div className="form-buttons">

                <button type="submit" 
                
                onClick={()=>{TokenGeneration();errorRef.current.style.opacity=0}}
                className="create-account-btn">Log in</button>
                <div className="add" style={{display:"flex",justifyContent:"space-between",fontSize:"10px"}}>

                <Link   to={"/signup"}>Create Account</Link>
                    <a href="#" className="forgot-password">Forget Password?</a>

    
                </div>
<p ref={errorRef} className="error" style={{color:"red",marginTop:"20px",fontSize:"13px",opacity:0}}>Invalid username or password</p>
                </div>
                </div>
        </div>
    </main>
    </div>
    
}
 



