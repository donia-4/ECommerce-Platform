import 'package:econnerce_app/providers/containprovider.dart';
import 'package:econnerce_app/providers/productprovider.dart';
import 'package:econnerce_app/registration/Login.dart';
import 'package:econnerce_app/registration/forgetpass.dart';
import 'package:econnerce_app/registration/signup.dart';
import 'package:econnerce_app/screens/home.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<MyApp> createState() => _MyAppState();
}

class _MyAppState extends State<MyApp> {
  @override
  Widget build(BuildContext context) {
    return  MultiProvider(
        providers: [
          ChangeNotifierProvider(create: (_) => Productprovider()),
          ChangeNotifierProvider(create: (_) => Containprovider()),
        ],
      child:
      MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Home()
    )
    );
  }
}
