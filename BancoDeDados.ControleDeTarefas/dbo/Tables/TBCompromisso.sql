CREATE TABLE [dbo].[TBCompromisso] (
    [id]          INT          IDENTITY (1, 1) NOT NULL,
    [assunto]     VARCHAR (50) NOT NULL,
    [local]       VARCHAR (50) NOT NULL,
    [data]        DATE         NOT NULL,
    [HoraInicio]  VARCHAR(50)     NOT NULL,
    [HoraTermino] VARCHAR(50)     NOT NULL,
    [idContato]   INT          NULL,
    [link]        VARCHAR (50) NULL,
    CONSTRAINT [PK_TBCompromisso] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_TBCompromisso_TBContatos] FOREIGN KEY ([idContato]) REFERENCES [dbo].[TBContatos] ([Id])
);

