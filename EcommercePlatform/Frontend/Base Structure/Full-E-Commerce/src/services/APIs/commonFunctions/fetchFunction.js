

export default async function FetchApi_Function (initialUrl,headersData){
    try
    {
let res=await fetch(initialUrl,{

      headers:headersData

});

if(!res.ok)
{
      throw new Error (`Fetch HTTP error!${res.status} `);
}

return await res.json();;

    }
    catch(error)
    {
 throw new Error (error.message);

}
}