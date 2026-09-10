using CentroDistribuicao.App.Data;
using CentroDistribuicao.App.Repositories;

Console.WriteLine("======================================");
Console.WriteLine("   SISTEMA CENTRO DE DISTRIBUICAO");
Console.WriteLine("======================================");
Console.WriteLine();

try
{
    await using (var conexao = ConexaoBanco.Criar())
    {
        await conexao.OpenAsync();
        Console.WriteLine("SQL Server conectado com sucesso.");
    }

    Console.WriteLine();
    Console.WriteLine("Estoque atual:");
    Console.WriteLine();

    var estoqueRepository = new EstoqueRepository();
    var produtos = await estoqueRepository.ListarAsync();

    if (produtos.Count == 0)
    {
        Console.WriteLine("Nenhum produto encontrado.");
        return;
    }

    Console.WriteLine($"{"Codigo",-10} {"Produto",-30} {"Un.",-6} {"Entradas",10} {"Saidas",10} {"Estoque",10}");
    Console.WriteLine(new string('-', 82));

    foreach (var produto in produtos)
    {
        Console.WriteLine(
            $"{produto.Codigo,-10} {produto.Descricao,-30} {produto.Unidade,-6} " +
            $"{produto.TotalEntrada,10:N3} {produto.TotalSaida,10:N3} {produto.EstoqueAtual,10:N3}");
    }
}
catch (Exception ex)
{
    Console.WriteLine();
    Console.WriteLine("Nao foi possivel acessar o banco de dados.");
    Console.WriteLine($"Detalhes: {ex.Message}");
}
