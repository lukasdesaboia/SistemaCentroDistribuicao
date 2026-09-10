USE CentroDistribuicao;
GO

-- Dados ficticios para demonstracao.
-- Nomes comerciais podem ser reais, mas CNPJ, telefone, documentos e movimentacoes sao inventados.

IF NOT EXISTS (SELECT 1 FROM dbo.Fornecedores WHERE CNPJ = '11.111.111/0001-11')
BEGIN
    INSERT INTO dbo.Fornecedores (RazaoSocial, NomeFantasia, CNPJ, Telefone, Email)
    VALUES ('Nestlé Brasil Ltda', 'Nestlé', '11.111.111/0001-11', '(11) 99999-1111', 'teste@nestle.com');
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Produtos WHERE Codigo = '1001')
    INSERT INTO dbo.Produtos (Codigo, Descricao, Unidade) VALUES ('1001', 'Nescau 400g', 'UN');
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Produtos WHERE Codigo = '1002')
    INSERT INTO dbo.Produtos (Codigo, Descricao, Unidade) VALUES ('1002', 'Leite Moça 395g', 'UN');
GO
IF NOT EXISTS (SELECT 1 FROM dbo.Produtos WHERE Codigo = '1003')
    INSERT INTO dbo.Produtos (Codigo, Descricao, Unidade) VALUES ('1003', 'Nescafé Tradição 200g', 'UN');
GO

DECLARE @IdFornecedor INT;
SELECT @IdFornecedor = IdFornecedor FROM dbo.Fornecedores WHERE CNPJ = '11.111.111/0001-11';

IF NOT EXISTS (SELECT 1 FROM dbo.Entradas WHERE NumeroDocumento = 'NF-000123')
BEGIN
    INSERT INTO dbo.Entradas (IdFornecedor, NumeroDocumento, Observacao)
    VALUES (@IdFornecedor, 'NF-000123', 'Recebimento de mercadoria - carga de demonstracao');
END
GO

DECLARE @IdEntrada INT, @IdNescau INT, @IdLeiteMoca INT, @IdNescafe INT;
SELECT @IdEntrada = IdEntrada FROM dbo.Entradas WHERE NumeroDocumento = 'NF-000123';
SELECT @IdNescau = IdProduto FROM dbo.Produtos WHERE Codigo = '1001';
SELECT @IdLeiteMoca = IdProduto FROM dbo.Produtos WHERE Codigo = '1002';
SELECT @IdNescafe = IdProduto FROM dbo.Produtos WHERE Codigo = '1003';

IF NOT EXISTS (SELECT 1 FROM dbo.EntradaItens WHERE IdEntrada = @IdEntrada AND IdProduto = @IdNescau)
    INSERT INTO dbo.EntradaItens (IdEntrada, IdProduto, Quantidade, ValorUnitario) VALUES (@IdEntrada, @IdNescau, 50, 8.50);
IF NOT EXISTS (SELECT 1 FROM dbo.EntradaItens WHERE IdEntrada = @IdEntrada AND IdProduto = @IdLeiteMoca)
    INSERT INTO dbo.EntradaItens (IdEntrada, IdProduto, Quantidade, ValorUnitario) VALUES (@IdEntrada, @IdLeiteMoca, 30, 7.90);
IF NOT EXISTS (SELECT 1 FROM dbo.EntradaItens WHERE IdEntrada = @IdEntrada AND IdProduto = @IdNescafe)
    INSERT INTO dbo.EntradaItens (IdEntrada, IdProduto, Quantidade, ValorUnitario) VALUES (@IdEntrada, @IdNescafe, 20, 12.50);
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Saidas WHERE NumeroDocumento = 'SAI-000001')
BEGIN
    INSERT INTO dbo.Saidas (Destino, NumeroDocumento, Observacao)
    VALUES ('OXXO - Loja 001', 'SAI-000001', 'Abastecimento de loja - demonstracao');
END
GO

DECLARE @IdSaida INT, @Produto1001 INT, @Produto1002 INT, @Produto1003 INT;
SELECT @IdSaida = IdSaida FROM dbo.Saidas WHERE NumeroDocumento = 'SAI-000001';
SELECT @Produto1001 = IdProduto FROM dbo.Produtos WHERE Codigo = '1001';
SELECT @Produto1002 = IdProduto FROM dbo.Produtos WHERE Codigo = '1002';
SELECT @Produto1003 = IdProduto FROM dbo.Produtos WHERE Codigo = '1003';

IF NOT EXISTS (SELECT 1 FROM dbo.SaidaItens WHERE IdSaida = @IdSaida AND IdProduto = @Produto1001)
    INSERT INTO dbo.SaidaItens (IdSaida, IdProduto, Quantidade) VALUES (@IdSaida, @Produto1001, 10);
IF NOT EXISTS (SELECT 1 FROM dbo.SaidaItens WHERE IdSaida = @IdSaida AND IdProduto = @Produto1002)
    INSERT INTO dbo.SaidaItens (IdSaida, IdProduto, Quantidade) VALUES (@IdSaida, @Produto1002, 5);
IF NOT EXISTS (SELECT 1 FROM dbo.SaidaItens WHERE IdSaida = @IdSaida AND IdProduto = @Produto1003)
    INSERT INTO dbo.SaidaItens (IdSaida, IdProduto, Quantidade) VALUES (@IdSaida, @Produto1003, 4);
GO
