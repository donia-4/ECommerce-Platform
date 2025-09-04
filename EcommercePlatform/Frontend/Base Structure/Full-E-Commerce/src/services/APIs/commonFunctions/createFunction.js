

export default async function CreateAPi_Function (initialUrl,headersData,intialData){

 try {

    let res = await  fetch(initialUrl,{

        method:"POST",
        body:JSON.stringify(intialData),
        headers:headersData
    })
    

// Http  level error (status code) 
  /*   if(!res.ok) {
        throw new Error (`Create HTTP error!${res.status} `);
    }  */


const contentType = res.headers.get("content-type");
        if (contentType && contentType.includes("application/json")) {
            return await res.json();
        } else {
            return await res.text();
        }       }
        catch(error)
        {
 throw new Error (error.message);
        }
}
