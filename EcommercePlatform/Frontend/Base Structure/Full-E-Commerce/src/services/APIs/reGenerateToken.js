

import CreateAPi_Function from "./commonFunctions/createFunction.js";


export default async function ReGenerateToken(intialData)
{ 

     let res =await CreateAPi_Function(import.meta.env.VITE_RE_TOKEN_API
,intialData);
     return res;

}










