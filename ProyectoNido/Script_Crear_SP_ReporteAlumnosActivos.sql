-- SP para listar alumnos activos
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ListarAlumnosActivos')
DROP PROCEDURE sp_ListarAlumnosActivos
GO

CREATE PROCEDURE sp_ListarAlumnosActivos
AS
BEGIN
    SELECT 
        a.Id_Alumno,
        a.Id_Apoderado,
        -- Obtenemos el nombre completo desde la tabla Usuario unida al Apoderado
        (u_apo.Nombres + ' ' + u_apo.ApPaterno + ' ' + u_apo.ApMaterno) as NombreCompleto,
        a.Id_TipoDocumento,
        td.Nombre as NombreTipoDocumento,
        a.Nombres,
        a.ApPaterno,
        a.ApMaterno,
        a.Documento,
        a.FechaNacimiento,
        a.Sexo,
        a.Activo
    FROM Alumno a
    INNER JOIN Apoderado apo ON a.Id_Apoderado = apo.Id_Apoderado
    INNER JOIN Usuario u_apo ON apo.Id_Apoderado = u_apo.Id -- Join con Usuario para datos personales del apoderado
    INNER JOIN TipoDocumento td ON a.Id_TipoDocumento = td.Id_TipoDocumento
    WHERE a.Activo = 1
END
GO
