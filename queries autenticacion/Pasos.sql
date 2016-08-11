/*Paso 1*/

use gaudyblanco;

CREATE TABLE dbo.[User]
(
    UserID INT IDENTITY(1,1) NOT NULL,
    LoginName NVARCHAR(40) NOT NULL,
    PasswordHash BINARY(64) NOT NULL,
    salt UNIQUEIDENTIFIER,
    CONSTRAINT [PK_User_UserID] PRIMARY KEY CLUSTERED (UserID ASC)
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
TRUNCATE TABLE [dbo].[User]

DECLARE @responseMessage NVARCHAR(250)

EXEC dbo.uspAddUser
          @pLogin = N'Admin',
          @pPassword = N'123',
          @responseMessage=@responseMessage OUTPUT

SELECT UserID, LoginName, PasswordHash, Salt
FROM [dbo].[User]

/*Pruebas*/

DECLARE	@responseMessage bit

--Correct login and password
EXEC	dbo.Login
		@pLoginName = N'Admin',
		@pPassword = N'123',
		@responseMessage = @responseMessage OUTPUT

SELECT	@responseMessage as N'@responseMessage'

--Incorrect login
EXEC	dbo.Login
		@pLoginName = N'Admin1', 
		@pPassword = N'123',
		@responseMessage = @responseMessage OUTPUT

SELECT	@responseMessage as N'@responseMessage'

--Incorrect password
EXEC	dbo.Login
		@pLoginName = N'Admin', 
		@pPassword = N'1234',
		@responseMessage = @responseMessage OUTPUT

SELECT	@responseMessage as N'@responseMessage'