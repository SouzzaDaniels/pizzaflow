import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:shared_preferences/shared_preferences.dart';
import '../models/pedido.dart';

class ApiService {
  // Troque pelo endereço do backend publicado no Render antes de gerar o APK final.
  // Em testes locais com emulador Android, use 10.0.2.2 no lugar de localhost.
  static const String baseUrl = 'https://pizzaflow-api-gsf9.onrender.com';

  Future<String?> _getToken() async {
    final prefs = await SharedPreferences.getInstance();
    return prefs.getString('token');
  }

  Future<void> _salvarToken(String token) async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.setString('token', token);
  }

  Future<void> logout() async {
    final prefs = await SharedPreferences.getInstance();
    await prefs.remove('token');
  }

  Future<bool> login(String telefone, String senha) async {
    print("Tentando login com telefone: '$telefone' | senha: '$senha'");

    final resposta = await http
        .post(
          Uri.parse('$baseUrl/api/auth/login'),
          headers: {'Content-Type': 'application/json'},
          body: jsonEncode({'telefone': telefone, 'senha': senha}),
        )
        .timeout(const Duration(seconds: 10));

    print("Status Code: ${resposta.statusCode}");
    print("Resposta: ${resposta.body}");

    if (resposta.statusCode == 200) {
      final dados = jsonDecode(resposta.body);
      await _salvarToken(dados['token']);
      return true;
    }
    return false;
  }

  Future<Map<String, String>> _headersAutenticados() async {
    final token = await _getToken();
    return {
      'Content-Type': 'application/json',
      if (token != null) 'Authorization': 'Bearer $token',
    };
  }

  /// Busca a fila de pedidos ativos (usado no ciclo de long polling a cada 15s).
  Future<List<Pedido>> buscarFilaDePedidos() async {
    final headers = await _headersAutenticados();
    final resposta = await http.get(Uri.parse('$baseUrl/api/admin/pedidos'),
        headers: headers);

    if (resposta.statusCode != 200) {
      throw Exception('Falha ao buscar pedidos (${resposta.statusCode})');
    }

    final List lista = jsonDecode(utf8.decode(resposta.bodyBytes));
    return lista.map((p) => Pedido.fromJson(p)).toList();
  }

  Future<void> atualizarStatus(int pedidoId, String novoStatus) async {
    final headers = await _headersAutenticados();
    final resposta = await http.put(
      Uri.parse('$baseUrl/api/admin/pedidos/$pedidoId/status'),
      headers: headers,
      body: jsonEncode({'status': novoStatus}),
    );

    if (resposta.statusCode != 200) {
      throw Exception('Falha ao atualizar status (${resposta.statusCode})');
    }
  }
}
