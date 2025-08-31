import 'package:econnerce_app/models/contain.dart';
import 'package:flutter/cupertino.dart';
import 'package:econnerce_app/models/product.dart';

class Containprovider with ChangeNotifier{
  final List<Contain> sales=[
    Contain(
      name: "Evening Dress",
      price: 80,
      discount: 20,
      image:
      ("lib/images/evening dress.png"),
    ),
    Contain(
      name: "Sport Dress",
      price: 50,
      discount: 15,
      image:
      ("lib/images/sport dress.png"),
    ),
  Contain(
      name: "Sport Dress",
      price: 30,
      discount: 10,
      image:
      ("lib/images/longdress.png"),

    ),
  ];
  List<Contain> get contain => sales;
}
