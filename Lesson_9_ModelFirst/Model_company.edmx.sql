
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/01/2025 20:45:31
-- Generated from EDMX file: C:\Users\Sergienko\source\repos\AdoNet\Lesson_9_ModelFirst\Model_company.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [New_company_PV_425_MF];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomersPictures]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PicturesSet] DROP CONSTRAINT [FK_CustomersPictures];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CustomersSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomersSet];
GO
IF OBJECT_ID(N'[dbo].[PicturesSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PicturesSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CustomersSet'
CREATE TABLE [dbo].[CustomersSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NULL
);
GO

-- Creating table 'PicturesSet'
CREATE TABLE [dbo].[PicturesSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [NamePicture] nvarchar(max)  NOT NULL,
    [CustomersId] int  NOT NULL
);
GO

-- Creating table 'EmployeeSet'
CREATE TABLE [dbo].[EmployeeSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Salary] decimal(18,0)  NOT NULL,
    [PositionId] int  NOT NULL
);
GO

-- Creating table 'PositionSet'
CREATE TABLE [dbo].[PositionSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PostionName] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CustomersSet'
ALTER TABLE [dbo].[CustomersSet]
ADD CONSTRAINT [PK_CustomersSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PicturesSet'
ALTER TABLE [dbo].[PicturesSet]
ADD CONSTRAINT [PK_PicturesSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [PK_EmployeeSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PositionSet'
ALTER TABLE [dbo].[PositionSet]
ADD CONSTRAINT [PK_PositionSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomersId] in table 'PicturesSet'
ALTER TABLE [dbo].[PicturesSet]
ADD CONSTRAINT [FK_CustomersPictures]
    FOREIGN KEY ([CustomersId])
    REFERENCES [dbo].[CustomersSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomersPictures'
CREATE INDEX [IX_FK_CustomersPictures]
ON [dbo].[PicturesSet]
    ([CustomersId]);
GO

-- Creating foreign key on [PositionId] in table 'EmployeeSet'
ALTER TABLE [dbo].[EmployeeSet]
ADD CONSTRAINT [FK_PositionEmployee]
    FOREIGN KEY ([PositionId])
    REFERENCES [dbo].[PositionSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PositionEmployee'
CREATE INDEX [IX_FK_PositionEmployee]
ON [dbo].[EmployeeSet]
    ([PositionId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------

INSERT INTO CustomersSet(FirstName,LastName,BirthDate)
VALUES('FN','LN','2025-01-10 08:30:00');