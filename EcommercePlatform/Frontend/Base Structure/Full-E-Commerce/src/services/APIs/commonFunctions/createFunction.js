

export default async function CreateAPi_Function (initialUrl,intialData){

 try {

    let res = await  fetch(initialUrl,{

        method:"POST",
        body:JSON.stringify(intialData),
        headers:{"Content-Type":"application/json"} 
    })

// Http  level error (status code) 
    if(!res.ok) {
        throw new Error (`Create HTTP error!${res.status} `);
    } 


return await res.json();
       }
        catch(error)
        {
 throw new Error (error.message);
        }
}
