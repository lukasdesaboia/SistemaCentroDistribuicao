using CentroDistribuicao.App.Models;
using CentroDistribuicao.App.Repositories;

namespace CentroDistribuicao.App.Menus;

public static class MenuPrincipal
{
    public static async Task ExecutarAsync()
    {
        bool continuar = true;

        while (continuar)
        {
            Console.Clear();

            Console.WriteLine("=================================");
            Console.WriteLine("    CENTRO DE DISTRIBUIÇÃO");
            Console.WriteLine("=================================");
            Console.WriteLine();
            Console.WriteLine("1 - Listar produtos");
            Console.WriteLine("2 - Cadastrar produto");
            Console.WriteLine("3 - Listar fornecedores");
            Console.WriteLine("4 - Cadastrar fornecedor");
            Console.WriteLine("5 - Registrar entrada");
            Console.WriteLine("6 - Registrar saída");
            Console.WriteLine("7 - Consultar estoque");
            Console.WriteLine("0 - Sair");
            Console.WriteLine();

            Console.Write("Escolha uma opção: ");
            string? opcao = Console.ReadLine();

            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    await ListarProdutosAsync();
                    break;

                case "2":
                    await CadastrarProdutoAsync();
                    break;

                case "3":
                    await ListarFornecedoresAsync();
                    break;

                case "4":
                    await CadastrarFornecedorAsync();
                    break;

                case "5":
                    await RegistrarEntradaAsync();
                    break;

                case "6":
                    await RegistrarSaidaAsync();
                    break;

                case "7":
                    await ConsultarEstoqueAsync();
                    break;

                case "0":
                    continuar = false;
                    Console.WriteLine("Sistema encerrado.");
                    continue;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            Console.WriteLine();
            Console.WriteLine("Pressione ENTER para continuar...");
            Console.ReadLine();
        }
    }

    private static async Task ListarProdutosAsync()
    {
        try
        {
            var repositorio = new ProdutoRepository();
            var produtos = await repositorio.ListarAsync();

            Console.WriteLine("=== PRODUTOS CADASTRADOS ===");
            Console.WriteLine();

            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            foreach (var produto in produtos)
            {
                Console.WriteLine(
                    $"ID: {produto.IdProduto} | " +
                    $"Código: {produto.Codigo} | " +
                    $"Produto: {produto.Descricao} | " +
                    $"Unidade: {produto.Unidade} | " +
                    $"Ativo: {(produto.Ativo ? "Sim" : "Não")}"
                );
            }
        }
        catch (Exception erro)
        {
            Console.WriteLine("Não foi possível consultar os produtos.");
            Console.WriteLine($"Erro: {erro.Message}");
        }
    }

    private static async Task CadastrarProdutoAsync()
    {
        Console.WriteLine("=== CADASTRAR PRODUTO ===");
        Console.WriteLine();

        Console.Write("Código do produto: ");
        string codigo = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Descrição: ");
        string descricao = Console.ReadLine()?.Trim() ?? "";

        Console.Write("Unidade (UN, CX, KG...): ");
        string unidade =
            Console.ReadLine()?.Trim().ToUpper() ?? "";

        if (string.IsNullOrWhiteSpace(codigo) ||
            string.IsNullOrWhiteSpace(descricao) ||
            string.IsNullOrWhiteSpace(unidade))
        {
            Console.WriteLine();
            Console.WriteLine(
                "Código, descrição e unidade são obrigatórios."
            );

            return;
        }

        var produto = new Produto
        {
            Codigo = codigo,
            Descricao = descricao,
            Unidade = unidade
        };

        try
        {
            var repositorio = new ProdutoRepository();

            await repositorio.CadastrarAsync(produto);

            Console.WriteLine();
            Console.WriteLine(
                "Produto cadastrado com sucesso."
            );
        }
        catch (Exception erro)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Não foi possível cadastrar o produto."
            );

