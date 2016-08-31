CREATE PROCEDURE dbo.agregarUsuario
	/*Parámetros: pLogin donde se recibe el nombre de usuario,
	pPassword donde se recibe la contraseña,
	cedula donde se recibe la cédula del estudiante asociado a dicho usuario
	y se declara el parámetro de salida estado que devuelve 1 si el usuario
	se pudo guardar en la base de datos y cualquier otro número que corresponde 
	al ERROR_MESSAGE() si no se pudo guardar*/
    @pLogin NVARCHAR(50), 
    @pPassword NVARCHAR(50),
	@cedula varchar(9),
    @estado bit OUTPUT
AS
BEGIN
    SET NOCOUNT ON

	/*Se genera un salt, el cual corresponde a una llave de encriptación del password*/
    DECLARE @salt UNIQUEIDENTIFIER=NEWID()
    BEGIN TRY

		/*Se inserta en la tabla Usuarios los datos de un nuevo usuario, se encripta la contraseña
		con un HASHBYTES con el algoritmo SHA2_512 con la unión del password digitado y el salt (notese que
		este salt es único para cada usuario sin importar que tengan la misma contraseña, este se almacena
		diferente para cada uno)*/
        INSERT INTO dbo.[Usuarios] (cedulaUsuario, nombreUsuario, PasswordHash, Salt)
        VALUES(@cedula, @pLogin, HASHBYTES('SHA2_512', @pPassword+CAST(@salt AS NVARCHAR(36))), @salt)

		/*si la inserción se pudo realizar se devuelve un 1*/
       SET @estado=1

    END TRY
    BEGIN CATCH
		/*En cualquier otro caso se devuelve el mensaje de error*/
        SET @estado=ERROR_MESSAGE() 
    END CATCH

END