CREATE PROCEDURE dbo.agregarUsuario
    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
	@cedula varchar(9),
    @responseMessage bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON

    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

        INSERT INTO dbo.[Usuarios] (cedulaUsuario, nombreUsuario, PasswordHash, Salt)
        VALUES(@cedula, @pLogin, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt)

       SET @responseMessage=1

    END TRY
    BEGIN CATCH
        SET @responseMessage=ERROR_MESSAGE() 
    END CATCH

END