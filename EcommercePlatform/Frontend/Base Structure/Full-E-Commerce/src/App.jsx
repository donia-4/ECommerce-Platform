import './App.css'
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Layout from './components/layout/layout';

function App() {

  const router=createBrowserRouter([{

    path:"/",
    element:<Layout/>,

/*  =============Catches errors in all child routes ============= */

/*     errorElement: <ErrorPage /> */  

  children:[
      //ِ===============Any elements have Nav && footer=============

/*    Ex:   {path:"",element:<Home/>} */   



// Add any components here







  {    path:"*",
/*     element:<Error/>
 */}

    ]
  },
  
  /*  ============= Specific 404 page for unknown routes ============= */

  {path: "*",
  /* errorElement: <ErrorPage /> */}
])
  return (
 <RouterProvider router={router}></RouterProvider>
  )
}

export default App
