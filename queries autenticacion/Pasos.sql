/*Paso 1*/

use gaudyblanco;

CREATE TABLE dbo.[Usuarios]
(
    cedulaUsuario varchar(9) NOT NULL,
    nombreUsuario NVARCHAR(40) NOT NULL PRIMARY KEY,
    PasswordHash BINARY(64) NOT NULL,
    salt UNIQUEIDENTIFIER,
	FOREIGN KEY (cedulaUsuario) REFERENCES Estudiante (cedula)
)

/*Paso 2*/
/*
Ejecutar el procedimiento almacenado para insertar usuarios
*/

/*Paso 3*/
/*
Ejecutar el procedimiento almacenado para login
*/

/*Pruebas para comprobar que sirve*/
TRUNCATE TABLE [dbo].[Usuarios]

DECLARE @responseMessage NVARCHAR(250)

EXEC dbo.agregarUsuario
		  @cedula = '115650402',
          @pLogin = N'Gaudy',
          @pPassword = N'hola',
          @estado=@responseMessage OUTPUT

SELECT cedulaUsuario, nombreUsuario, PasswordHash, Salt
FROM [dbo].[Usuarios]

/*Pruebas*/

DECLARE	@isInDB bit

--Correct login and password
EXEC	dbo.Login
		@pLoginName = N'gaudy',
		@pPassword = N'hola',
		@isInDB = @isInDB OUTPUT

SELECT	@isInDB as N'@isInDB'

--Incorrect login
EXEC	dbo.Login
		@pLoginName = N'Admin1', 
		@pPassword = N'123',
		@isInDB = @isInDB OUTPUT

SELECT	@isInDB as N'@isInDB'

--Incorrect password
EXEC	dbo.Login
		@pLoginName = N'Admin', 
		@pPassword = N'1234',
		@isInDB = @isInDB OUTPUT

SELECT	@isInDB as N'@isInDB'