
import CreateAPi_Function from "./commonFunctions/createFunction.js";
export default async function SignUp_Api(intialData)
{
     
     let res =await CreateAPi_Function(import.meta.env.VITE_SIGNUP_API,{"Content-Type":"application/json"},intialData );
     return res;
}

