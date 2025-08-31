import 'package:econnerce_app/registration/Login.dart';
import 'package:flutter/material.dart';
import 'package:econnerce_app/registration/customfield.dart';
import 'package:econnerce_app/registration/custombutton.dart';

class Signup extends StatefulWidget {
  const Signup({super.key});

  @override
  State<Signup> createState() => _SignupState();
}

class _SignupState extends State<Signup> {

  @override
  Widget build(BuildContext context) {
    return Scaffold(
         backgroundColor: Colors.red[100],
      body:SafeArea(child:
      SingleChildScrollView(
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            IconButton(onPressed:(){} , icon: Icon(Icons.arrow_back_ios,)),
            SizedBox(height: 10,),
            Text("  Sign up",style: TextStyle(fontSize: 34,fontWeight: FontWeight.bold,),),
            SizedBox(height: 60,),

            Container(
              padding: EdgeInsets.all(20),
              margin: EdgeInsets.all(10),
              height: 630,
              decoration: BoxDecoration(
                color: Colors.white,
                borderRadius: BorderRadius.all(Radius.circular(50))
              ),
              child: Column(
                children: [
                  Padding(
                    padding: const EdgeInsets.only(left: 20,right: 20),
                    child: Customtextfield(label: "Name", icon: Icon(Icons.person))
                  ),

            SizedBox(height: 10,),
             Padding(
                padding: const EdgeInsets.only(left: 20,right: 20),
                child: Customtextfield(label: "Email", icon: Icon(Icons.email))
              ),

            SizedBox(height: 10,),
            Padding(
              padding: const EdgeInsets.only(left: 20,right: 20),
              child: Customtextfield(label: "Password", icon: Icon(Icons.lock))
            ),
            const SizedBox(height: 20),
            Row(
              mainAxisAlignment: MainAxisAlignment.end,
            children: [
              Text("Already have an account?",style: TextStyle(fontSize: 15,fontWeight: FontWeight.w500),),
              IconButton(onPressed: (){
                Navigator.of(context).push(MaterialPageRoute(builder: (context)=>Login()));
              }, icon: Icon(Icons.arrow_right_alt_rounded,color: Colors.red[900],),iconSize: 25,)
            ],
            ),
            
                Custonbuttons(name: "SIGN UP"),

                Center(
                  child: Text("Or sign up with social account",style: TextStyle(fontSize: 15,fontWeight:FontWeight.w700 ),),
                ),
                SizedBox(height: 20,),
               Row(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                   Container(
                       width: 60,
                       height: 60,
                       margin: const EdgeInsets.symmetric(horizontal: 10),
                       decoration: BoxDecoration(
                         color: Colors.blueGrey[50],
                         borderRadius: BorderRadius.circular(15),
                         border: Border.all(color: Colors.grey.shade400),
                       ),
                     child: MaterialButton(onPressed: (){},
                       child: Image.asset("lib/images/google.jpg",height: 50,width: 50,),
                     ),
                   ),
                    Container(
                      width: 60,
                      height: 60,
                      margin: const EdgeInsets.symmetric(horizontal: 10),
                      decoration: BoxDecoration(
                        color: Colors.blueGrey[50],
                        borderRadius: BorderRadius.circular(15),
                        border: Border.all(color: Colors.grey.shade400),
                      ),
                      child: MaterialButton(onPressed: (){},
                        child: Image.asset("lib/images/facebook.jpg",height: 50,width: 50,),
                      ),
                    ),
                   ]
                ),
      ],
              ),
            ),
          ],
),
),
)
);
}
}
