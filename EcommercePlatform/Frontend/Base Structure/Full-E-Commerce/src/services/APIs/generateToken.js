

import CreateAPi_Function from "./commonFunctions/createFunction.js";


export default async function GenerateToken(intialData)
{ 

     let res =await CreateAPi_Function(import.meta.env.VITE_TOKEN_API,intialData);
     return res;

}










