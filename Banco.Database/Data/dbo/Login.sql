-- ===================================
-- FUNCAO: Cria o Login padrão
--------------------------------------
-- 03/02/2023 - Richard Ponciano - Criação do script.
--====================================

IF NOT EXISTS(SELECT TOP 1 1 FROM dbo.[Login] WHERE Usuario = 'ADM')
BEGIN
	SET IDENTITY_INSERT [dbo].[Login] ON 

	INSERT INTO dbo.[Login] (Id, Usuario, Senha, CriadoEm, CriadoPor) 
	VALUES (1, 'ADM', 'ADM123', GETDATE(), 1)

	SET IDENTITY_INSERT [dbo].[Login] OFF
END