import 'package:econnerce_app/models/product.dart';
import 'package:flutter/cupertino.dart';

class Productprovider with ChangeNotifier{

  final List<Product> items = [
    Product(
      name: "Blouse",
      price: 125,
      oldPrice: 155,
      discount: 20,
      image:
     ("lib/images/blouse.png"),
    ),
    Product(
      name: "Longsleeve Violeta",
      price: 45,
      oldPrice: 225,
      discount: 15,
      image:
     ("lib/images/violate.png"),
    ),
    Product(
      name: "T-Shirt",
      price: 10,
      oldPrice: 180,
      discount: 10,
      image:
      ("lib/images/tshirt.png"),

    ),
  ];
  List<Product> get products => items;



}