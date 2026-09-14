using System.Data;
using CentroDistribuicao.App.Data;
using CentroDistribuicao.App.Models;

namespace CentroDistribuicao.App.Repositories;

public class SaidaRepository
{
    public async Task<int> RegistrarAsync(
        string destino,
        string? numeroDocumento,
        string? observacao,
        List<SaidaItemCadastro> itens)
    {
        if (string.IsNullOrWhiteSpace(destino))
        {
            throw new Exception("O destino é obrigatório.");
        }

        if (itens.Count == 0)
        {
            throw new Exception(
                "A saída precisa possuir pelo menos um produto."
            );
        }

        await using var conexao = ConexaoBanco.Criar();
        await conexao.OpenAsync();

        using var transacao = conexao.BeginTransaction();

        try
        {
            const string sqlSaida = """
                INSERT INTO dbo.Saidas
                (
                    Destino,
                    NumeroDocumento,
                    Observacao
                )
                OUTPUT INSERTED.IdSaida
                VALUES
                (
                    @Destino,
                    @NumeroDocumento,
                    @Observacao
                );
                """;

            await using var comandoSaida =
                conexao.CreateCommand();

            comandoSaida.Transaction = transacao;
            comandoSaida.CommandText = sqlSaida;

            comandoSaida.Parameters.AddWithValue(
                "@Destino",
                destino
            );

            comandoSaida.Parameters.AddWithValue(
                "@NumeroDocumento",
                string.IsNullOrWhiteSpace(numeroDocumento)
                    ? DBNull.Value
                    : numeroDocumento
            );

            comandoSaida.Parameters.AddWithValue(
                "@Observacao",
                string.IsNullOrWhiteSpace(observacao)
                    ? DBNull.Value
                    : observacao
            );

            object? resultado =
                await comandoSaida.ExecuteScalarAsync();

            if (resultado == null ||
                resultado == DBNull.Value)
            {
                throw new Exception(
                    "Não foi possível gerar o código da saída."
                );
            }

            int idSaida =
                Convert.ToInt32(resultado);

            foreach (var item in itens)
            {
                await using var comandoItem =
                    conexao.CreateCommand();

                comandoItem.Transaction = transacao;

                comandoItem.CommandType =
                    CommandType.StoredProcedure;

                comandoItem.CommandText =
                    "dbo.sp_RegistrarSaidaItem";

                comandoItem.Parameters.AddWithValue(
                    "@IdSaida",
                    idSaida
                );

                comandoItem.Parameters.AddWithValue(
                    "@IdProduto",
                    item.IdProduto
                );

                comandoItem.Parameters.AddWithValue(
                    "@Quantidade",
                    item.Quantidade
                );

                await comandoItem.ExecuteNonQueryAsync();
            }

            transacao.Commit();

            return idSaida;
        }
        catch
        {
            try
            {
                transacao.Rollback();
            }
            catch
            {
                // A procedure pode já ter cancelado
                // a transação em caso de erro.
            }

            throw;
        }
    }
}
