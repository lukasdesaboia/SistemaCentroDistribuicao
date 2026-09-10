using CentroDistribuicao.App.Repositories;namespace CentroDistribuicao.App.Menus;

public static class MenuPrincipal
{
    public static async Task ExecutarAsync()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("    CENTRO DE DISTRIBUIÇÃO");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("1 - Listar produtos");
            Console.WriteLine("2 - Cadastrar produto");
            Console.WriteLine("3 - Listar fornecedores");
            Console.WriteLine("4 - Cadastrar fornecedor");
            Console.WriteLine("5 - Registrar entrada");
            Console.WriteLine("6 - Registrar saída");
            Console.WriteLine("7 - Consultar estoque");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();

            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine();

            Console.WriteLine();

            switch (opcao)
            {
case "1":
    await ListarProdutosAsync();
    break;

                case "2":
                   await CadastrarProdutoAsync();
                   break;

                case "3":
                    Console.WriteLine("Listagem de fornecedores.");
                    break;

                case "4":
                    Console.WriteLine("Cadastro de fornecedor.");
                    break;

                case "5":
                    Console.WriteLine("Registro de entrada.");
                    break;

                case "6":
                    Console.WriteLine("Registro de saída.");
                    break;

                case "7":
                    Console.WriteLine("Consulta de estoque.");
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Sistema encerrado.");
                    continue;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();

            await Task.CompletedTask;
        }
    }
private static async Task ListarProdutosAsync()
{
    try
    {
        var repositorio = new ProdutoRepository();

        var produtos = await repositorio.ListarAsync();

        Console.WriteLine("=== PRODUTOS CADASTRADOS ===");
        Console.WriteLine();

        if (produtos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        foreach (var produto in produtos)
        {
            Console.WriteLine(
                $"ID: {produto.IdProduto} | " +
                $"Código: {produto.Codigo} | " +
                $"Produto: {produto.Descricao} | " +
                $"Unidade: {produto.Unidade} | " +
                $"Ativo: {(produto.Ativo ? "Sim" : "Não")}"
            );
        }
    }
    catch (Exception erro)
    {
        Console.WriteLine("Não foi possível consultar os produtos.");
        Console.WriteLine($"Erro: {erro.Message}");
    }
}    // switch e restante do menu...

    private static async Task ListarProdutosAsync()
    {
        // código da consulta
    }
private static async Task CadastrarProdutoAsync()
{
    Console.WriteLine("=== CADASTRAR PRODUTO ===");
    Console.WriteLine();

    Console.Write("Código do produto: ");
    string codigo = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Descrição: ");
    string descricao = Console.ReadLine()?.Trim() ?? "";

    Console.Write("Unidade (UN, CX, KG...): ");
    string unidade = Console.ReadLine()?.Trim().ToUpper() ?? "";

    if (string.IsNullOrWhiteSpace(codigo) ||
        string.IsNullOrWhiteSpace(descricao) ||
        string.IsNullOrWhiteSpace(unidade))
    {
        Console.WriteLine();
        Console.WriteLine("Código, descrição e unidade são obrigatórios.");
        return;
    }

    var produto = new CentroDistribuicao.App.Models.Produto
    {
        Codigo = codigo,
        Descricao = descricao,
        Unidade = unidade
    };

    try
    {
        var repositorio = new ProdutoRepository();

        await repositorio.CadastrarAsync(produto);

        Console.WriteLine();
        Console.WriteLine("Produto cadastrado com sucesso.");
    }
    catch (Exception erro)
    {
        Console.WriteLine();
        Console.WriteLine("Não foi possível cadastrar o produto.");
        Console.WriteLine($"Erro: {erro.Message}");
    }
}
