-- SP para listar docentes activos
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'sp_ListarProfesoresActivos')
DROP PROCEDURE sp_ListarProfesoresActivos
GO

CREATE PROCEDURE sp_ListarProfesoresActivos
AS
BEGIN
    SELECT 
        p.Id_Profesor,
        u.NombreUsuario,
        u.Nombres,
        u.ApPaterno,
        u.ApMaterno,
        u.Documento,
        p.FechaIngreso,

        u.Id_TipoDocumento,
        td.Nombre as NombreTipoDocumento,
        u.FechaNacimiento,
        u.Sexo,
        u.Id_Distrito,
        d.Nombre as NombreDistrito,
        u.Direccion,
        u.Telefono,
        u.Email,
        u.Activo,
        u.Intentos,
        u.Bloqueado,
        u.FechaBloqueo,
        u.UltimoIntento,
        u.UltimoLoginExitoso,
        u.FechaCreacion
    FROM Profesor p
    INNER JOIN Usuario u ON p.Id_Profesor = u.Id -- Corregido: Id_Usuario -> Id
    LEFT JOIN TipoDocumento td ON u.Id_TipoDocumento = td.Id_TipoDocumento
    LEFT JOIN Distrito d ON u.Id_Distrito = d.Id_Distrito
    WHERE u.Activo = 1
END
GO
