
import CreateAPi_Function from "./commonFunctions/createFunction.js";
export default async function AddTOCart(intialData,Token)
{
     let res =await CreateAPi_Function(import.meta.env.VITE_ADD_TO_CART_API,{"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`},
     intialData

     );
     return res;
}



