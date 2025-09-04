    import CreateAPi_Function from "./commonFunctions/createFunction.js";
import { getToken, ReToken } from "./commonFunctions/TokenFunction.js";
    export default async function Logout()
    {
    let Token =getToken();
    console.log(Token);
    
         let res =await CreateAPi_Function(import.meta.env.VITE_LOGOUT_API,   
              {"Content-Type":"application/json",
         'Authorization': `Bearer ${Token}`});
    
    
         
    console.log("logout");
    
         
         if(res.statusCode===401){
              console.log("logout");
              
    let retoken= await ReToken();
    if (!retoken) return [];
    
    res  =await FetchApi_Function(import.meta.env.VITE_LOGOUT_API,    
    {"Content-Type":"application/json",
         'Authorization': `Bearer ${retoken}`});
         }


       if(res.succeeded) localStorage.removeItem("userData");
         
        console.log(res);
        
         return res;
    }
    