IF DB_ID('CentroDistribuicao') IS NULL
BEGIN
    CREATE DATABASE CentroDistribuicao;
END
GO

USE CentroDistribuicao;
GO

SELECT DB_NAME() AS BancoAtual;
GO
