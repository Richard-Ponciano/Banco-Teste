/*----------------------------------------------------------------------------
-- Autor....: Richard Ponciano
-- Criacao..: 02/02/2023
-- Descricao: Execução de scripts de dados
------------------------------------------------------------------------------*/

/*************************** BLOCO 1: SCRIPTS DE CORREÇÃO DE DADOS COM CHAMADA ÚNICA ********************************/
/************************************ SEMPRE REMOVER CHAMADAS E SCRIPTS APOS RELEASE EM PRODUCAO ********************************************/
PRINT ''
PRINT '*** PostDeployment: Scripts de correcao de dados'
PRINT ''

----PRINT 'Ajuste qqr coisa'
--:r .\correcao\AjusteQqrCoisa.sql
--GO

GO
/**************************************** BLOCO 2: SCRIPTS DE CARGA DE DADOS PADRAO DA APLICACAO  *******************************************/
PRINT ''
PRINT '*** PostDeployment: Scripts de dados'
PRINT ''

:r .\dbo\Login.sql
GO


/*************************** BLOCO 3: SCRIPTS DE CORREÇÃO DE DADOS COM CHAMADA ÚNICA ********************************/
/************************************ SEMPRE REMOVER CHAMADAS E SCRIPTS APOS RELEASE EM PRODUCAO ********************************************/

--PRINT 'Correcao Qqr Coisa'
--:r .\correcao\Correcao_QqrCoisa.sql
--GO



/********************************************** BLOCO 4: SCRIPTS DE PERMISSAO E FINALIZACAO ************************************************/
/***************************************** FAVOR DEIXAR ESSA CHAMADA NA ÚLTIMA LINHA DESSE SCRIPT ******************************************/
 -- 
:r ..\Script\GrantAllObjects.sql
:r ..\Script\Dacpac.sql  
GO


/***************************************************************** THE END *****************************************************************/
PRINT ''
PRINT '*** PostDeployment: Final'
PRINT ''
GO