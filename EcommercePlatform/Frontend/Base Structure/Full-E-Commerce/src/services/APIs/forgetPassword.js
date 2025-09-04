

import CreateAPi_Function from "./commonFunctions/createFunction.js";


export default async function ForgetPasword(intialData)
{ 
      
     let res =await CreateAPi_Function(import.meta.env.VITE_LOGIN_API, {"Content-Type":"application/json",
     'Authorization': `Bearer ${Token}`} );
     return res;

}



