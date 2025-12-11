-- =============================================
-- Stored Procedure: sp_ListarOfertasGrupoServicio
-- Descripción: Lista todas las ofertas de grupos de servicio del periodo actual
--              con información del docente para que los apoderados puedan verlas
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ListarOfertasGrupoServicio]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_ListarOfertasGrupoServicio]
GO

CREATE PROCEDURE [dbo].[sp_ListarOfertasGrupoServicio]
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Obtener el periodo actual (puedes ajustar esta lógica según tu sistema)
    DECLARE @PeriodoActual INT;
    SELECT @PeriodoActual = MAX(Periodo) FROM GrupoAnual; -- O usar otra tabla de configuración
    
    -- Si no hay periodo, usar 1 por defecto
    IF @PeriodoActual IS NULL
        SET @PeriodoActual = 1;
    
    -- Listar todos los grupos de servicio del periodo actual con información completa
    -- Si un servicio no tiene grupos, no se mostrará (solo se muestran servicios con grupos disponibles)
    SELECT 
        gs.Id_GrupoServicio,
        sa.Nombre AS Servicio,
        ISNULL(sa.Descripcion, '') AS Descripcion,
        sa.Tipo,
        sa.Costo,
        ISNULL(s.Nombre, 'Sin asignar') AS Salon,
        ISNULL(s.Aforo, 0) AS Aforo,
        gs.Periodo,
        ISNULL(COUNT(DISTINCT servAlum.Id_Alumno), 0) AS TotalAlumnos,
        ISNULL(u.Nombres, '') AS NombreDocente,
        ISNULL(u.ApPaterno, '') AS ApellidoPaternoDocente,
        ISNULL(u.ApMaterno, '') AS ApellidoMaternoDocente
    FROM GrupoServicio gs
    INNER JOIN ServicioAdicional sa ON gs.Id_ServicioAdicional = sa.Id_ServicioAdicional
    LEFT JOIN Salon s ON gs.Id_Salon = s.Id_Salon
    LEFT JOIN Usuario u ON gs.Id_Profesor = u.Id
    LEFT JOIN ServicioAlumno servAlum ON gs.Id_GrupoServicio = servAlum.Id_GrupoServicio
        AND (servAlum.FechaFinal IS NULL OR servAlum.FechaFinal >= CAST(GETDATE() AS DATE))
    WHERE gs.Periodo = @PeriodoActual
    GROUP BY 
        gs.Id_GrupoServicio,
        sa.Nombre,
        sa.Descripcion,
        sa.Tipo,
        sa.Costo,
        s.Nombre,
        s.Aforo,
        gs.Periodo,
        u.Nombres,
        u.ApPaterno,
        u.ApMaterno
    ORDER BY sa.Nombre, gs.Id_GrupoServicio
END
GO

