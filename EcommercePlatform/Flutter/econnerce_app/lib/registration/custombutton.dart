import 'package:flutter/material.dart';
class Custonbuttons extends StatefulWidget {
  final String name;
  Custonbuttons({super.key,required this.name});

  @override
  State<Custonbuttons> createState() => _CustonbuttonsState();
}

class _CustonbuttonsState extends State<Custonbuttons> {
  @override
  Widget build(BuildContext context) {
    return Container(
      margin: EdgeInsets.only(left: 10,right: 10,top: 30,bottom: 130),
      width: 500,
      height: 48,
      child: MaterialButton(onPressed: (){},
        child: Text(widget.name,style: TextStyle(fontSize: 18,fontWeight: FontWeight.w600,color: Colors.white),),
        color: Colors.green,
        shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.all(Radius.circular(40))
        ),
      ),
    );
  }
}
