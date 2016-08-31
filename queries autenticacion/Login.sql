/*Paso 3*/
CREATE PROCEDURE dbo.Login
	/*Parámetros: pLoginName donde se recibe el nombre de usuario,
	pPassword donde se recibe la contraseña,
	y se declara el parámetro de salida isInDB que devuelve 1 si el usuario
	si está en la BD o 0 si no está*/
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
		/*Si si existe una cédula con este nombreUsuario se pregunta si el Password de dicho usuario
		corresponde al recibido por parámetro junto con el salt de esa tupla*/
        SET @userID=(SELECT cedulaUsuario FROM [dbo].[Usuarios] WHERE nombreUsuario=@pLoginName AND PasswordHash=HASHBYTES('SHA2_512', @pPassword+CAST(Salt AS NVARCHAR(36))))

		/*si al final de ambas consultas userID es null se retorna 0*/
       IF(@userID IS NULL)
           SET @isInDB=0
		/*Si al final de ambas consultas userID no es null se retorna 1*/
       ELSE 
           SET @isInDB=1
    END
	/*Si no existe ninguna cédula asociada a ese nombre de usuario se retorna 0*/
    ELSE
       SET @isInDB=0

END