-- =============================================
-- Stored Procedure: sp_ListarAlumnosPorGrupoAnual
-- Descripción: Lista los alumnos matriculados en un grupo anual específico
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ListarAlumnosPorGrupoAnual]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_ListarAlumnosPorGrupoAnual]
GO

CREATE PROCEDURE [dbo].[sp_ListarAlumnosPorGrupoAnual]
    @Id_GrupoAnual INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT DISTINCT
        a.Id_Alumno,
        a.Nombres,
        a.ApPaterno,
        a.ApMaterno,
        a.Documento,
        a.FechaNacimiento,
        a.Sexo
    FROM Alumno a
    INNER JOIN Matricula m ON a.Id_Alumno = m.Id_Alumno
    WHERE m.Id_GrupoAnual = @Id_GrupoAnual
        AND m.Estado = 'A'  -- Solo matrículas activas
    ORDER BY a.ApPaterno, a.ApMaterno, a.Nombres
END
GO

