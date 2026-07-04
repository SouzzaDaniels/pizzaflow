import 'package:flutter/material.dart';
import '../models/pedido.dart';
import '../services/api_service.dart';

class PedidoDetalheScreen extends StatefulWidget {
  final Pedido pedido;

  const PedidoDetalheScreen({super.key, required this.pedido});

  @override
  State<PedidoDetalheScreen> createState() => _PedidoDetalheScreenState();
}

class _PedidoDetalheScreenState extends State<PedidoDetalheScreen> {
  final _api = ApiService();
  late String _statusAtual;
  bool _atualizando = false;

  @override
  void initState() {
    super.initState();
    _statusAtual = widget.pedido.status;
  }

  Future<void> _avancarStatus() async {
    final novo = proximoStatus(_statusAtual);
    if (novo == _statusAtual) return;

    setState(() => _atualizando = true);
    try {
      await _api.atualizarStatus(widget.pedido.id, novo);
      if (!mounted) return;
      setState(() {
        _statusAtual = novo;
        _atualizando = false;
      });
    } catch (_) {
      if (!mounted) return;
      setState(() => _atualizando = false);
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Não foi possível atualizar o status.')),
      );
    }
  }

  @override
  Widget build(BuildContext context) {
    final pedido = widget.pedido;
    final ultimaEtapa = _statusAtual == etapasPedido.last;

    return Scaffold(
      appBar: AppBar(title: Text('Pedido #${pedido.id}')),
      body: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Text('Cliente: ${pedido.clienteNome}', style: const TextStyle(fontWeight: FontWeight.bold)),
            Text('Telefone: ${pedido.clienteTelefone}'),
            Text('Endereço: ${pedido.clienteEndereco}'),
            const SizedBox(height: 16),
            Text('Status atual: $_statusAtual', style: const TextStyle(fontSize: 18)),
            const SizedBox(height: 16),
            const Text('Itens do pedido:', style: TextStyle(fontWeight: FontWeight.bold)),
            ...pedido.itens.map(
              (item) => Text('${item.quantidade}x ${item.pizzaNome} — R\$ ${item.precoUnitario.toStringAsFixed(2)}'),
            ),
            const SizedBox(height: 16),
            Text('Total: R\$ ${pedido.valorTotal.toStringAsFixed(2)}', style: const TextStyle(fontSize: 16)),
            const Spacer(),
            ElevatedButton(
              onPressed: (_atualizando || ultimaEtapa) ? null : _avancarStatus,
              child: Text(
                ultimaEtapa
                    ? 'Pedido entregue'
                    : _atualizando
                        ? 'Atualizando...'
                        : 'Avançar para "${proximoStatus(_statusAtual)}"',
              ),
            ),
          ],
        ),
      ),
    );
  }
}
