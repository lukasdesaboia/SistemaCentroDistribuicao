using CentroDistribuicao.App.Data;
using CentroDistribuicao.App.Models;

namespace CentroDistribuicao.App.Repositories;

public class FornecedorRepository
{
    public async Task<List<Fornecedor>> ListarAsync()
    {
        const string sql = """
            SELECT
                IdFornecedor,
                RazaoSocial,
                NomeFantasia,
                CNPJ,
                Telefone,
                Email,
                Ativo
            FROM dbo.Fornecedores
            ORDER BY NomeFantasia, RazaoSocial;
            """;

        var fornecedores = new List<Fornecedor>();

        await using var conexao = ConexaoBanco.Criar();
        await conexao.OpenAsync();

        await using var comando = conexao.CreateCommand();
        comando.CommandText = sql;

        await using var leitor = await comando.ExecuteReaderAsync();

        while (await leitor.ReadAsync())
        {
            fornecedores.Add(new Fornecedor
            {
                IdFornecedor = leitor.GetInt32(0),
                RazaoSocial = leitor.GetString(1),
                NomeFantasia = leitor.IsDBNull(2) ? null : leitor.GetString(2),
                CNPJ = leitor.GetString(3),
                Telefone = leitor.IsDBNull(4) ? null : leitor.GetString(4),
                Email = leitor.IsDBNull(5) ? null : leitor.GetString(5),
                Ativo = leitor.GetBoolean(6)
            });
        }

        return fornecedores;
    }
}
