
import FetchApi_Function from "./commonFunctions/fetchFunction.js";
export default async function ViewProducts()
{
     let res =await FetchApi_Function(import.meta.env.VITE_VIEW_PRODUCTS_API,{"Content-Type":"application/json"} );
     return res;
}

