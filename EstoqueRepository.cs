using CentroDistribuicao.App.Data;
using CentroDistribuicao.App.Models;

namespace CentroDistribuicao.App.Repositories;

public class EstoqueRepository
{
    public async Task<List<ProdutoEstoque>> ListarAsync()
    {
        const string sql = """
            SELECT IdProduto, Codigo, Descricao, Unidade, TotalEntrada, TotalSaida, EstoqueAtual
            FROM dbo.vw_EstoqueAtual
            ORDER BY Descricao;
            """;

        var produtos = new List<ProdutoEstoque>();
        await using var conexao = ConexaoBanco.Criar();
        await conexao.OpenAsync();

        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;

        await using var leitor = await comando.ExecuteReaderAsync();
        while (await leitor.ReadAsync())
        {
            produtos.Add(new ProdutoEstoque
            {
                IdProduto = leitor.GetInt32(0),
                Codigo = leitor.GetString(1),
                Descricao = leitor.GetString(2),
                Unidade = leitor.GetString(3),
                TotalEntrada = leitor.GetDecimal(4),
                TotalSaida = leitor.GetDecimal(5),
                EstoqueAtual = leitor.GetDecimal(6)
            });
        }
        return produtos;
    }
}
