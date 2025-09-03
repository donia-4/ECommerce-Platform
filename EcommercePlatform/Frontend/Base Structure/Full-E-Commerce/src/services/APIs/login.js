

import CreateAPi_Function from "./commonFunctions/createFunction.js";


export default async function Login_Api(intialData)
{ 
      
     let res =await CreateAPi_Function(import.meta.env.VITE_LOGIN_API,intialData);
     return res;

}



