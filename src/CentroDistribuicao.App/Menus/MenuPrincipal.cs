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
                    Console.WriteLine("Listagem de produtos.");
                    break;

                case "2":
                    Console.WriteLine("Cadastro de produto.");
                    break;

                case "3":
                    Console.WriteLine("Listagem de fornecedores.");
                    break;

                case "4":
                    Console.WriteLine("Cadastro de fornecedor.");
                    break;

                case "5":
                    Console.WriteLine("Registro de entrada.");
                    break;

                case "6":
                    Console.WriteLine("Registro de saída.");
                    break;

                case "7":
                    Console.WriteLine("Consulta de estoque.");
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

            await Task.CompletedTask;
        }
    }
}
