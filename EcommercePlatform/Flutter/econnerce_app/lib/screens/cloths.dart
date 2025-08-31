import 'package:econnerce_app/models/contain.dart';
import 'package:econnerce_app/providers/containprovider.dart';
import 'package:flutter/material.dart';
import 'package:econnerce_app/models/product.dart';
import 'package:econnerce_app/providers/productprovider.dart';
import 'package:provider/provider.dart';
class Cloths extends StatefulWidget {
  const Cloths({super.key});

  @override
  State<Cloths> createState() => _ClothsState();
}

class _ClothsState extends State<Cloths> {
  @override
  Widget build(BuildContext context) {
    final contain = Provider.of<Containprovider>(context).sales;
    final products = Productprovider().products;
    return Scaffold(
      bottomNavigationBar: BottomNavigationBar(
        type: BottomNavigationBarType.fixed,
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: "Home"),
          BottomNavigationBarItem(icon: Icon(Icons.shopping_cart_sharp), label: "Shop"),
          BottomNavigationBarItem(icon: Icon(Icons.shopping_bag), label: "Bag"),
          BottomNavigationBarItem(icon: Icon(Icons.favorite_border), label: "Favorites"),
          BottomNavigationBarItem(icon: Icon(Icons.person), label: "Profile"),
        ],
        selectedItemColor: Colors.red[600],
        unselectedItemColor: Colors.grey,
        selectedFontSize: 15,
        selectedLabelStyle: TextStyle(fontWeight: FontWeight.w500),
      ),
      body: SingleChildScrollView(
        child:Column(
          children: [
                  Stack(
                    children: [
                      Image.asset("lib/images/cloths.jpg"),
                      Positioned(
                        bottom: 20,
                          left: 20,
                          child:Text(
                            "Street clothes",
                            style: TextStyle(
                              fontSize: 35,
                              fontWeight: FontWeight.bold,
                              color: Colors.white,
                            ),
                          ), )
                    ],
                  ),
            const SizedBox(height: 20),


            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: const [
                  Text(
                    "Sale",
                    style: TextStyle(fontSize: 34, fontWeight: FontWeight.bold),
                  ),
                  Text(
                    "View all",
                    style: TextStyle(
                      fontSize: 14,
                      color: Colors.red,
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                ],
              ),
            ),
            Padding(
              padding: const EdgeInsets.only(right: 300),
              child: Text(" Super summer sale",style: TextStyle(fontSize: 11,color: Colors.grey),),
            ),
        SizedBox(height: 20,),
        SizedBox(
            height: 280,
            child: ListView.builder(
                scrollDirection: Axis.horizontal,
                itemCount: contain.length,
                itemBuilder: (context, i) {
                  final Contain product = contain[i];
                  double finalPrice = product.price * (1 - product.discount / 100);

                  return Container(
                    width: 150,
                    margin: EdgeInsets.only(left: 12, right: 4),
                    child: Card(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15),
                      ),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Stack(
                            children: [
                              Image.asset(
                                product.image,
                                height: 200,
                                width: double.infinity,
                                fit: BoxFit.contain,

                              ),
                              if (product.discount > 0)
                                Positioned(
                                  top: 8,
                                  left: 8,
                                  child: Container(
                                    padding: const EdgeInsets.symmetric(horizontal: 6, vertical: 2),
                                    decoration: BoxDecoration(
                                      color: Colors.red,
                                      borderRadius: BorderRadius.circular(12),
                                    ),
                                    child: Text(
                                      "-${product.discount}%",
                                      style: const TextStyle(color: Colors.white, fontSize: 12),
                                    ),
                                  ),
                                ),
                            ],
                          ),Padding(
                            padding: const EdgeInsets.all(8.0),
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(
                                  product.name,
                                  style: TextStyle(
                                    fontWeight: FontWeight.bold,
                                    fontSize: 14,
                                  ),
                                ),
                                const SizedBox(height: 4),
                                Row(
                                  children: [
                                    if (product.discount > 0)
                                      Text(
                                        "\$${product.price}",
                                        style: TextStyle(
                                          fontSize: 12,
                                          color: Colors.grey,
                                          decoration: TextDecoration.lineThrough,
                                        ),
                                      ),
                                    SizedBox(width: 6),
                                    Text(
                                      "\$${finalPrice.toStringAsFixed(2)}",
                                      style: TextStyle(
                                        fontSize: 14,
                                        color: Colors.red,
                                        fontWeight: FontWeight.bold,
                                      ),
                                    ),
                                  ],
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  );
                },
            ),
        ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 16.0),
              child: Row(
                mainAxisAlignment: MainAxisAlignment.spaceBetween,
                children: const [
                  Text(
                    "New",
                    style: TextStyle(fontSize: 34, fontWeight: FontWeight.bold),
                  ),
                  Text(
                    "View all",
                    style: TextStyle(
                      fontSize: 14,
                      color: Colors.red,
                      fontWeight: FontWeight.w600,
                    ),
                  ),
                ],
              ),
            ),
            const SizedBox(height: 10),
            SizedBox(
              height: 280,
              child: ListView.builder(
                scrollDirection: Axis.horizontal,
                itemCount: products.length,
                itemBuilder: (context, i) {
                  final Product product = products[i];


                  return Container(
                    width: 150,
                    margin: EdgeInsets.only(left: 12, right: 4),
                    child: Card(
                      shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.circular(15),
                      ),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Stack(
                            children: [
                              Image.asset(
                                product.image,
                                height: 200,
                                width: double.infinity,
                                fit: BoxFit.contain,

                              ),

                              Positioned(
                                top: 8,
                                left: 8,
                                child: Container(
                                  padding: const EdgeInsets.symmetric(horizontal: 6, vertical: 2),
                                  decoration: BoxDecoration(
                                    color: Colors.black,
                                    borderRadius: BorderRadius.circular(12),
                                  ),
                                  child: Text(
                                    "New",
                                    style: const TextStyle(color: Colors.white, fontSize: 12),
                                  ),
                                ),
                              ),
                            ],
                          ),

                          Padding(
                            padding: const EdgeInsets.all(8.0),
                            child: Column(
                              crossAxisAlignment: CrossAxisAlignment.start,
                              children: [
                                Text(
                                  product.name,
                                  style: TextStyle(
                                    fontWeight: FontWeight.bold,
                                    fontSize: 14,
                                  ),
                                ),
                                const SizedBox(height: 4),
                                Text(
                                  "\$${product.price.toStringAsFixed(2)}",
                                  style: TextStyle(
                                    fontSize: 14,
                                    color: Colors.red,
                                    fontWeight: FontWeight.bold,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ],
                      ),
                    ),
                  );
                },
              ),
            )
          ],
        ),
      ),
    );
  }
}
