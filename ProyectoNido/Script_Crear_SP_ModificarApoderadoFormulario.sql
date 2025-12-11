-- =============================================
-- Stored Procedure: modificar_apoderado_formulario
-- Descripción: Modifica el Usuario y el Apoderado en una sola transacción
--              Solo actualiza el archivo del apoderado si se proporciona uno nuevo
--              Maneja correctamente las relaciones sin violar claves foráneas
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[modificar_apoderado_formulario]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[modificar_apoderado_formulario]
GO

CREATE PROCEDURE [dbo].[modificar_apoderado_formulario]
    @Id_Apoderado INT,
    @Id_TipoDocumento INT,
    @NombreUsuario NVARCHAR(50),
    @Nombres NVARCHAR(100),
    @ApPaterno NVARCHAR(100),
    @ApMaterno NVARCHAR(100),
    @Documento NVARCHAR(20),
    @FechaNacimiento DATETIME = NULL,
    @Sexo NVARCHAR(1) = NULL,
    @Id_Distrito INT = NULL,
    @Direccion NVARCHAR(255) = NULL,
    @Telefono NVARCHAR(20) = NULL,
    @Email NVARCHAR(100) = NULL,
    @Activo BIT,
    @TamanioBytes INT = NULL,
    @NombreArchivo NVARCHAR(255) = NULL,
    @CopiaDni VARBINARY(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    BEGIN TRANSACTION;
    
    BEGIN TRY
        -- Verificar que el apoderado existe
        IF NOT EXISTS (SELECT 1 FROM Apoderado WHERE Id_Apoderado = @Id_Apoderado)
        BEGIN
            RAISERROR('El apoderado especificado no existe.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- Verificar que el usuario existe
        IF NOT EXISTS (SELECT 1 FROM Usuario WHERE Id = @Id_Apoderado)
        BEGIN
            RAISERROR('El usuario especificado no existe.', 16, 1);
            ROLLBACK TRANSACTION;
            RETURN;
        END
        
        -- Modificar la tabla Usuario (sin modificar la clave)
        UPDATE Usuario
        SET 
            Id_TipoDocumento = @Id_TipoDocumento,
            NombreUsuario = @NombreUsuario,
            -- Clave: NO se actualiza (se mantiene la existente)
            Nombres = @Nombres,
            ApPaterno = @ApPaterno,
            ApMaterno = @ApMaterno,
            Documento = @Documento,
            FechaNacimiento = @FechaNacimiento,
            Sexo = @Sexo,
            Id_Distrito = @Id_Distrito,
            Direccion = @Direccion,
            Telefono = @Telefono,
            Email = @Email,
            Activo = @Activo
        WHERE Id = @Id_Apoderado;
        
        -- Modificar la tabla Apoderado (solo archivo si se proporciona uno nuevo)
        IF (@TamanioBytes IS NOT NULL AND @TamanioBytes > 0) OR (@CopiaDni IS NOT NULL)
        BEGIN
            UPDATE Apoderado
            SET 
                TamanioBytes = @TamanioBytes,
                NombreArchivo = @NombreArchivo,
                CopiaDni = @CopiaDni
            WHERE Id_Apoderado = @Id_Apoderado;
        END
        -- Si no se proporciona archivo, no se actualiza (mantiene el existente)
        
        COMMIT TRANSACTION;
        
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;
            
        DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
        DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
        DECLARE @ErrorState INT = ERROR_STATE();
        
        RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
    END CATCH
END
GO

