
import CreateAPi_Function from "./commonFunctions/createFunction.js";
export default async function SignUp_Api(intialData)
{
     let res =await CreateAPi_Function("https://localhost:7299/api/Account/register",intialData);
     return res;
}

