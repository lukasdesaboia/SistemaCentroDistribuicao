USE CentroDistribuicao;
GO

SELECT IdFornecedor, NomeFantasia, CNPJ, Ativo
FROM dbo.Fornecedores
ORDER BY NomeFantasia;
GO

SELECT IdProduto, Codigo, Descricao, Unidade, Ativo
FROM dbo.Produtos
ORDER BY Descricao;
GO

SELECT
    E.NumeroDocumento,
    F.NomeFantasia AS Fornecedor,
    P.Codigo,
    P.Descricao AS Produto,
    EI.Quantidade,
    EI.ValorUnitario,
    EI.Quantidade * EI.ValorUnitario AS ValorTotal,
    E.DataEntrada
FROM dbo.EntradaItens EI
INNER JOIN dbo.Entradas E ON E.IdEntrada = EI.IdEntrada
INNER JOIN dbo.Fornecedores F ON F.IdFornecedor = E.IdFornecedor
INNER JOIN dbo.Produtos P ON P.IdProduto = EI.IdProduto
ORDER BY E.DataEntrada DESC, P.Descricao;
GO

SELECT Codigo, Descricao, Unidade, TotalEntrada, TotalSaida, EstoqueAtual
FROM dbo.vw_EstoqueAtual
ORDER BY Descricao;
GO

SELECT
    S.NumeroDocumento,
    S.Destino,
    P.Codigo,
    P.Descricao AS Produto,
    SI.Quantidade,
    S.DataSaida
FROM dbo.SaidaItens SI
INNER JOIN dbo.Saidas S ON S.IdSaida = SI.IdSaida
INNER JOIN dbo.Produtos P ON P.IdProduto = SI.IdProduto
ORDER BY S.DataSaida DESC, P.Descricao;
GO
