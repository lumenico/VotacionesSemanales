
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/07/2017 18:58:15
-- Generated from EDMX file: C:\inetpub\wwwroot\Votaciones\Votaciones\Modelo.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [VotacionBares];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_SemanaComensal_Comensales]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SemanaComensal] DROP CONSTRAINT [FK_SemanaComensal_Comensales];
GO
IF OBJECT_ID(N'[dbo].[FK_SemanaComensal_Semanas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SemanaComensal] DROP CONSTRAINT [FK_SemanaComensal_Semanas];
GO
IF OBJECT_ID(N'[dbo].[FK_VotacionesSemanales_Bar]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotacionesSemanales] DROP CONSTRAINT [FK_VotacionesSemanales_Bar];
GO
IF OBJECT_ID(N'[dbo].[FK_VotacionesSemanales_Comensales]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotacionesSemanales] DROP CONSTRAINT [FK_VotacionesSemanales_Comensales];
GO
IF OBJECT_ID(N'[dbo].[FK_VotacionesSemanales_Semanas]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[VotacionesSemanales] DROP CONSTRAINT [FK_VotacionesSemanales_Semanas];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Bar]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bar];
GO
IF OBJECT_ID(N'[dbo].[Comensales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Comensales];
GO
IF OBJECT_ID(N'[dbo].[SemanaComensal]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SemanaComensal];
GO
IF OBJECT_ID(N'[dbo].[SemanaInforme]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SemanaInforme];
GO
IF OBJECT_ID(N'[dbo].[Semanas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Semanas];
GO
IF OBJECT_ID(N'[dbo].[VotacionesSemanales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VotacionesSemanales];
GO
IF OBJECT_ID(N'[VotacionBaresModelStoreContainer].[V_SemanaActual_Votaciones]', 'U') IS NOT NULL
    DROP TABLE [VotacionBaresModelStoreContainer].[V_SemanaActual_Votaciones];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Bar'
CREATE TABLE [dbo].[Bar] (
    [idBar] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [Direccion] nvarchar(200)  NULL,
    [Descripcion] nvarchar(2000)  NULL
);
GO

-- Creating table 'Comensales'
CREATE TABLE [dbo].[Comensales] (
    [IdComensal] int IDENTITY(1,1) NOT NULL,
    [Nombre] varchar(100)  NOT NULL,
    [Rol] varchar(1000)  NULL,
    [Image] varchar(100)  NULL
);
GO

-- Creating table 'Semanas'
CREATE TABLE [dbo].[Semanas] (
    [idSemana] int IDENTITY(1,1) NOT NULL,
    [Nombre] nvarchar(50)  NOT NULL,
    [FechaIni] datetime  NOT NULL,
    [FechaFin] datetime  NOT NULL,
    [NumSemana] int  NULL
);
GO

-- Creating table 'VotacionesSemanales'
CREATE TABLE [dbo].[VotacionesSemanales] (
    [idComensal] int  NOT NULL,
    [idBar] int  NOT NULL,
    [idSemana] int  NOT NULL,
    [Votacion] bit  NOT NULL,
    [idVotacion] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'SemanaComensal'
CREATE TABLE [dbo].[SemanaComensal] (
    [idSemana] int  NOT NULL,
    [idComensal] int  NOT NULL,
    [fecha] datetime  NOT NULL
);
GO

-- Creating table 'V_SemanaActual_Votaciones'
CREATE TABLE [dbo].[V_SemanaActual_Votaciones] (
    [Comensal] varchar(100)  NOT NULL,
    [Restaurante] varchar(50)  NOT NULL
);
GO

-- Creating table 'SemanaInforme'
CREATE TABLE [dbo].[SemanaInforme] (
    [IdSemanaInforme] int IDENTITY(1,1) NOT NULL,
    [NumSemana] int  NOT NULL,
    [IdComensal_Alfa] int  NULL,
    [IdComensal_Omega] int  NULL,
    [IdRestaurante_Ganador] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [idBar] in table 'Bar'
ALTER TABLE [dbo].[Bar]
ADD CONSTRAINT [PK_Bar]
    PRIMARY KEY CLUSTERED ([idBar] ASC);
GO

-- Creating primary key on [IdComensal] in table 'Comensales'
ALTER TABLE [dbo].[Comensales]
ADD CONSTRAINT [PK_Comensales]
    PRIMARY KEY CLUSTERED ([IdComensal] ASC);
GO

-- Creating primary key on [idSemana] in table 'Semanas'
ALTER TABLE [dbo].[Semanas]
ADD CONSTRAINT [PK_Semanas]
    PRIMARY KEY CLUSTERED ([idSemana] ASC);
GO

-- Creating primary key on [idVotacion] in table 'VotacionesSemanales'
ALTER TABLE [dbo].[VotacionesSemanales]
ADD CONSTRAINT [PK_VotacionesSemanales]
    PRIMARY KEY CLUSTERED ([idVotacion] ASC);
GO

-- Creating primary key on [idSemana], [idComensal] in table 'SemanaComensal'
ALTER TABLE [dbo].[SemanaComensal]
ADD CONSTRAINT [PK_SemanaComensal]
    PRIMARY KEY CLUSTERED ([idSemana], [idComensal] ASC);
GO

-- Creating primary key on [Comensal], [Restaurante] in table 'V_SemanaActual_Votaciones'
ALTER TABLE [dbo].[V_SemanaActual_Votaciones]
ADD CONSTRAINT [PK_V_SemanaActual_Votaciones]
    PRIMARY KEY CLUSTERED ([Comensal], [Restaurante] ASC);
GO

-- Creating primary key on [IdSemanaInforme] in table 'SemanaInforme'
ALTER TABLE [dbo].[SemanaInforme]
ADD CONSTRAINT [PK_SemanaInforme]
    PRIMARY KEY CLUSTERED ([IdSemanaInforme] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [idBar] in table 'VotacionesSemanales'
ALTER TABLE [dbo].[VotacionesSemanales]
ADD CONSTRAINT [FK_VotacionesSemanales_Bar]
    FOREIGN KEY ([idBar])
    REFERENCES [dbo].[Bar]
        ([idBar])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VotacionesSemanales_Bar'
CREATE INDEX [IX_FK_VotacionesSemanales_Bar]
ON [dbo].[VotacionesSemanales]
    ([idBar]);
GO

-- Creating foreign key on [idComensal] in table 'VotacionesSemanales'
ALTER TABLE [dbo].[VotacionesSemanales]
ADD CONSTRAINT [FK_VotacionesSemanales_Comensales]
    FOREIGN KEY ([idComensal])
    REFERENCES [dbo].[Comensales]
        ([IdComensal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VotacionesSemanales_Comensales'
CREATE INDEX [IX_FK_VotacionesSemanales_Comensales]
ON [dbo].[VotacionesSemanales]
    ([idComensal]);
GO

-- Creating foreign key on [idSemana] in table 'VotacionesSemanales'
ALTER TABLE [dbo].[VotacionesSemanales]
ADD CONSTRAINT [FK_VotacionesSemanales_Semanas]
    FOREIGN KEY ([idSemana])
    REFERENCES [dbo].[Semanas]
        ([idSemana])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_VotacionesSemanales_Semanas'
CREATE INDEX [IX_FK_VotacionesSemanales_Semanas]
ON [dbo].[VotacionesSemanales]
    ([idSemana]);
GO

-- Creating foreign key on [idComensal] in table 'SemanaComensal'
ALTER TABLE [dbo].[SemanaComensal]
ADD CONSTRAINT [FK_SemanaComensal_Comensales]
    FOREIGN KEY ([idComensal])
    REFERENCES [dbo].[Comensales]
        ([IdComensal])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SemanaComensal_Comensales'
CREATE INDEX [IX_FK_SemanaComensal_Comensales]
ON [dbo].[SemanaComensal]
    ([idComensal]);
GO

-- Creating foreign key on [idSemana] in table 'SemanaComensal'
ALTER TABLE [dbo].[SemanaComensal]
ADD CONSTRAINT [FK_SemanaComensal_Semanas]
    FOREIGN KEY ([idSemana])
    REFERENCES [dbo].[Semanas]
        ([idSemana])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------