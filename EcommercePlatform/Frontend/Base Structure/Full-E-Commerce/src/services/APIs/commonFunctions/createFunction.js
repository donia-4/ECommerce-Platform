

export default async function CreateAPi_Function (initialUrl,headersData,intialData){

 try {

    let res = await  fetch(initialUrl,{

        method:"POST",
        body:JSON.stringify(intialData),
        headers:headersData
    })
    

// Http  level error (status code) 
     if(!res.ok) {
            return "Please enter your data correctly ";
    }  


const contentType = res.headers.get("content-type");
        if (contentType && contentType.includes("application/json")) {
            return await res.json();
        } else {
            return await res.text();
        }       }
        catch(error)
        {
            return "A network error occurred";
/*  throw new Error (error.message);
 */        }
}
