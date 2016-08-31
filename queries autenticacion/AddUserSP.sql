CREATE PROCEDURE dbo.agregarUsuario
	/*Par�metros: pLogin donde se recibe el nombre de usuario,
	pPassword donde se recibe la contrase�a,
	cedula donde se recibe la c�dula del estudiante asociado a dicho usuario
	y se declara el par�metro de salida estado que devuelve 1 si el usuario
	se pudo guardar en la base de datos y cualquier otro n�mero que corresponde 
	al ERROR_MESSAGE() si no se pudo guardar*/
    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
	@cedula varchar(9),
    @estado bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	/*Se genera un salt, el cual corresponde a una llave de encriptaci�n del password*/
    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

		/*Se inserta en la tabla Usuarios los datos de un nuevo usuario, se encripta la contrase�a
		con un HASHBYTES con el algoritmo SHA2_512 con la uni�n del password digitado y el salt (notese que
		este salt es �nico para cada usuario sin importar que tengan la misma contrase�a, este se almacena
		diferente para cada uno)*/
        INSERT INTO dbo.[Usuarios] (cedulaUsuario, nombreUsuario, PasswordHash, Salt)
        VALUES(@cedula, @pLogin, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt)

		/*si la inserci�n se pudo realizar se devuelve un 1*/
       SET @estado=1

    END TRY
    BEGIN CATCH
		/*En cualquier otro caso se devuelve el mensaje de error*/
        SET @estado=ERROR_MESSAGE() 
    END CATCH

END