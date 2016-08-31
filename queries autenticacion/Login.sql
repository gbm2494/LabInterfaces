/*Paso 3*/
CREATE PROCEDURE dbo.Login
	/*Par�metros: pLoginName donde se recibe el nombre de usuario,
	pPassword donde se recibe la contrase�a,
	y se declara el par�metro de salida isInDB que devuelve 1 si el usuario
	si est� en la BD o 0 si no est�*/
    @pLoginName NVARCHAR(254),
    @pPassword NVARCHAR(50),
    @isInDB bit=0 OUTPUT
AS
BEGIN

    SET NOCOUNT ON

	/*Se declara variable para buscar el usuario*/
    DECLARE @userID INT

	/*Se pregunta si existe una cedulaUsuario que tenga en nombreUsuario lo recibido en pLoginName*/
    IF EXISTS (SELECT TOP 1 cedulaUsuario FROM [dbo].[Usuarios] WHERE nombreUsuario=@pLoginName)
    BEGIN
		/*Si si existe una c�dula con este nombreUsuario se pregunta si el Password de dicho usuario
		corresponde al recibido por par�metro junto con el salt de esa tupla*/
        SET @userID=(SELECT cedulaUsuario FROM [dbo].[Usuarios] WHERE nombreUsuario=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

		/*si al final de ambas consultas userID es null se retorna 0*/
       IF(@userID IS NULL)
           SET @isInDB=0
		/*Si al final de ambas consultas userID no es null se retorna 1*/
       ELSE 
           SET @isInDB=1
    END
	/*Si no existe ninguna c�dula asociada a ese nombre de usuario se retorna 0*/
    ELSE
       SET @isInDB=0

END