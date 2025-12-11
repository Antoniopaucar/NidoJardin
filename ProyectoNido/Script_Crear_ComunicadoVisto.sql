-- Script para crear la tabla ComunicadoVisto y el stored procedure
-- Ejecutar en la base de datos BDNido

USE [BDNido]
GO

-- Crear la tabla ComunicadoVisto si no existe
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ComunicadoVisto]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[ComunicadoVisto](
        [Id_Comunicado] [int] NOT NULL,
        [Id_Usuario] [int] NOT NULL,
        [FechaVisto] [datetime] NOT NULL DEFAULT (getdate()),
        CONSTRAINT [PK_ComunicadoVisto] PRIMARY KEY CLUSTERED 
        (
            [Id_Comunicado] ASC,
            [Id_Usuario] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
    ) ON [PRIMARY]

    -- Agregar Foreign Keys
    ALTER TABLE [dbo].[ComunicadoVisto] 
    ADD CONSTRAINT [FK_ComunicadoVisto_Comunicado] 
    FOREIGN KEY([Id_Comunicado]) REFERENCES [dbo].[Comunicado] ([Id_Comunicado])

    ALTER TABLE [dbo].[ComunicadoVisto] 
    ADD CONSTRAINT [FK_ComunicadoVisto_Usuario] 
    FOREIGN KEY([Id_Usuario]) REFERENCES [dbo].[Usuario] ([Id])

    PRINT 'Tabla ComunicadoVisto creada exitosamente'
END
ELSE
BEGIN
    PRINT 'La tabla ComunicadoVisto ya existe'
END
GO

-- Crear el stored procedure sp_MarcarComunicadoVisto si no existe
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_MarcarComunicadoVisto]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_MarcarComunicadoVisto]
GO

CREATE PROCEDURE [dbo].[sp_MarcarComunicadoVisto]
    @Id_Comunicado INT,
    @Id_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Solo insertar si no existe ya (evitar duplicados)
    IF NOT EXISTS (
        SELECT 1 FROM ComunicadoVisto 
        WHERE Id_Comunicado = @Id_Comunicado AND Id_Usuario = @Id_Usuario
    )
    BEGIN
        INSERT INTO ComunicadoVisto (Id_Comunicado, Id_Usuario, FechaVisto)
        VALUES (@Id_Comunicado, @Id_Usuario, GETDATE())
    END
END
GO

-- Crear el stored procedure listar_comunicados_por_rol_usuario si no existe
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[listar_comunicados_por_rol_usuario]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[listar_comunicados_por_rol_usuario]
GO

CREATE PROCEDURE [dbo].[listar_comunicados_por_rol_usuario]
    @Id_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;
    
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
    INNER JOIN UsuarioRol ur ON c.Id_Rol = ur.Id_Rol
    LEFT JOIN ComunicadoVisto cv ON c.Id_Comunicado = cv.Id_Comunicado 
        AND cv.Id_Usuario = @Id_Usuario
    WHERE ur.Id_Usuario = @Id_Usuario
        AND (c.FechaFinal IS NULL OR c.FechaFinal >= CAST(GETDATE() AS DATE))
    ORDER BY c.FechaCreacion DESC
END
GO

PRINT 'Stored procedures creados exitosamente'
GO

