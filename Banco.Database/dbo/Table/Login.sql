CREATE TABLE [dbo].[Login]
(
	[Id]            INT         NOT NULL IDENTITY(1,1),
    [Usuario]       VARCHAR(50) NOT NULL, 
    [Senha]         VARCHAR(50) NOT NULL,
    [CriadoEm]      DATETIME    NOT NULL, 
    [CriadoPor]     INT         NOT NULL, 
    [AtualizadoEm]  DATETIME    NULL, 
    [AtualizadoPor] INT         NULL, 
    CONSTRAINT [PK_Login] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Login_Insercao] FOREIGN KEY ([CriadoPor]) REFERENCES [Login]([Id]), 
    CONSTRAINT [FK_Login_Atualizacao] FOREIGN KEY ([AtualizadoPor]) REFERENCES [Login]([Id])
)
GO

CREATE INDEX [IX_Login_01] ON [dbo].[Login] ([Usuario] ASC, [Senha] ASC)