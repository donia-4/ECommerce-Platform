import React, { useState } from 'react';
import "./css/Nav.css"
import { FaTv } from "react-icons/fa6";
import { CiCircleList } from "react-icons/ci";
import { CiCreditCard1 } from "react-icons/ci";
import { MdOutlineLanguage } from "react-icons/md";
import { MdOutlineAssignmentInd } from "react-icons/md";
import { SiGnuprivacyguard } from "react-icons/si";
import { MdOutlineDashboard } from "react-icons/md";
import { FaUserFriends } from "react-icons/fa";
import { CiDatabase } from "react-icons/ci";
import { motion } from 'framer-motion';
import { NavLink } from 'react-router-dom';

export default function Nav() {

const [isPageOpen,setIsPageOpen]=useState(false);

 return <div className="Nav-dashboard">

        <div className="container">


<div className="header">
            <h6><span className="logo">
 <MdOutlineDashboard/>
            </span>
                 Admin Dashboard</h6>

</div>
            <ul className='main-container'>

                <li>
                    <NavLink to={"/dashboard"}>
                        <div className="logo">
                            <FaTv  style={{color:"rgb(94 114 228)"}} />
                        </div>
                        Dashboard
                    </NavLink>

                </li>
                <li>

                    <div className="top" onClick={()=>{setIsPageOpen(!isPageOpen)}} >

                        <a >
                            <span className="logo">
                                <CiCircleList style={{color:"rgb(251 99 64)"}} />
                            </span>
                            Pages</a>
                    </div>

                

                </li>

<li className='page-slide'>
        <motion.div 
                    initial={{height:"0px",width:"0px",opacity:0 ,overflow:"hidden"}}
                  animate={isPageOpen?{overflow:"visible",width:"100%",height:"fit-content",opacity:1}:''
                  }

                  transition={{
                    duration:0.5
                  }}
                    
                    className="bottom">

                        <ul className='page-list'>
                            <li >
                                
                                 <NavLink to={"/users"}>
                                    <span className='logo' style={{color:"rgb(200 99 100)"}}> <FaUserFriends/></span>
                                      Users</NavLink></li>
                            <li > <NavLink to={"/productss"}>  
                                <span className='logo'><CiDatabase  style={{color:"rgb(180 99 100)"}}/></span>
                                products </NavLink></li>
                        </ul>
                    </motion.div>

</li>
                <li>
                    <NavLink to={"/billing"} >
                        <span className="logo">
                            <CiCreditCard1 style={{color:"green"}} />
                        </span>
                        Billing
                    </NavLink>
                </li>

                <li>
                    <NavLink to={"/Rtl"}>
                        <span className="logo">
                            <MdOutlineLanguage style={{color:"rgb(251 99 64)"}}/>
                        </span>
                        RTL
                    </NavLink>
                </li>
                <li className='account'>
                    <div className="top">

                        <p>Account Pages</p>
                    </div>
                    <div className="bottom">

                       
                    </div>

                </li>

                            
                            <li>

                                <NavLink to={"/sign in"} >
                                    <span className="logo">
                                        <MdOutlineAssignmentInd style={{color:"rgb(251 99 64)"}} />
                                    </span>
                                    Sign In
                                </NavLink>
                            </li>
                            <li>
                                <NavLink to={"/sign up"} >
                                    <span className="logo">
                                        <SiGnuprivacyguard style={{color:"rgb(17 205 239)"}} />
                                    </span>
                                    Sign Up
                                </NavLink>
                            </li>

            </ul>
        </div>

    </div>;
}