# Sistema de Centro de Distribuição

Projeto inicial em SQL Server para controle de entrada e saída de mercadorias em um centro de distribuição.

## Objetivo

Controlar:

- Fornecedores
- Produtos
- Entradas de mercadorias
- Itens das entradas
- Saídas de mercadorias
- Itens das saídas
- Estoque atual

## Estrutura

```text
SistemaCentroDistribuicao/
├── database/
│   ├── 00_CriarBanco.sql
│   ├── 01_CriarTabelas.sql
│   ├── 02_DadosTeste.sql
│   ├── 03_ConsultasTeste.sql
│   └── 04_TesteValidacao.sql
├── src/
├── docs/
├── .gitignore
└── README.md
```

## Ordem de execução

Execute os scripts no SQL Server Management Studio nesta ordem:

1. `00_CriarBanco.sql`
2. `01_CriarTabelas.sql`
3. `02_DadosTeste.sql`
4. `03_ConsultasTeste.sql`

O arquivo `04_TesteValidacao.sql` é opcional e deve gerar erro propositalmente, pois testa se o banco impede uma quantidade negativa.

## Próximas etapas

- Registrar saídas
- Validar estoque disponível
- Criar consultas e relatórios
- Integrar o banco ao C#
- Criar interface do sistema
- Adicionar tratamento de erros
