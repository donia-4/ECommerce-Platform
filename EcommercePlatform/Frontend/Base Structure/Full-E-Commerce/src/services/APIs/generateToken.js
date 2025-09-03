

import CreateAPi_Function from "./commonFunctions/createFunction.js";


export default async function GenerateToken(intialData)
{ 

     let res =await CreateAPi_Function("https:localhost:7299/api/Account/login"
,intialData);
     return res;

}










