
import FetchApi_Function from "./commonFunctions/fetchFunction.js";
import { getToken, ReToken } from "./commonFunctions/TokenFunction.js";
export default async function ViewCart()
{
let Token =getToken();
if (!Token) return [];

     let res =await FetchApi_Function(import.meta.env.VITE_VIEW_CART_API,   
          
     
{"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`});


     

     
     if(res.statusCode===401){
          console.log("viewCart Retoken");
          
let retoken= await ReToken();
if (!retoken) return [];

res =await FetchApi_Function(import.meta.env.VITE_VIEW_CART_API,             
{"Content-Type":"application/json",
     'Authorization': `Bearer ${retoken}`});
     }
     
     return res;
}



