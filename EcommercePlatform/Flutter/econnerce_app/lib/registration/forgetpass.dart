import 'package:econnerce_app/registration/Login.dart';
import 'package:flutter/material.dart';
import 'package:econnerce_app/registration/custombutton.dart';
class Forgetpassword extends StatefulWidget {
  const Forgetpassword({super.key});

  @override
  State<Forgetpassword> createState() => _ForgetpasswordState();
}

class _ForgetpasswordState extends State<Forgetpassword> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        backgroundColor: Colors.blueGrey[50],
        body:SafeArea(child:
        SingleChildScrollView(
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              IconButton(onPressed:(){
                Navigator.of(context).pop(MaterialPageRoute(builder: (context)=>Login()));
              } , icon: Icon(Icons.arrow_back_ios,)),
              SizedBox(height: 10,),
              Text("  Forgot Password",style: TextStyle(fontSize: 34,fontWeight: FontWeight.bold,color: Colors.red[500]),),
              SizedBox(height: 60,),

                Column(
                    mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                    children: [
                         Text("Please, enter your email address. You will receive \n a link to create a new password via email.",
                           style: TextStyle(fontSize: 14,fontWeight: FontWeight.w500),
                         ),
                      Padding(
                        padding: const EdgeInsets.only(left: 20,right: 20,top: 30),
                        child: TextFormField(
                          decoration:InputDecoration(
                              prefixIcon: Icon(Icons.email),
                              label: Text("Email",style: TextStyle(fontSize: 15),),
                              border: OutlineInputBorder(
                                borderRadius: BorderRadius.circular(20),
                              ),
                              focusedBorder: OutlineInputBorder(
                                borderSide: BorderSide(color: Colors.grey),
                                borderRadius: BorderRadius.circular(20),
                              )
                          ),
                        ),
                      ),

                      Custonbuttons(name: "SEND"),

                    ],
                  ),

            ],
          ),
        ),
        )
    );
  }
}
