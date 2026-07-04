import 'package:flutter/material.dart';
import '../services/api_service.dart';
import 'pedidos_screen.dart';

class LoginScreen extends StatefulWidget {
  const LoginScreen({super.key});

  @override
  State<LoginScreen> createState() => _LoginScreenState();
}

class _LoginScreenState extends State<LoginScreen> {
  final _api = ApiService();
  final _telefoneController = TextEditingController();
  final _senhaController = TextEditingController();
  bool _carregando = false;
  String? _erro;

  Future<void> _entrar() async {
    setState(() {
      _carregando = true;
      _erro = null;
    });

    try {
      final sucesso = await _api.login(_telefoneController.text.trim(), _senhaController.text);
      if (!mounted) return;

      if (sucesso) {
        Navigator.of(context).pushReplacement(
          MaterialPageRoute(builder: (_) => const PedidosScreen()),
        );
      } else {
        setState(() => _erro = 'Telefone ou senha inválidos.');
      }
    } catch (_) {
      setState(() => _erro = 'Não foi possível conectar à API.');
    } finally {
      if (mounted) setState(() => _carregando = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('PizzaFlow — Gestor')),
      body: Padding(
        padding: const EdgeInsets.all(24.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            TextField(
              controller: _telefoneController,
              decoration: const InputDecoration(labelText: 'Telefone'),
              keyboardType: TextInputType.phone,
            ),
            const SizedBox(height: 16),
            TextField(
              controller: _senhaController,
              decoration: const InputDecoration(labelText: 'Senha'),
              obscureText: true,
            ),
            const SizedBox(height: 24),
            if (_erro != null) Text(_erro!, style: const TextStyle(color: Colors.red)),
            const SizedBox(height: 8),
            ElevatedButton(
              onPressed: _carregando ? null : _entrar,
              child: _carregando ? const CircularProgressIndicator() : const Text('Entrar'),
            ),
          ],
        ),
      ),
    );
  }
}
