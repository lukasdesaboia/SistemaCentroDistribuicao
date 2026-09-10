namespace CentroDistribuicao.App.Models;

public class ProdutoEstoque
{
    public int IdProduto { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public decimal TotalEntrada { get; set; }
    public decimal TotalSaida { get; set; }
    public decimal EstoqueAtual { get; set; }
}
