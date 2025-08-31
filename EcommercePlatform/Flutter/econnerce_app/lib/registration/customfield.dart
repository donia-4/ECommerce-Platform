import 'package:flutter/material.dart';
class Customtextfield extends StatefulWidget {
final  String label;
 final Widget icon;

 Customtextfield({super.key,required this.label,required this.icon});

  @override
  State<Customtextfield> createState() => _CustomtextfieldState();
}

class _CustomtextfieldState extends State<Customtextfield> {
  @override
  Widget build(BuildContext context) {
    return TextField(
      decoration:InputDecoration(
          prefixIcon:widget.icon ,
          label: Text(widget.label,style: TextStyle(fontSize: 15),),
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(20),
          ),
          focusedBorder: OutlineInputBorder(
            borderSide: BorderSide(color: Colors.grey),
            borderRadius: BorderRadius.circular(20),
          )
      ),
    );
  }
}
