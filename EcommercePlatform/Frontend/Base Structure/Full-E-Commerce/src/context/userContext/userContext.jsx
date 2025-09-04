import { createContext, useEffect, useState } from "react"
import ReGenerateToken from "../../services/APIs/reGenerateToken";


export const UserContext=createContext();


export default function UserProvider({children})
{



    const [userData,setUserData]=useState({});

    function UserDataSetting()
    {
         if(localStorage.getItem("userData")!=null ||localStorage.getItem("userData")!=undefined)
        {
            let Data= JSON.parse(localStorage.getItem("userData")) ;
            console.log("userData",Data);
            setUserData(Data);
            
            
         

        
        }
    }

function isLogin()
{
    
 
  return(localStorage.getItem("userData")!=null ||localStorage.getItem("userData")!=undefined);
}





    return <UserContext.Provider value={{UserDataSetting,isLogin
    }}>
        {children}
    </UserContext.Provider>

}