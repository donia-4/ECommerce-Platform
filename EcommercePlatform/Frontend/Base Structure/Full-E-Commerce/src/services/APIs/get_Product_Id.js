
import FetchApi_Function from "./commonFunctions/fetchFunction.js";
export default async function ProductById(id)
{
     console.log(`${import.meta.env.VITE_PRODUCT_BY_ID_API}/${id}`);
     
     let res =await FetchApi_Function(`${import.meta.env.VITE_PRODUCT_BY_ID_API}/${id}`,{"Content-Type":"application/json"} );
     return res;
}

