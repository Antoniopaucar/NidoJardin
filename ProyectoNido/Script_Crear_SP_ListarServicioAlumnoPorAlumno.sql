-- =============================================
-- Stored Procedure: listar_ServicioAlumnoPorAlumno
-- Descripción: Lista los servicios adicionales asignados a un alumno específico
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[listar_ServicioAlumnoPorAlumno]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[listar_ServicioAlumnoPorAlumno]
GO

CREATE PROCEDURE [dbo].[listar_ServicioAlumnoPorAlumno]
    @Id_Alumno INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        sa.Id_ServicioAlumno,
        sa.Id_GrupoServicio,
        sa.Id_Alumno,
        sa.FechaInicio,
        sa.FechaFinal,
        sa.FechaPago,
        sa.HoraInicio,
        sa.HoraFinal,
        sa.Monto,
        -- Campos adicionales del JOIN
        (a.Nombres + ' ' + a.ApPaterno + ' ' + a.ApMaterno) AS NombreAlumno,
        servAdicional.Nombre AS NombreGrupo
    FROM ServicioAlumno sa
    INNER JOIN Alumno a ON sa.Id_Alumno = a.Id_Alumno
    INNER JOIN GrupoServicio gs ON sa.Id_GrupoServicio = gs.Id_GrupoServicio
    INNER JOIN ServicioAdicional servAdicional ON gs.Id_ServicioAdicional = servAdicional.Id_ServicioAdicional
    WHERE sa.Id_Alumno = @Id_Alumno
    ORDER BY sa.FechaInicio DESC, servAdicional.Nombre
END
GO

