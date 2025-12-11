-- =============================================
-- Stored Procedure: sp_ListarAlumnosPorGrupoServicio
-- Descripción: Lista los alumnos inscritos en un grupo de servicio específico
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ListarAlumnosPorGrupoServicio]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_ListarAlumnosPorGrupoServicio]
GO

CREATE PROCEDURE [dbo].[sp_ListarAlumnosPorGrupoServicio]
    @Id_GrupoServicio INT
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
    INNER JOIN ServicioAlumno sa ON a.Id_Alumno = sa.Id_Alumno
    WHERE sa.Id_GrupoServicio = @Id_GrupoServicio
        AND (sa.FechaFinal IS NULL OR sa.FechaFinal >= CAST(GETDATE() AS DATE))
    ORDER BY a.ApPaterno, a.ApMaterno, a.Nombres
END
GO

