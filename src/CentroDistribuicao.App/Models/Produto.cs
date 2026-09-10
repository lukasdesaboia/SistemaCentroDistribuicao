namespace CentroDistribuicao.App.Models;

public class Produto
{
    public int IdProduto { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Unidade { get; set; } = string.Empty;
    public bool Ativo { get; set; }
namespace CentroDistribuicao.App.Models;

public class Fornecedor
{
    public int IdFornecedor { get; set; }
    public string RazaoSocial { get; set; } = string.Empty;
    public string? NomeFantasia { get; set; }
    public string CNPJ { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public bool Ativo { get; set; }
}}
