
import CreateAPi_Function from "./commonFunctions/createFunction.js";
import { ReToken,getToken } from "./commonFunctions/TokenFunction.js";
export default async function AddTOCart(intialData)
{
     let Token =getToken();

     let res =await CreateAPi_Function(import.meta.env.VITE_ADD_TO_CART_API,{"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`},
     intialData);

      if(res.statusCode===401){

          console.log("viewCart Retoken");
          
let retoken= await ReToken();
if (!retoken) return [];

res =await FetchApi_Function(import.meta.env.VITE_VIEW_CART_API,             
{"Content-Type":"application/json",
'Authorization': `Bearer ${retoken}`}
);
     
}

     return res;
}



