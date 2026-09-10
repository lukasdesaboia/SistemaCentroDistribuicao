# Estrutura do sistema

Fluxo principal:

Fornecedor -> Entrada -> EntradaItens -> Produto
Produto -> SaidaItens -> Saida

O estoque nao e gravado manualmente em uma coluna. Ele e calculado pela view `vw_EstoqueAtual`:

`Estoque atual = total de entradas - total de saidas`

A procedure `sp_RegistrarSaidaItem` valida o estoque antes de permitir uma nova saida.
