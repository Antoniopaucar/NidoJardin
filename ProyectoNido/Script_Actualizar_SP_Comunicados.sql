-- Script para actualizar el stored procedure listar_comunicados_por_rol_usuario
-- Este SP filtra los comunicados según los roles que tiene asignado el usuario
-- Ejecutar en la base de datos BDNido

USE [BDNido]
GO

-- Verificar y actualizar el stored procedure
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[listar_comunicados_por_rol_usuario]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[listar_comunicados_por_rol_usuario]
GO

CREATE PROCEDURE [dbo].[listar_comunicados_por_rol_usuario]
    @Id_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Este SP lista los comunicados dirigidos a los roles que tiene asignado el usuario
    -- La lógica es:
    -- 1. Obtiene todos los comunicados (c)
    -- 2. Filtra solo aquellos donde el rol del comunicado (c.Id_Rol) 
    --    coincide con algún rol que tiene el usuario (ur.Id_Rol)
    -- 3. Verifica que el usuario tenga ese rol asignado (ur.Id_Usuario = @Id_Usuario)
    -- 4. Solo muestra comunicados vigentes (sin fecha final o fecha final >= hoy)
    
    SELECT DISTINCT
        c.Id_Comunicado,
        c.Id_Usuario,
        u.NombreUsuario,
        u.Nombres,
        u.ApPaterno,
        c.Id_Rol,
        r.NombreRol,
        c.Nombre,
        c.Descripcion,
        c.FechaCreacion,
        c.FechaFinal,
        CASE 
            WHEN cv.Id_Usuario IS NOT NULL THEN CAST(1 AS BIT)
            ELSE CAST(0 AS BIT)
        END AS Visto
    FROM Comunicado c
    INNER JOIN Usuario u ON c.Id_Usuario = u.Id
    INNER JOIN Rol r ON c.Id_Rol = r.Id_Rol
    -- CRÍTICO: Solo muestra comunicados donde el usuario tiene el rol asignado
    INNER JOIN UsuarioRol ur ON c.Id_Rol = ur.Id_Rol 
        AND ur.Id_Usuario = @Id_Usuario
    LEFT JOIN ComunicadoVisto cv ON c.Id_Comunicado = cv.Id_Comunicado 
        AND cv.Id_Usuario = @Id_Usuario
    WHERE (c.FechaFinal IS NULL OR c.FechaFinal >= CAST(GETDATE() AS DATE))
    ORDER BY c.FechaCreacion DESC
END
GO

PRINT 'Stored procedure listar_comunicados_por_rol_usuario actualizado correctamente'
GO

-- Script de prueba para verificar que funciona
-- Descomentar para probar (reemplazar @Id_Usuario con un ID real)
/*
DECLARE @Id_Usuario INT = 1; -- Cambiar por un ID de usuario real

-- Ver roles del usuario
SELECT 
    u.Id,
    u.NombreUsuario,
    r.Id_Rol,
    r.NombreRol
FROM Usuario u
INNER JOIN UsuarioRol ur ON u.Id = ur.Id_Usuario
INNER JOIN Rol r ON ur.Id_Rol = r.Id_Rol
WHERE u.Id = @Id_Usuario;

-- Ver comunicados que debería ver
EXEC listar_comunicados_por_rol_usuario @Id_Usuario;
*/

