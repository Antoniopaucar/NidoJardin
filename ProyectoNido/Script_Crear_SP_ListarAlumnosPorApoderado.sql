-- =============================================
-- Stored Procedure: sp_ListarAlumnosPorApoderado
-- Descripción: Lista los alumnos (hijos) de un apoderado específico
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ListarAlumnosPorApoderado]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_ListarAlumnosPorApoderado]
GO

CREATE PROCEDURE [dbo].[sp_ListarAlumnosPorApoderado]
    @Id_Apoderado INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        a.Id_Alumno AS Id,
        a.Nombres,
        a.ApPaterno AS ApellidoPaterno,
        a.ApMaterno AS ApellidoMaterno,
        a.Documento,
        a.FechaNacimiento,
        a.Sexo,
        a.Activo
    FROM Alumno a
    WHERE a.Id_Apoderado = @Id_Apoderado
        AND a.Activo = 1
    ORDER BY a.ApPaterno, a.ApMaterno, a.Nombres
END
GO

