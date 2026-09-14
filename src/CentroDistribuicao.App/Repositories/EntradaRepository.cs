using CentroDistribuicao.App.Data;
using CentroDistribuicao.App.Models;

namespace CentroDistribuicao.App.Repositories;

public class EntradaRepository
{
    public async Task<int> RegistrarAsync(
        int idFornecedor,
        string? numeroDocumento,
        string? observacao,
        List<EntradaItemCadastro> itens)
    {
        if (itens.Count == 0)
        {
            throw new Exception(
                "A entrada precisa possuir pelo menos um produto."
            );
        }

        await using var conexao = ConexaoBanco.Criar();
        await conexao.OpenAsync();

        using var transacao = conexao.BeginTransaction();

        try
        {
            const string sqlEntrada = """
                INSERT INTO dbo.Entradas
                (
                    IdFornecedor,
                    NumeroDocumento,
                    Observacao
                )
                OUTPUT INSERTED.IdEntrada
                VALUES
                (
                    @IdFornecedor,
                    @NumeroDocumento,
                    @Observacao
                );
                """;

            await using var comandoEntrada =
                conexao.CreateCommand();

            comandoEntrada.Transaction = transacao;
            comandoEntrada.CommandText = sqlEntrada;

            comandoEntrada.Parameters.AddWithValue(
                "@IdFornecedor",
                idFornecedor
            );

            comandoEntrada.Parameters.AddWithValue(
                "@NumeroDocumento",
                string.IsNullOrWhiteSpace(numeroDocumento)
                    ? DBNull.Value
                    : numeroDocumento
            );

            comandoEntrada.Parameters.AddWithValue(
                "@Observacao",
                string.IsNullOrWhiteSpace(observacao)
                    ? DBNull.Value
                    : observacao
            );

            object? resultado =
                await comandoEntrada.ExecuteScalarAsync();

            if (resultado == null ||
                resultado == DBNull.Value)
            {
                throw new Exception(
                    "Não foi possível gerar o código da entrada."
                );
            }

            int idEntrada =
                Convert.ToInt32(resultado);

            const string sqlItem = """
                INSERT INTO dbo.EntradaItens
                (
                    IdEntrada,
                    IdProduto,
                    Quantidade,
                    ValorUnitario
                )
                VALUES
                (
                    @IdEntrada,
                    @IdProduto,
                    @Quantidade,
                    @ValorUnitario
                );
                """;

            foreach (var item in itens)
            {
                await using var comandoItem =
                    conexao.CreateCommand();

                comandoItem.Transaction = transacao;
                comandoItem.CommandText = sqlItem;

                comandoItem.Parameters.AddWithValue(
                    "@IdEntrada",
                    idEntrada
                );

                comandoItem.Parameters.AddWithValue(
                    "@IdProduto",
                    item.IdProduto
                );

                comandoItem.Parameters.AddWithValue(
                    "@Quantidade",
                    item.Quantidade
                );

                comandoItem.Parameters.AddWithValue(
                    "@ValorUnitario",
                    item.ValorUnitario.HasValue
                        ? item.ValorUnitario.Value
                        : DBNull.Value
                );

                await comandoItem.ExecuteNonQueryAsync();
            }

            transacao.Commit();

            return idEntrada;
        }
        catch
        {
            transacao.Rollback();
            throw;
        }
    }
}
