 /*----------------------------------------------------------------------------
-- Autor....: Richard Ponciano
-- Criacao..: 02/02/2023
-- Descricao: Atribui permissão em todos os objetos do banco de dados assim como Db_DataReader e Db_DataWriter
------------------------------------------------------------------------------*/
SET NOCOUNT ON
GO
 
DECLARE @user           SYSNAME
DECLARE @CMD1           VARCHAR(8000)
DECLARE @MAXOID         INT
DECLARE @OwnerName      VARCHAR(128)
DECLARE @ObjectName     VARCHAR(128)
DECLARE @FuncDataType   VARCHAR(50)
 
CREATE TABLE #StoredProcedures
    (OID                INT     IDENTITY (1,1),
     StoredProcOwner    VARCHAR(128) NOT NULL,
     StoredProcName     VARCHAR(128) NOT NULL,
     FuncDataType       VARCHAR(50) NULL)
 
-- objects list
INSERT INTO #StoredProcedures (StoredProcOwner, StoredProcName, FuncDataType)
SELECT ROUTINE_SCHEMA, ROUTINE_NAME, DATA_TYPE
    FROM INFORMATION_SCHEMA.ROUTINES 
    WHERE ROUTINE_NAME NOT LIKE 'dt_%'
    AND ROUTINE_TYPE IN ('PROCEDURE' , 'FUNCTION')
UNION ALL 
SELECT SCHEMA_NAME(schema_id), [name], 'USERTYPE' 
    FROM sys.types 
    WHERE is_user_defined = 1
 
-- users list
DECLARE MS_CRS_C1 CURSOR FOR
SELECT name FROM sys.sysusers 
 WHERE name IN ('app.salespower') --- usuários dos ambientes
 ORDER BY name
 
OPEN MS_CRS_C1
FETCH MS_CRS_C1 INTO @user
 
WHILE @@FETCH_STATUS >= 0
BEGIN
 
    EXEC sp_addrolemember 'db_datawriter', @user
    EXEC sp_addrolemember 'db_datareader', @user
 
    SELECT @MAXOID = MAX(OID) FROM #StoredProcedures
 
    WHILE @MAXOID > 0
    BEGIN 
 
        SELECT @OwnerName = StoredProcOwner,
            @ObjectName = StoredProcName,
            @FuncDataType = FuncDataType
        FROM #StoredProcedures
        WHERE OID = @MAXOID
 
        IF @FuncDataType = 'TABLE' -- func do tipo TABLE 
        BEGIN
            SELECT @CMD1 = 'GRANT SELECT ON ' + '[' + @OwnerName + ']' + '.' + '[' + @ObjectName + ']' + ' TO [' + @user + ']'
            --SELECT @CMD1 = 'REVOKE SELECT ON ' + '[' + @OwnerName + ']' + '.' + '[' + @ObjectName + ']' + ' TO ' + @user
        END 
        ELSE -- procedure ou func normal
        BEGIN
            SELECT @CMD1 = 'GRANT EXECUTE ON ' + '[' + @OwnerName + ']' + '.' + '[' + @ObjectName + ']' + ' TO [' + @user + ']'
           --SELECT @CMD1 = 'REVOKE EXECUTE ON ' + '[' + @OwnerName + ']' + '.' + '[' + @ObjectName + ']' + ' TO ' + @user
        END
 
        IF @FuncDataType = 'USERTYPE' -- func do tipo USERTYPE --- GRANT EXECUTE ON TYPE::[dbo].[uttCodigosDeIntegracao] to [usuario]
        BEGIN
            SELECT @CMD1 = 'GRANT EXECUTE ON TYPE::' + '[' + @OwnerName + ']' + '.' + '[' + @ObjectName + ']' + ' TO [' + @user + ']'
        END 
 
        EXEC(@CMD1)
        --PRINT(@CMD1)
        SET @MAXOID = @MAXOID - 1
 
    END
    
    FETCH MS_CRS_C1 INTO @user
 
END
DEALLOCATE MS_CRS_C1
 
DROP TABLE #StoredProcedures
 
SET NOCOUNT OFF
GO