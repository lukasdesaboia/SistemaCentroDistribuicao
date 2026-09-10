USE CentroDistribuicao;
GO

CREATE OR ALTER PROCEDURE dbo.sp_RegistrarSaidaItem
    @IdSaida INT,
    @IdProduto INT,
    @Quantidade DECIMAL(18,3)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF @Quantidade <= 0
        THROW 50001, 'A quantidade deve ser maior que zero.', 1;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS (SELECT 1 FROM dbo.Saidas WHERE IdSaida = @IdSaida)
            THROW 50002, 'Saida nao encontrada.', 1;

        IF NOT EXISTS (
            SELECT 1 FROM dbo.Produtos WITH (UPDLOCK, HOLDLOCK)
            WHERE IdProduto = @IdProduto AND Ativo = 1
        )
            THROW 50003, 'Produto nao encontrado ou inativo.', 1;

        DECLARE @TotalEntrada DECIMAL(18,3), @TotalSaida DECIMAL(18,3), @EstoqueAtual DECIMAL(18,3);

        SELECT @TotalEntrada = COALESCE(SUM(Quantidade), 0)
        FROM dbo.EntradaItens WHERE IdProduto = @IdProduto;

        SELECT @TotalSaida = COALESCE(SUM(Quantidade), 0)
        FROM dbo.SaidaItens WHERE IdProduto = @IdProduto;

        SET @EstoqueAtual = @TotalEntrada - @TotalSaida;

        IF @Quantidade > @EstoqueAtual
            THROW 50004, 'Estoque insuficiente para realizar a saida.', 1;

        INSERT INTO dbo.SaidaItens (IdSaida, IdProduto, Quantidade)
        VALUES (@IdSaida, @IdProduto, @Quantidade);

        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
