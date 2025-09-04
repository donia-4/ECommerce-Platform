
import DeleteAPi_Function from "./commonFunctions/deleteFunction.js";
import { getToken, ReToken } from "./commonFunctions/TokenFunction.js";
export default async function DeleteCart(id)
{
let Token = getToken();

     let res =await DeleteAPi_Function(`${import.meta.env.VITE_REMOVE_ITEM_CART_API}/${id}`,
          {"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`}     );


          if(res.statusCode===401){
               console.log("viewCart Retoken");
               
     let retoken= await ReToken();
     if (!retoken) return [];
     
     res =await DeleteAPi_Function(`${import.meta.env.VITE_REMOVE_ITEM_CART_API}/${id}`,
          {"Content-Type":"application/json",
     'Authorization': `Bearer ${retoken}`},
     intialData
     );
}
     return res;
}

