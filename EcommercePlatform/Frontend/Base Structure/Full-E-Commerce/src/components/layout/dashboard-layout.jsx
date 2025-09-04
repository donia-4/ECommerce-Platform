import React from 'react';
import { Outlet } from 'react-router-dom';
 import Nav from '../../dashboard/common/Nav/Nav';

export default function DashboardLayout()
{

    return <div className="Dasboard-layout" >
         <div className="fixed-bg">
 <Nav/>
 <Outlet/>
 </div>
    </div>;
}