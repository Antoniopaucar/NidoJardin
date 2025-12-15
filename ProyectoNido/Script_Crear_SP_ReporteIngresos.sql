USE [BDNido]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sp_ReporteIngresos_Listar]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[sp_ReporteIngresos_Listar]
GO

CREATE PROCEDURE [dbo].[sp_ReporteIngresos_Listar]
    @IdSalon INT = NULL,
    @IdDistrito INT = NULL,
    @FechaInicio DATE = NULL,
    @FechaFin DATE = NULL
AS
BEGIN
    SET NOCOUNT ON;

    -- 1. Obtener pagos de MATRICULAS (Pensiones)
    SELECT 
        md.FechaPago,
        s.Nombre AS Salon,
        d.Nombre AS Distrito,
        (a.Nombres + ' ' + a.ApPaterno + ' ' + a.ApMaterno) AS Alumno,
        md.NombreCuota AS Concepto,
        md.TotalLinea AS Monto,
        md.EstadoPago,   -- 'C', 'P', 'X'
        CASE 
            WHEN md.EstadoPago = 'C' THEN 'Cancelado'
            WHEN md.EstadoPago = 'P' THEN 'Pendiente'
            WHEN md.EstadoPago = 'X' THEN 'Exonerado'
            ELSE 'Otro'
        END AS EstadoDescripcion,
        'Pension' AS TipoOrigen
    FROM MatriculaDetalle md
    INNER JOIN Matricula m ON md.Id_Matricula = m.Id_Matricula
    INNER JOIN Alumno a ON m.Id_Alumno = a.Id_Alumno
    INNER JOIN Usuario u_apo ON a.Id_Apoderado = u_apo.Id -- El apoderado es un Usuario
    LEFT JOIN Distrito d ON u_apo.Id_Distrito = d.Id_Distrito
    INNER JOIN GrupoAnual ga ON m.Id_GrupoAnual = ga.Id_GrupoAnual
    INNER JOIN Salon s ON ga.Id_Salon = s.Id_Salon
    WHERE md.Activo = 1
      AND md.EstadoPago = 'C' -- Solo Cancelados
      AND (@IdSalon IS NULL OR s.Id_Salon = @IdSalon)
      AND (@IdDistrito IS NULL OR d.Id_Distrito = @IdDistrito)
      AND (@FechaInicio IS NULL OR md.FechaPago >= @FechaInicio) 
      AND (@FechaFin IS NULL OR md.FechaPago <= @FechaFin)

    UNION ALL

    -- 2. Obtener pagos de SERVICIOS ADICIONALES (Talleres, etc.)
    SELECT 
        sa.FechaPago,
        s.Nombre AS Salon,
        d.Nombre AS Distrito,
        (a.Nombres + ' ' + a.ApPaterno + ' ' + a.ApMaterno) AS Alumno,
        serv.Nombre AS Concepto,
        sa.Monto,
        'C' AS EstadoPago, -- Si tiene FechaPago es Cancelado
        'Cancelado' AS EstadoDescripcion,
        'Servicio' AS TipoOrigen
    FROM ServicioAlumno sa
    INNER JOIN Alumno a ON sa.Id_Alumno = a.Id_Alumno
    INNER JOIN Usuario u_apo ON a.Id_Apoderado = u_apo.Id
    LEFT JOIN Distrito d ON u_apo.Id_Distrito = d.Id_Distrito
    INNER JOIN GrupoServicio gs ON sa.Id_GrupoServicio = gs.Id_GrupoServicio
    INNER JOIN Salon s ON gs.Id_Salon = s.Id_Salon
    INNER JOIN ServicioAdicional serv ON gs.Id_ServicioAdicional = serv.Id_ServicioAdicional
    WHERE 
          sa.FechaPago IS NOT NULL -- Solo Pagados
      AND (@IdSalon IS NULL OR s.Id_Salon = @IdSalon)
      AND (@IdDistrito IS NULL OR d.Id_Distrito = @IdDistrito)
      AND (@FechaInicio IS NULL OR sa.FechaPago >= @FechaInicio)
      AND (@FechaFin IS NULL OR sa.FechaPago <= @FechaFin)

    ORDER BY FechaPago DESC
END
GO
