import 'package:flutter/cupertino.dart';
class Product {
  final String name;
  final double price;
  final double oldPrice;
  final int discount;
  final String image;


  Product({
    required this.name,
    required this.price,
    required this.oldPrice,
    required this.discount,
    required this.image,
  });
}
