CREATE TABLE [dbo].[Cliente]
(
	[Id]            INT          NOT NULL IDENTITY(1,1),
    [LoginId]       INT          NOT NULL,
    [Documento]     VARCHAR(14)  NOT NULL, 
    [PessoaFisica]  BIT          NOT NULL, 
    [Nome]          VARCHAR(50)  NOT NULL, 
    [Sobrenome]     VARCHAR(100) NOT NULL, 
    [CriadoEm]      DATETIME     NOT NULL, 
    [CriadoPor]     INT          NOT NULL, 
    [AtualizadoEm]  DATETIME     NULL DEFAULT NULL, 
    [AtualizadoPor] INT          NULL DEFAULT NULL,
    CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cliente_Login] FOREIGN KEY ([LoginId]) REFERENCES [Login]([Id]),
    CONSTRAINT [FK_Cliente_LoginInsercao] FOREIGN KEY ([CriadoPor]) REFERENCES [Login]([Id]), 
    CONSTRAINT [FK_Cliente_LoginAtualizacao] FOREIGN KEY ([AtualizadoPor]) REFERENCES [Login]([Id]), 
    
)
GO

CREATE INDEX [IX_Cliente_01] ON [dbo].[Cliente] ([Documento] ASC)