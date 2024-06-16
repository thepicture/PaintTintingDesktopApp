
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/16/2024 16:13:05
-- Generated from EDMX file: C:\Users\vboxuser\source\repos\thepicture\PaintTintingDesktopApp\Models\Entities\BaseModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PaintTintingBase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[PaintTintingBaseModelStoreContainer].[FK_Contact_Customer]', 'F') IS NOT NULL
    ALTER TABLE [PaintTintingBaseModelStoreContainer].[Contact] DROP CONSTRAINT [FK_Contact_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Paint]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Paint];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Paint_FirstParent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paint] DROP CONSTRAINT [FK_Paint_FirstParent];
GO
IF OBJECT_ID(N'[dbo].[FK_Paint_PaintProvider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paint] DROP CONSTRAINT [FK_Paint_PaintProvider];
GO
IF OBJECT_ID(N'[dbo].[FK_Paint_SecondParent]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Paint] DROP CONSTRAINT [FK_Paint_SecondParent];
GO
IF OBJECT_ID(N'[dbo].[FK_User_UserType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_UserType];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[Paint]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Paint];
GO
IF OBJECT_ID(N'[dbo].[PaintProvider]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PaintProvider];
GO
IF OBJECT_ID(N'[dbo].[Service]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Service];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO
IF OBJECT_ID(N'[dbo].[UserType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserType];
GO
IF OBJECT_ID(N'[PaintTintingBaseModelStoreContainer].[Contact]', 'U') IS NOT NULL
    DROP TABLE [PaintTintingBaseModelStoreContainer].[Contact];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customer'
CREATE TABLE [dbo].[Customer] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Order'
CREATE TABLE [dbo].[Order] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [PaintId] int  NOT NULL,
    [NumberOfPaint] int  NOT NULL,
    [CustomerId] int  NOT NULL,
    [SellerId] int  NOT NULL,
    [DateTime] datetime  NOT NULL
);
GO

-- Creating table 'Paint'
CREATE TABLE [dbo].[Paint] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ColorAsHex] nvarchar(9)  NOT NULL,
    [ProductName] nvarchar(100)  NOT NULL,
    [PaintProviderId] int  NOT NULL,
    [PackagingInLiters] decimal(20,2)  NOT NULL,
    [PriceInRubles] decimal(19,4)  NOT NULL,
    [FirstParentId] int  NULL,
    [SecondParentId] int  NULL
);
GO

-- Creating table 'PaintProvider'
CREATE TABLE [dbo].[PaintProvider] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(100)  NOT NULL
);
GO

-- Creating table 'Service'
CREATE TABLE [dbo].[Service] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(255)  NOT NULL,
    [PriceInRubles] decimal(19,4)  NOT NULL,
    [Description] nvarchar(2048)  NULL
);
GO

-- Creating table 'User'
CREATE TABLE [dbo].[User] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserTypeId] int  NOT NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [Patronymic] nvarchar(50)  NULL,
    [Login] nvarchar(50)  NOT NULL,
    [PasswordHash] varbinary(max)  NOT NULL,
    [Salt] varbinary(16)  NOT NULL,
    [Photo] varbinary(max)  NULL
);
GO

-- Creating table 'UserType'
CREATE TABLE [dbo].[UserType] (
    [Id] int  NOT NULL,
    [Title] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Contact'
CREATE TABLE [dbo].[Contact] (
    [Id] int  NOT NULL,
    [PhoneNumber] nvarchar(11)  NOT NULL,
    [Email] nvarchar(100)  NULL,
    [CustomerId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customer'
ALTER TABLE [dbo].[Customer]
ADD CONSTRAINT [PK_Customer]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [PK_Order]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Paint'
ALTER TABLE [dbo].[Paint]
ADD CONSTRAINT [PK_Paint]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PaintProvider'
ALTER TABLE [dbo].[PaintProvider]
ADD CONSTRAINT [PK_PaintProvider]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Service'
ALTER TABLE [dbo].[Service]
ADD CONSTRAINT [PK_Service]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [PK_User]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserType'
ALTER TABLE [dbo].[UserType]
ADD CONSTRAINT [PK_UserType]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id], [PhoneNumber], [CustomerId] in table 'Contact'
ALTER TABLE [dbo].[Contact]
ADD CONSTRAINT [PK_Contact]
    PRIMARY KEY CLUSTERED ([Id], [PhoneNumber], [CustomerId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerId] in table 'Contact'
ALTER TABLE [dbo].[Contact]
ADD CONSTRAINT [FK_Contact_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Contact_Customer'
CREATE INDEX [IX_FK_Contact_Customer]
ON [dbo].[Contact]
    ([CustomerId]);
GO

-- Creating foreign key on [CustomerId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_Order_Customer]
    FOREIGN KEY ([CustomerId])
    REFERENCES [dbo].[Customer]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Customer'
CREATE INDEX [IX_FK_Order_Customer]
ON [dbo].[Order]
    ([CustomerId]);
GO

-- Creating foreign key on [PaintId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_Order_Paint]
    FOREIGN KEY ([PaintId])
    REFERENCES [dbo].[Paint]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Paint'
CREATE INDEX [IX_FK_Order_Paint]
ON [dbo].[Order]
    ([PaintId]);
GO

-- Creating foreign key on [SellerId] in table 'Order'
ALTER TABLE [dbo].[Order]
ADD CONSTRAINT [FK_Order_User]
    FOREIGN KEY ([SellerId])
    REFERENCES [dbo].[User]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_User'
CREATE INDEX [IX_FK_Order_User]
ON [dbo].[Order]
    ([SellerId]);
GO

-- Creating foreign key on [FirstParentId] in table 'Paint'
ALTER TABLE [dbo].[Paint]
ADD CONSTRAINT [FK_Paint_FirstParent]
    FOREIGN KEY ([FirstParentId])
    REFERENCES [dbo].[Paint]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Paint_FirstParent'
CREATE INDEX [IX_FK_Paint_FirstParent]
ON [dbo].[Paint]
    ([FirstParentId]);
GO

-- Creating foreign key on [PaintProviderId] in table 'Paint'
ALTER TABLE [dbo].[Paint]
ADD CONSTRAINT [FK_Paint_PaintProvider]
    FOREIGN KEY ([PaintProviderId])
    REFERENCES [dbo].[PaintProvider]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Paint_PaintProvider'
CREATE INDEX [IX_FK_Paint_PaintProvider]
ON [dbo].[Paint]
    ([PaintProviderId]);
GO

-- Creating foreign key on [SecondParentId] in table 'Paint'
ALTER TABLE [dbo].[Paint]
ADD CONSTRAINT [FK_Paint_SecondParent]
    FOREIGN KEY ([SecondParentId])
    REFERENCES [dbo].[Paint]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Paint_SecondParent'
CREATE INDEX [IX_FK_Paint_SecondParent]
ON [dbo].[Paint]
    ([SecondParentId]);
GO

-- Creating foreign key on [UserTypeId] in table 'User'
ALTER TABLE [dbo].[User]
ADD CONSTRAINT [FK_User_UserType]
    FOREIGN KEY ([UserTypeId])
    REFERENCES [dbo].[UserType]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_UserType'
CREATE INDEX [IX_FK_User_UserType]
ON [dbo].[User]
    ([UserTypeId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------