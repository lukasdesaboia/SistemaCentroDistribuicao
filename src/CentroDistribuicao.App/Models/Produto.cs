namespace CentroDistribuicao.App.Models;

public class Produto
{
    public int IdProduto { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string Descricao { get; set; } = string.Empty;

    public string Unidade { get; set; } = string.Empty;

    public bool Ativo { get; set; }
}
