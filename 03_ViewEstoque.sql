USE CentroDistribuicao;
GO

CREATE OR ALTER VIEW dbo.vw_EstoqueAtual
AS
SELECT
    P.IdProduto,
    P.Codigo,
    P.Descricao,
    P.Unidade,
    COALESCE(ENT.TotalEntrada, 0) AS TotalEntrada,
    COALESCE(SAI.TotalSaida, 0) AS TotalSaida,
    COALESCE(ENT.TotalEntrada, 0) - COALESCE(SAI.TotalSaida, 0) AS EstoqueAtual
FROM dbo.Produtos P
LEFT JOIN (
    SELECT IdProduto, SUM(Quantidade) AS TotalEntrada
    FROM dbo.EntradaItens
    GROUP BY IdProduto
) ENT ON ENT.IdProduto = P.IdProduto
LEFT JOIN (
    SELECT IdProduto, SUM(Quantidade) AS TotalSaida
    FROM dbo.SaidaItens
    GROUP BY IdProduto
) SAI ON SAI.IdProduto = P.IdProduto;
GO
