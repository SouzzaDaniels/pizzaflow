import 'dart:async';
import 'package:flutter/material.dart';
import '../models/pedido.dart';
import '../services/api_service.dart';
import 'pedido_detalhe_screen.dart';

class PedidosScreen extends StatefulWidget {
  const PedidosScreen({super.key});

  @override
  State<PedidosScreen> createState() => _PedidosScreenState();
}

class _PedidosScreenState extends State<PedidosScreen> {
  final _api = ApiService();
  List<Pedido> _pedidos = [];
  Timer? _timer;
  bool _carregando = true;
  String? _erro;

  @override
  void initState() {
    super.initState();
    _carregarPedidos();
    // Long polling: consulta a fila de pedidos a cada 15 segundos, conforme escopo do MVP.
    _timer = Timer.periodic(const Duration(seconds: 15), (_) => _carregarPedidos());
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  Future<void> _carregarPedidos() async {
    try {
      final pedidos = await _api.buscarFilaDePedidos();
      if (!mounted) return;
      setState(() {
        _pedidos = pedidos;
        _carregando = false;
        _erro = null;
      });
    } catch (_) {
      if (!mounted) return;
      setState(() {
        _carregando = false;
        _erro = 'Não foi possível atualizar a fila de pedidos.';
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Fila de pedidos'),
        actions: [
          IconButton(onPressed: _carregarPedidos, icon: const Icon(Icons.refresh)),
        ],
      ),
      body: RefreshIndicator(
        onRefresh: _carregarPedidos,
        child: _carregando
            ? const Center(child: CircularProgressIndicator())
            : _erro != null
                ? Center(child: Text(_erro!))
                : _pedidos.isEmpty
                    ? const Center(child: Text('Nenhum pedido ativo no momento.'))
                    : ListView.builder(
                        itemCount: _pedidos.length,
                        itemBuilder: (context, index) {
                          final pedido = _pedidos[index];
                          return Card(
                            margin: const EdgeInsets.symmetric(horizontal: 12, vertical: 6),
                            child: ListTile(
                              title: Text('Pedido #${pedido.id} — ${pedido.clienteNome}'),
                              subtitle: Text('${pedido.status} · R\$ ${pedido.valorTotal.toStringAsFixed(2)}'),
                              trailing: const Icon(Icons.chevron_right),
                              onTap: () async {
                                await Navigator.of(context).push(
                                  MaterialPageRoute(builder: (_) => PedidoDetalheScreen(pedido: pedido)),
                                );
                                _carregarPedidos();
                              },
                            ),
                          );
                        },
                      ),
      ),
    );
  }
}
