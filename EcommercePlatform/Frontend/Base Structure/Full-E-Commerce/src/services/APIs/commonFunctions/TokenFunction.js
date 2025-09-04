import ReGenerateToken from "../reGenerateToken";

export async function ReToken()
    {
    console.log("RETOKENNNN");

         if(localStorage.getItem("userData")!=null ||localStorage.getItem("userData")!=undefined)
        {     

            console.log("ddddddddd");
            
             let Data= JSON.parse(localStorage.getItem("userData")) ;
               console.log("Data",Data
);
                   
    let res = await ReGenerateToken(Data.refreshToken);
                       console.log("res",res);

    
    Data.accessToken=res.data.accessToken;
    Data.refreshToken=res.data.refreshToken;
    localStorage.setItem("userData" , JSON.stringify({...Data}));
    
    return res.data.accessToken; 

        }


    return "";
    }




    export    function getToken()
    {
        
 if(localStorage.getItem("userData")!=null ||localStorage.getItem("userData")!=undefined)
        {            let Data= JSON.parse(localStorage.getItem("userData")) ;
            
            return Data.accessToken;

        }
return "";
    }
