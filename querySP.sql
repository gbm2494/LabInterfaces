-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE eliminarEstudiante @nombre varchar(20)

AS
select distinct Estudiante.Cedula
into #temp
from Estudiante join Usuarios on Estudiante.Cedula = Usuarios.cedulaUsuario
WHERE Estudiante.nombre = @nombre 

delete Usuarios
where cedulaUsuario in (select * from #temp)

delete Estudiante
where Cedula in (select * from #temp)

drop table #temp
GO

