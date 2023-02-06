CREATE TABLE [dbo].[Conta]
(
	[Id]            INT         NOT NULL IDENTITY(1,1),
    [ClienteId]     INT         NOT NULL,
    [Agencia]       VARCHAR(4)  NOT NULL,
    [Numero]        VARCHAR(10) NOT NULL,
    [Ativa]         BIT         NOT NULL,
    [CriadoEm]      DATETIME    NOT NULL, 
    [CriadoPor]     INT         NOT NULL, 
    [AtualizadoEm]  DATETIME    NULL DEFAULT NULL, 
    [AtualizadoPor] INT         NULL DEFAULT NULL,
    CONSTRAINT [PK_Conta] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Conta_Cliente] FOREIGN KEY ([ClienteId]) REFERENCES [Cliente]([Id]),
    CONSTRAINT [FK_Conta_LoginInsercao] FOREIGN KEY ([CriadoPor]) REFERENCES [Login]([Id]), 
    CONSTRAINT [FK_Conta_LoginAtualizacao] FOREIGN KEY ([AtualizadoPor]) REFERENCES [Login]([Id])
)
GO

CREATE INDEX [IX_Conta_01] ON [dbo].[Conta] ([Agencia] ASC, [Numero] ASC)