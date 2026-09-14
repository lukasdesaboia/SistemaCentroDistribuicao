# Sistema de Centro de Distribuição

Projeto que desenvolvi para praticar a integração entre C# e SQL Server.

A ideia foi montar um sistema simples de centro de distribuição, onde é possível cadastrar produtos e fornecedores, registrar entradas e saídas de mercadorias e consultar o estoque atual.

O projeto foi feito pensando mais na lógica e no funcionamento do que em interface gráfica, por isso a aplicação funciona pelo console.

## Tecnologias utilizadas

- C# / .NET 10
- SQL Server Express
- Microsoft.Data.SqlClient
- DataGrip
- VS Code
- GitHub

## Funcionalidades

O sistema atualmente possui:

- Listagem de produtos
- Cadastro de produtos
- Listagem de fornecedores
- Cadastro de fornecedores
- Registro de entrada de mercadorias
- Registro de saída de mercadorias
- Consulta de estoque
- Validação para impedir saída maior que o estoque disponível

## Menu do sistema

```text
=== CENTRO DE DISTRIBUIÇÃO ===

1 - Listar produtos
2 - Cadastrar produto
3 - Listar fornecedores
4 - Cadastrar fornecedor
5 - Registrar entrada
6 - Registrar saída
7 - Consultar estoque
0 - Sair
