using CentroDistribuicao.App.Data;
using CentroDistribuicao.App.Models;

namespace CentroDistribuicao.App.Repositories;

public class ProdutoRepository
{
    public async Task<List<Produto>> ListarAsync()
    {
        const string sql = """
            SELECT
                IdProduto,
                Codigo,
                Descricao,
                Unidade,
                Ativo
            FROM dbo.Produtos
            ORDER BY Descricao;
            """;

        var produtos = new List<Produto>();

        await using var conexao = ConexaoBanco.Criar();
        await conexao.OpenAsync();

        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;

        await using var leitor = await comando.ExecuteReaderAsync();

        while (await leitor.ReadAsync())
        {
            produtos.Add(new Produto
            {
                IdProduto = leitor.GetInt32(0),
                Codigo = leitor.GetString(1),
                Descricao = leitor.GetString(2),
                Unidade = leitor.GetString(3),
                Ativo = leitor.GetBoolean(4)
            });
        }

        return produtos;
    }

    public async Task CadastrarAsync(Produto produto)
    {
        const string sql = """
            INSERT INTO dbo.Produtos
            (
                Codigo,
                Descricao,
                Unidade
            )
            VALUES
            (
                @Codigo,
                @Descricao,
                @Unidade
            );
            """;

        await using var conexao = ConexaoBanco.Criar();
        await conexao.OpenAsync();

        await using var comando = conexao.CreateCommand();

        comando.CommandText = sql;

        comando.Parameters.AddWithValue(
            "@Codigo",
            produto.Codigo
        );

        comando.Parameters.AddWithValue(
            "@Descricao",
            produto.Descricao
        );

        comando.Parameters.AddWithValue(
            "@Unidade",
            produto.Unidade
        );

        await comando.ExecuteNonQueryAsync();
    }
}
