-- =============================================
-- Stored Procedure: sp_ListarOfertasGrupoServicio (VERSIÓN MEJORADA)
-- Descripción: Lista todas las ofertas de grupos de servicio del periodo actual
--              Si un servicio no tiene grupos, muestra el servicio sin información de grupo
-- =============================================

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ListarOfertasGrupoServicio]') AND type in (N'P', N'PC'))
    DROP PROCEDURE [dbo].[sp_ListarOfertasGrupoServicio]
GO

CREATE PROCEDURE [dbo].[sp_ListarOfertasGrupoServicio]
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Obtener el periodo actual
    DECLARE @PeriodoActual INT;
    SELECT @PeriodoActual = MAX(Periodo) FROM GrupoAnual;
    
    -- Si no hay periodo, usar 1 por defecto
    IF @PeriodoActual IS NULL
        SET @PeriodoActual = 1;
    
    -- Primero, obtener todos los servicios adicionales que tienen grupos en el periodo actual
    -- Luego, para cada servicio, mostrar sus grupos disponibles
    SELECT 
        gs.Id_GrupoServicio,
        sa.Id_ServicioAdicional,
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
    FROM ServicioAdicional sa
    INNER JOIN GrupoServicio gs ON sa.Id_ServicioAdicional = gs.Id_ServicioAdicional
    LEFT JOIN Salon s ON gs.Id_Salon = s.Id_Salon
    LEFT JOIN Usuario u ON gs.Id_Profesor = u.Id
    LEFT JOIN ServicioAlumno servAlum ON gs.Id_GrupoServicio = servAlum.Id_GrupoServicio
        AND (servAlum.FechaFinal IS NULL OR servAlum.FechaFinal >= CAST(GETDATE() AS DATE))
    WHERE gs.Periodo = @PeriodoActual
    GROUP BY 
        gs.Id_GrupoServicio,
        sa.Id_ServicioAdicional,
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

-- NOTA: Este SP solo muestra servicios que tienen grupos de servicio creados.
-- Si un servicio adicional no tiene grupos, no aparecerá en los resultados.
-- Para mostrar todos los servicios (incluso sin grupos), se necesitaría una lógica diferente.

