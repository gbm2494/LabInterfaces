/*Paso 3*/
CREATE PROCEDURE dbo.Login
    @pLoginName NVARCHAR(254),
    @pPassword NVARCHAR(50),
    @responseMessage bit=0 OUTPUT
AS
BEGIN

    SET NOCOUNT ON

    DECLARE @userID INT

    IF EXISTS (SELECT TOP 1 cedulaUsuario FROM [dbo].[Usuarios] WHERE nombreUsuario=@pLoginName)
    BEGIN
        SET @userID=(SELECT cedulaUsuario FROM [dbo].[Usuarios] WHERE nombreUsuario=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

       IF(@userID IS NULL)
           SET @responseMessage=0
       ELSE 
           SET @responseMessage=1
    END
    ELSE
       SET @responseMessage=0

END