            Console.WriteLine($"Erro: {erro.Message}");
        }
    }

    private static async Task ListarFornecedoresAsync()
    {
        try
        {
            var repositorio = new FornecedorRepository();
            var fornecedores = await repositorio.ListarAsync();

            Console.WriteLine(
                "=== FORNECEDORES CADASTRADOS ==="
            );

            Console.WriteLine();

            if (fornecedores.Count == 0)
            {
                Console.WriteLine(
                    "Nenhum fornecedor cadastrado."
                );

                return;
            }

            foreach (var fornecedor in fornecedores)
            {
                Console.WriteLine(
                    $"ID: {fornecedor.IdFornecedor} | " +
                    $"Fornecedor: " +
                    $"{fornecedor.NomeFantasia ?? fornecedor.RazaoSocial} | " +
                    $"CNPJ: {fornecedor.CNPJ} | " +
                    $"Telefone: {fornecedor.Telefone ?? "-"} | " +
                    $"Ativo: {(fornecedor.Ativo ? "Sim" : "Não")}"
                );
            }
        }
        catch (Exception erro)
        {
            Console.WriteLine(
                "Não foi possível consultar os fornecedores."
            );

            Console.WriteLine($"Erro: {erro.Message}");
        }
    }

    private static async Task CadastrarFornecedorAsync()
    {
        Console.WriteLine("=== CADASTRAR FORNECEDOR ===");
        Console.WriteLine();

        Console.Write("Razão social: ");
        string razaoSocial =
            Console.ReadLine()?.Trim() ?? "";

        Console.Write("Nome fantasia: ");
        string nomeFantasia =
            Console.ReadLine()?.Trim() ?? "";

        Console.Write("CNPJ: ");
        string cnpj =
            Console.ReadLine()?.Trim() ?? "";

        Console.Write("Telefone: ");
        string telefone =
            Console.ReadLine()?.Trim() ?? "";

        Console.Write("E-mail: ");
        string email =
            Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(razaoSocial) ||
            string.IsNullOrWhiteSpace(cnpj))
        {
            Console.WriteLine();
            Console.WriteLine(
                "Razão social e CNPJ são obrigatórios."
            );

            return;
        }

        var fornecedor = new Fornecedor
        {
            RazaoSocial = razaoSocial,
            NomeFantasia = nomeFantasia,
            CNPJ = cnpj,
            Telefone = telefone,
            Email = email
        };

        try
        {
            var repositorio =
                new FornecedorRepository();

            await repositorio.CadastrarAsync(fornecedor);

            Console.WriteLine();
            Console.WriteLine(
                "Fornecedor cadastrado com sucesso."
            );
        }
        catch (Exception erro)
        {
            Console.WriteLine();
            Console.WriteLine(
                "Não foi possível cadastrar o fornecedor."
            );

            Console.WriteLine($"Erro: {erro.Message}");
        }
    }

    private static async Task RegistrarEntradaAsync()
    {
        try
        {
            Console.WriteLine("=== REGISTRAR ENTRADA ===");
            Console.WriteLine();

            var fornecedorRepository =
                new FornecedorRepository();

            var fornecedores =
                await fornecedorRepository.ListarAsync();

            var fornecedoresAtivos =
                fornecedores
                    .Where(f => f.Ativo)
                    .ToList();

            if (fornecedoresAtivos.Count == 0)
            {
                Console.WriteLine(
                    "Nenhum fornecedor ativo cadastrado."
                );

                return;
            }

            Console.WriteLine("Fornecedores:");
            Console.WriteLine();

            foreach (var fornecedor in fornecedoresAtivos)
            {
                Console.WriteLine(
                    $"{fornecedor.IdFornecedor} - " +
                    $"{fornecedor.NomeFantasia ?? fornecedor.RazaoSocial}"
                );
            }

            Console.WriteLine();
            Console.Write("ID do fornecedor: ");

            if (!int.TryParse(
                    Console.ReadLine(),
                    out int idFornecedor))
            {
                Console.WriteLine(
                    "Fornecedor inválido."
                );

                return;
            }

            bool fornecedorExiste =
                fornecedoresAtivos.Any(
                    f => f.IdFornecedor == idFornecedor
                );

            if (!fornecedorExiste)
            {
                Console.WriteLine(
                    "Fornecedor não encontrado."
                );

                return;
            }

            Console.Write("Número do documento: ");

            string numeroDocumento =
                Console.ReadLine()?.Trim() ?? "";

            Console.Write("Observação: ");

            string observacao =
                Console.ReadLine()?.Trim() ?? "";

            var produtoRepository =
                new ProdutoRepository();

            var produtos =
                await produtoRepository.ListarAsync();

            var produtosAtivos =
                produtos
                    .Where(p => p.Ativo)
                    .ToList();

            if (produtosAtivos.Count == 0)
            {
                Console.WriteLine(
                    "Nenhum produto ativo cadastrado."
                );

                return;
            }

            var itens =
                new List<EntradaItemCadastro>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine(
                    "Produtos disponíveis:"
                );

                Console.WriteLine();

                foreach (var produto in produtosAtivos)
                {
                    Console.WriteLine(
                        $"{produto.IdProduto} - " +
                        $"{produto.Codigo} - " +
                        $"{produto.Descricao}"
                    );
                }

                Console.WriteLine();

                Console.Write(
                    "ID do produto (0 para finalizar): "
                );

                if (!int.TryParse(
                        Console.ReadLine(),
                        out int idProduto))
                {
                    Console.WriteLine(
                        "ID inválido."
                    );

                    continue;
                }

                if (idProduto == 0)
                {
                    break;
                }

                var produtoSelecionado =
                    produtosAtivos.FirstOrDefault(
                        p => p.IdProduto == idProduto
                    );

                if (produtoSelecionado == null)
                {
                    Console.WriteLine(
                        "Produto não encontrado."
                    );

                    continue;
                }

                if (itens.Any(
                        i => i.IdProduto == idProduto))
                {
                    Console.WriteLine(
                        "Esse produto já foi adicionado."
                    );

                    continue;
                }

                Console.Write("Quantidade: ");

                if (!decimal.TryParse(
                        Console.ReadLine(),
                        out decimal quantidade) ||
                    quantidade <= 0)
                {
                    Console.WriteLine(
                        "Quantidade inválida."
                    );

                    continue;
                }

                Console.Write(
                    "Valor unitário " +
                    "(ENTER se não quiser informar): "
                );

                string textoValor =
                    Console.ReadLine()?.Trim() ?? "";

                decimal? valorUnitario = null;

                if (!string.IsNullOrWhiteSpace(
                        textoValor))
                {
                    if (!decimal.TryParse(
                            textoValor,
                            out decimal valor) ||
                        valor < 0)
                    {
                        Console.WriteLine(
                            "Valor inválido."
                        );

                        continue;
                    }

                    valorUnitario = valor;
                }

                itens.Add(
                    new EntradaItemCadastro
                    {
                        IdProduto = idProduto,
                        Quantidade = quantidade,
                        ValorUnitario = valorUnitario
                    }
                );

                Console.WriteLine();

                Console.WriteLine(
                    $"{produtoSelecionado.Descricao} " +
                    "adicionado à entrada."
                );
            }

            if (itens.Count == 0)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "Entrada cancelada: " +
                    "nenhum produto informado."
                );

                return;
            }

            var entradaRepository =
                new EntradaRepository();

            int idEntrada =
                await entradaRepository.RegistrarAsync(
                    idFornecedor,
                    numeroDocumento,
                    observacao,
                    itens
                );

            Console.WriteLine();

            Console.WriteLine(
                "Entrada registrada com sucesso."
            );

            Console.WriteLine(
                $"Código da entrada: {idEntrada}"
            );
        }
        catch (Exception erro)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Não foi possível registrar a entrada."
            );

            Console.WriteLine(
                $"Erro: {erro.Message}"
            );
        }
    }

    private static async Task RegistrarSaidaAsync()
    {
        try
        {
            Console.WriteLine("=== REGISTRAR SAÍDA ===");
            Console.WriteLine();

            Console.Write("Destino da mercadoria: ");

            string destino =
                Console.ReadLine()?.Trim() ?? "";

            if (string.IsNullOrWhiteSpace(destino))
            {
                Console.WriteLine(
                    "O destino é obrigatório."
                );

                return;
            }

            Console.Write("Número do documento: ");

            string numeroDocumento =
                Console.ReadLine()?.Trim() ?? "";

            Console.Write("Observação: ");

            string observacao =
                Console.ReadLine()?.Trim() ?? "";

            var produtoRepository =
                new ProdutoRepository();

            var produtos =
                await produtoRepository.ListarAsync();

            var produtosAtivos =
                produtos
                    .Where(p => p.Ativo)
                    .ToList();

            if (produtosAtivos.Count == 0)
            {
                Console.WriteLine(
                    "Nenhum produto ativo cadastrado."
                );

                return;
            }

            var itens =
                new List<SaidaItemCadastro>();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("=== PRODUTOS ===");
                Console.WriteLine();

                foreach (var produto in produtosAtivos)
                {
                    Console.WriteLine(
                        $"{produto.IdProduto} - " +
                        $"{produto.Codigo} - " +
                        $"{produto.Descricao}"
                    );
                }

                Console.WriteLine();

                Console.Write(
                    "ID do produto (0 para finalizar): "
                );

                if (!int.TryParse(
                        Console.ReadLine(),
                        out int idProduto))
                {
                    Console.WriteLine(
                        "ID inválido."
                    );

                    continue;
                }

                if (idProduto == 0)
                {
                    break;
                }

                var produtoSelecionado =
                    produtosAtivos.FirstOrDefault(
                        p => p.IdProduto == idProduto
                    );

                if (produtoSelecionado == null)
                {
                    Console.WriteLine(
                        "Produto não encontrado."
                    );

                    continue;
                }

                if (itens.Any(
                        i => i.IdProduto == idProduto))
                {
                    Console.WriteLine(
                        "Esse produto já foi adicionado à saída."
                    );

                    continue;
                }

                Console.Write("Quantidade: ");

                if (!decimal.TryParse(
                        Console.ReadLine(),
                        out decimal quantidade) ||
                    quantidade <= 0)
                {
                    Console.WriteLine(
                        "Quantidade inválida."
                    );

                    continue;
                }

                itens.Add(
                    new SaidaItemCadastro
                    {
                        IdProduto = idProduto,
                        Quantidade = quantidade
                    }
                );

                Console.WriteLine();

                Console.WriteLine(
                    $"{produtoSelecionado.Descricao} " +
                    "adicionado à saída."
                );
            }

            if (itens.Count == 0)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "Saída cancelada: " +
                    "nenhum produto informado."
                );

                return;
            }

            var saidaRepository =
                new SaidaRepository();

            int idSaida =
                await saidaRepository.RegistrarAsync(
                    destino,
                    numeroDocumento,
                    observacao,
                    itens
                );

            Console.WriteLine();

            Console.WriteLine(
                "Saída registrada com sucesso."
            );

            Console.WriteLine(
                $"Código da saída: {idSaida}"
            );
        }
        catch (Exception erro)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Não foi possível registrar a saída."
            );

            Console.WriteLine(
                $"Erro: {erro.Message}"
            );
        }
    }

    private static async Task ConsultarEstoqueAsync()
    {
        try
        {
            Console.WriteLine("=== ESTOQUE ATUAL ===");
            Console.WriteLine();

            var repositorio =
                new EstoqueRepository();

            var estoque =
                await repositorio.ListarAsync();

            if (estoque.Count == 0)
            {
                Console.WriteLine(
                    "Nenhum produto encontrado no estoque."
                );

                return;
            }

            foreach (var produto in estoque)
            {
                Console.WriteLine(
                    $"Código: {produto.Codigo} | " +
                    $"Produto: {produto.Descricao}"
                );

                Console.WriteLine(
                    $"Entradas: {produto.TotalEntrada:N3} | " +
                    $"Saídas: {produto.TotalSaida:N3} | " +
                    $"Estoque: {produto.EstoqueAtual:N3} " +
                    $"{produto.Unidade}"
                );

                Console.WriteLine(
                    new string('-', 70)
                );
            }
        }
        catch (Exception erro)
        {
            Console.WriteLine();

            Console.WriteLine(
                "Não foi possível consultar o estoque."
            );

            Console.WriteLine(
                $"Erro: {erro.Message}"
            );
        }
    }
}
