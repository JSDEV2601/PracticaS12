
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/11/2025 23:16:41
-- Generated from EDMX file: C:\Users\zid\Desktop\tarea\PracticaS12\PracticaS12\EF\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PracticaS12];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Abonos_Principal]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Abonos] DROP CONSTRAINT [FK_Abonos_Principal];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Abonos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Abonos];
GO
IF OBJECT_ID(N'[dbo].[Principal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Principal];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Abonos'
CREATE TABLE [dbo].[Abonos] (
    [Id_Compra] bigint  NOT NULL,
    [Id_Abono] bigint IDENTITY(1,1) NOT NULL,
    [Monto] decimal(18,2)  NOT NULL,
    [Fecha] datetime  NOT NULL
);
GO

-- Creating table 'Principal'
CREATE TABLE [dbo].[Principal] (
    [Id_Compra] bigint IDENTITY(1,1) NOT NULL,
    [Precio] decimal(18,5)  NOT NULL,
    [Saldo] decimal(18,5)  NOT NULL,
    [Descripcion] varchar(500)  NOT NULL,
    [Estado] varchar(100)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_Abono] in table 'Abonos'
ALTER TABLE [dbo].[Abonos]
ADD CONSTRAINT [PK_Abonos]
    PRIMARY KEY CLUSTERED ([Id_Abono] ASC);
GO

-- Creating primary key on [Id_Compra] in table 'Principal'
ALTER TABLE [dbo].[Principal]
ADD CONSTRAINT [PK_Principal]
    PRIMARY KEY CLUSTERED ([Id_Compra] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Compra] in table 'Abonos'
ALTER TABLE [dbo].[Abonos]
ADD CONSTRAINT [FK_Abonos_Principal]
    FOREIGN KEY ([Id_Compra])
    REFERENCES [dbo].[Principal]
        ([Id_Compra])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Abonos_Principal'
CREATE INDEX [IX_FK_Abonos_Principal]
ON [dbo].[Abonos]
    ([Id_Compra]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------