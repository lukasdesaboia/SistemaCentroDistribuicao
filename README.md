# Sistema de Centro de Distribuicao

Projeto em C# e SQL Server para controle de entrada, saida e estoque de mercadorias em um centro de distribuicao.

## Tecnologias

- C# / .NET 10
- SQL Server Express
- Microsoft.Data.SqlClient
- DataGrip
- VS Code

## Funcionalidades atuais

- Cadastro de fornecedores e produtos
- Registro de entradas e saidas
- Calculo de estoque
- View de estoque atual
- Validacao para impedir saida acima do estoque
- Aplicacao C# conectando ao SQL Server e exibindo o estoque

## Ordem dos scripts

1. `database/00_CriarBanco.sql`
2. `database/01_CriarTabelas.sql`
3. `database/02_DadosTeste.sql`
4. `database/03_ViewEstoque.sql`
5. `database/04_ProcedureRegistrarSaida.sql`
6. `database/05_Consultas.sql`

`06_TesteValidacao.sql` e opcional e deve gerar erro de estoque insuficiente.

## Executar o C#

```powershell
cd src/CentroDistribuicao.App
dotnet restore
dotnet run
```

A conexao padrao usa `localhost\SQLEXPRESS`, banco `CentroDistribuicao` e autenticacao do Windows.

## Dados de demonstracao

Os dados dos scripts sao ficticios. Nomes comerciais podem ser reais apenas para deixar os exemplos naturais; CNPJ, telefone, documentos e movimentacoes nao representam dados reais das empresas.
