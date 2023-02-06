CREATE TABLE [dbo].[ContaHistorico]
(
	[Id]        INT            NOT NULL IDENTITY(1,1), 
    [ContaId]   INT            NOT NULL, 
    [Credito]   DECIMAL(10, 2) NOT NULL, 
    [Debito]    DECIMAL(10, 2) NOT NULL, 
    [Saldo]     DECIMAL(12, 2) NOT NULL,
    [CriadoEm]  DATETIME       NOT NULL, 
    [CriadoPor] INT            NULL, 
    CONSTRAINT [PK_ContaHistorico] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_ContaHistorico_Conta] FOREIGN KEY ([ContaId]) REFERENCES [Conta]([Id]),
    CONSTRAINT [FK_ContaHistorico_LoginInsercao] FOREIGN KEY ([CriadoPor]) REFERENCES [Login]([Id])
)
GO

CREATE INDEX [IX_ContaHistorico_01] ON [dbo].[ContaHistorico] ([ContaId] ASC)