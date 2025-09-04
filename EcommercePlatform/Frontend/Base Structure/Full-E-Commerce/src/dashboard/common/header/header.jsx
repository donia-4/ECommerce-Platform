import React, { useContext } from 'react';
import "./css/Header.css"
import { useParams } from 'react-router-dom';
export default function Header()
{
    const {path}=useParams();
    console.log(path);
    

    return <div className="Header"> 
<div className="left">
<p><span>Pages</span> / {path ?? "Dashboard"} </p>
<h6> {path ?? "Dashboard"} </h6>
</div>
<div className="right">
<div className="input">
    <input type='text' placeholder='Type here' />
</div>
    <p>Sign In</p>

</div>
    </div>;
}