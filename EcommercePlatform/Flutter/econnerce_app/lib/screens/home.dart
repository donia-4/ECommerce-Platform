import 'package:econnerce_app/models/product.dart';
import 'package:econnerce_app/providers/productprovider.dart';
import 'package:econnerce_app/screens/cloths.dart';
import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  @override
  Widget build(BuildContext context) {
    final products = Productprovider().products;
    final productprovider = Provider.of<Productprovider>(context);

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
        child: Column(
          children: [
           Stack(
              children: [
                Image.asset("lib/images/fashion.png"),
                Positioned(
                  bottom: 50,
                  left: 15,
                  child: Column(
                    children: [
                      Text(
                        "Fashion\nSale",
                        style: TextStyle(
                          fontSize: 50,
                          fontWeight: FontWeight.bold,
                          color: Colors.white,
                        ),
                      ),
                      Container(
                        height: 40,
                        width: 170,
                        decoration: BoxDecoration(
                          borderRadius: BorderRadius.all(Radius.circular(30)),
                          color: Colors.red,
                        ),
                        child: MaterialButton(
                          onPressed: () {
                            Navigator.of(context).push(MaterialPageRoute(builder: (context)=>Cloths()));
                          },
                          child: Text(
                            "Check",
                            style: TextStyle(fontSize: 15, color: Colors.white),
                          ),
                        ),
                      )
                    ],
                  ),
                ),
              ],
            ),

            const SizedBox(height: 20),


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
