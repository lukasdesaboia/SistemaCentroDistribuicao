USE CentroDistribuicao;
GO

-- Teste opcional: deve falhar sem alterar os dados.
DECLARE @IdSaida INT, @IdProduto INT;
SELECT TOP 1 @IdSaida = IdSaida FROM dbo.Saidas ORDER BY IdSaida;
SELECT @IdProduto = IdProduto FROM dbo.Produtos WHERE Codigo = '1001';

EXEC dbo.sp_RegistrarSaidaItem
    @IdSaida = @IdSaida,
    @IdProduto = @IdProduto,
    @Quantidade = 99999;
GO
