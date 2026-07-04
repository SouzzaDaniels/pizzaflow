import 'package:flutter/material.dart';
import 'screens/login_screen.dart';

void main() {
  runApp(const PizzaFlowGestorApp());
}

class PizzaFlowGestorApp extends StatelessWidget {
  const PizzaFlowGestorApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'PizzaFlow Gestor',
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        colorSchemeSeed: Colors.red,
        useMaterial3: true,
      ),
      home: const LoginScreen(),
    );
  }
}
