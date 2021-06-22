CREATE TABLE [dbo].[TBContatos] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Nome]     VARCHAR (200) NOT NULL,
    [Email]    VARCHAR (200) NOT NULL,
    [Telefone] VARCHAR (200) NOT NULL,
    [Empresa]  VARCHAR (200) NOT NULL,
    [Cargo]    VARCHAR (200) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

