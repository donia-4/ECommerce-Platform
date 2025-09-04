

export default async function FetchApi_Function (initialUrl){
    try
    {
let res=await fetch(initialUrl,{

      headers:{"Content-Type":"application/json"} 

});

if(!res.ok)
{
    setFetchError(true);
      throw new Error (`Fetch HTTP error!${res.status} `);
}

return await res.json();;

    }
    catch(error)
    {
 throw new Error (error.message);

}
}