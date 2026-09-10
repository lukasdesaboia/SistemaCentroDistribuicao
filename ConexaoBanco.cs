using Microsoft.Data.SqlClient;

namespace CentroDistribuicao.App.Data;

public static class ConexaoBanco
{
    private const string ConexaoPadrao =
        @"Server=localhost\SQLEXPRESS;Database=CentroDistribuicao;Integrated Security=True;TrustServerCertificate=True;";

    public static SqlConnection Criar()
    {
        string conexao = Environment.GetEnvironmentVariable("CD_CONNECTION_STRING") ?? ConexaoPadrao;
        return new SqlConnection(conexao);
    }
}
