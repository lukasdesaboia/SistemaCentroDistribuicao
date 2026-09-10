USE CentroDistribuicao;
GO

IF OBJECT_ID('dbo.Fornecedores', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Fornecedores
    (
        IdFornecedor INT IDENTITY(1,1) PRIMARY KEY,
        RazaoSocial VARCHAR(150) NOT NULL,
        NomeFantasia VARCHAR(150) NULL,
        CNPJ VARCHAR(18) NOT NULL UNIQUE,
        Telefone VARCHAR(20) NULL,
        Email VARCHAR(150) NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME2 NOT NULL DEFAULT SYSDATETIME()
    );
END
GO

IF OBJECT_ID('dbo.Produtos', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Produtos
    (
        IdProduto INT IDENTITY(1,1) PRIMARY KEY,
        Codigo VARCHAR(50) NOT NULL UNIQUE,
        Descricao VARCHAR(200) NOT NULL,
        Unidade VARCHAR(10) NOT NULL,
        Ativo BIT NOT NULL DEFAULT 1,
        DataCadastro DATETIME2 NOT NULL DEFAULT SYSDATETIME()
    );
END
GO

IF OBJECT_ID('dbo.Entradas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Entradas
    (
        IdEntrada INT IDENTITY(1,1) PRIMARY KEY,
        IdFornecedor INT NOT NULL,
        NumeroDocumento VARCHAR(50) NULL,
        DataEntrada DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
        Observacao VARCHAR(500) NULL,
        CONSTRAINT FK_Entradas_Fornecedores
            FOREIGN KEY (IdFornecedor) REFERENCES dbo.Fornecedores(IdFornecedor)
    );
END
GO

IF OBJECT_ID('dbo.EntradaItens', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.EntradaItens
    (
        IdEntradaItem INT IDENTITY(1,1) PRIMARY KEY,
        IdEntrada INT NOT NULL,
        IdProduto INT NOT NULL,
        Quantidade DECIMAL(18,3) NOT NULL,
        ValorUnitario DECIMAL(18,2) NULL,
        CONSTRAINT FK_EntradaItens_Entradas
            FOREIGN KEY (IdEntrada) REFERENCES dbo.Entradas(IdEntrada),
        CONSTRAINT FK_EntradaItens_Produtos
            FOREIGN KEY (IdProduto) REFERENCES dbo.Produtos(IdProduto),
        CONSTRAINT CK_EntradaItens_Quantidade CHECK (Quantidade > 0),
        CONSTRAINT CK_EntradaItens_ValorUnitario CHECK (ValorUnitario IS NULL OR ValorUnitario >= 0)
    );
END
GO

IF OBJECT_ID('dbo.Saidas', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Saidas
    (
        IdSaida INT IDENTITY(1,1) PRIMARY KEY,
        Destino VARCHAR(150) NOT NULL,
        NumeroDocumento VARCHAR(50) NULL,
        DataSaida DATETIME2 NOT NULL DEFAULT SYSDATETIME(),
        Observacao VARCHAR(500) NULL
    );
END
GO

IF OBJECT_ID('dbo.SaidaItens', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.SaidaItens
    (
        IdSaidaItem INT IDENTITY(1,1) PRIMARY KEY,
        IdSaida INT NOT NULL,
        IdProduto INT NOT NULL,
        Quantidade DECIMAL(18,3) NOT NULL,
        CONSTRAINT FK_SaidaItens_Saidas
            FOREIGN KEY (IdSaida) REFERENCES dbo.Saidas(IdSaida),
        CONSTRAINT FK_SaidaItens_Produtos
            FOREIGN KEY (IdProduto) REFERENCES dbo.Produtos(IdProduto),
        CONSTRAINT CK_SaidaItens_Quantidade CHECK (Quantidade > 0)
    );
END
GO
