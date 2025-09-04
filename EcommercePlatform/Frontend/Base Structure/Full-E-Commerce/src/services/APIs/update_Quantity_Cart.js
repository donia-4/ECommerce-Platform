
import UpdateAPi_Function from "./commonFunctions/updateFuntion.js";

import { ReToken ,getToken } from "./commonFunctions/TokenFunction.js";
export default async function UpdateQunatityCart(intialData)
{

     let Token =getToken();
     let res =await UpdateAPi_Function(import.meta.env.VITE_UPDATE_QUNTITY_CART_API,
          {"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`}
     ,intialData
     );

         
          if(res.statusCode===401){
               
     let retoken= await ReToken();
     if (!retoken) return [];
     
     res =await  UpdateAPi_Function(import.meta.env.VITE_UPDATE_QUNTITY_CART_API,
          {"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`}
     ,intialData
     );

          }


     return res;
}

