

export default async function DeleteAPi_Function (initialUrl,headersData, intialData){

 try {

    let res = await  fetch(initialUrl,{

        method:"DELETE",
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
