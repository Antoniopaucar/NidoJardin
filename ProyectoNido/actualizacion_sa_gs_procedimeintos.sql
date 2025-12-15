CREATE OR ALTER PROC dbo.GrupoServicio_Obtener
    @Id_GrupoServicio INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        gs.*,
        s.Nombre AS NombreSalon,
        sa.Nombre AS NombreServicio,
        sa.Tipo,
        sa.Costo
    FROM dbo.GrupoServicio gs
    INNER JOIN dbo.Salon s ON s.Id_Salon = gs.Id_Salon
    INNER JOIN dbo.ServicioAdicional sa ON sa.Id_ServicioAdicional = gs.Id_ServicioAdicional
    WHERE gs.Id_GrupoServicio = @Id_GrupoServicio;
END;
GO

CREATE OR ALTER PROC dbo.ServicioAlumno_ListarPorAlumno
    @Id_Alumno INT,
    @Periodo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        x.Id_ServicioAlumno,
        x.Id_Alumno,
        x.Id_GrupoServicio,
        gs.Periodo,
        sa.Nombre AS Servicio,
        s.Nombre AS Salon,
        x.FechaInicio, x.FechaFinal, x.FechaPago,
        x.HoraInicio, x.HoraFinal,
        x.Monto
    FROM dbo.ServicioAlumno x
    INNER JOIN dbo.GrupoServicio gs ON gs.Id_GrupoServicio = x.Id_GrupoServicio
    INNER JOIN dbo.ServicioAdicional sa ON sa.Id_ServicioAdicional = gs.Id_ServicioAdicional
    INNER JOIN dbo.Salon s ON s.Id_Salon = gs.Id_Salon
    WHERE x.Id_Alumno = @Id_Alumno
      AND (@Periodo IS NULL OR gs.Periodo = @Periodo)
    ORDER BY gs.Periodo DESC, sa.Nombre, x.FechaInicio DESC;
END;
GO


CREATE OR ALTER PROC dbo.GrupoServicio_Insertar
    @Id_Salon INT,
    @Id_Profesor INT,
    @Id_ServicioAdicional INT,
    @Periodo INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY
        -- Validaciones mínimas
        IF @Periodo IS NULL OR @Periodo < 2000
            THROW 50001, 'Periodo inválido.', 1;

        IF NOT EXISTS (SELECT 1 FROM dbo.Salon WHERE Id_Salon = @Id_Salon)
            THROW 50002, 'El salón no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM dbo.Profesor WHERE Id_Profesor = @Id_Profesor)
            THROW 50003, 'El profesor no existe.', 1;

        IF NOT EXISTS (SELECT 1 FROM dbo.ServicioAdicional WHERE Id_ServicioAdicional = @Id_ServicioAdicional)
            THROW 50004, 'El servicio adicional no existe.', 1;

        -- Evitar duplicado (clave lógica)
        IF EXISTS (
            SELECT 1
            FROM dbo.GrupoServicio
            WHERE Id_Salon = @Id_Salon
              AND Id_ServicioAdicional = @Id_ServicioAdicional
              AND Periodo = @Periodo
        )
            THROW 50005, 'Ya existe un GrupoServicio para ese salón/servicio/periodo.', 1;

        BEGIN TRAN;

        INSERT INTO dbo.GrupoServicio (Id_Salon, Id_Profesor, Id_ServicioAdicional, Periodo)
        VALUES (@Id_Salon, @Id_Profesor, @Id_ServicioAdicional, @Periodo);

        DECLARE @IdNuevo INT = SCOPE_IDENTITY();

        COMMIT;

        SELECT @IdNuevo AS Id_GrupoServicio;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK;
        THROW;
    END CATCH
END;
GO

CREATE OR ALTER PROC dbo.insertar_servicioAlumno
(
    @Id_GrupoServicio INT,
    @Id_Alumno        INT,
    @FechaInicio      DATE,
    @FechaFinal       DATE = NULL,
    @FechaPago        DATE = NULL,
    @HoraInicio       TIME(7) = NULL,
    @HoraFinal        TIME(7) = NULL,
    @Monto            DECIMAL(6,2)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        -- Validaciones de fechas/horas
        IF @FechaFinal IS NOT NULL AND @FechaFinal < @FechaInicio
        BEGIN
            SELECT 'FechaFinal no puede ser menor que FechaInicio.' AS Mensaje; RETURN;
        END

        IF @HoraInicio IS NOT NULL AND @HoraFinal IS NOT NULL AND @HoraFinal <= @HoraInicio
        BEGIN
            SELECT 'HoraFinal debe ser mayor que HoraInicio.' AS Mensaje; RETURN;
        END

        IF @Monto < 0
        BEGIN
            SELECT 'Monto no puede ser negativo.' AS Mensaje; RETURN;
        END

        -- Evitar duplicar alumno en el mismo grupo (regla simple)
        IF EXISTS (SELECT 1 FROM ServicioAlumno WHERE Id_GrupoServicio=@Id_GrupoServicio AND Id_Alumno=@Id_Alumno)
        BEGIN
            SELECT 'El alumno ya está registrado en este GrupoServicio.' AS Mensaje; RETURN;
        END

        -- Obtener AFORO del salón del grupo
        DECLARE @Aforo INT;

        SELECT @Aforo = s.Aforo
        FROM GrupoServicio gs
        INNER JOIN Salon s ON s.Id_Salon = gs.Id_Salon
        WHERE gs.Id_GrupoServicio = @Id_GrupoServicio;

        IF @Aforo IS NULL
        BEGIN
            SELECT 'No se encontró el GrupoServicio o su Salón.' AS Mensaje; RETURN;
        END

        -- Contar OCUPADOS activos (libera cupo si FechaFinal venció)
        DECLARE @Ocupados INT;

        SELECT @Ocupados = COUNT(*)
        FROM ServicioAlumno sa
        WHERE sa.Id_GrupoServicio = @Id_GrupoServicio
          AND (sa.FechaFinal IS NULL OR sa.FechaFinal >= CAST(GETDATE() AS DATE));

        IF @Ocupados >= @Aforo
        BEGIN
            SELECT CONCAT('Aforo completo. Capacidad: ', @Aforo, ' alumnos.') AS Mensaje; RETURN;
        END

        -- Insertar
        INSERT INTO ServicioAlumno
        (Id_GrupoServicio, Id_Alumno, FechaInicio, FechaFinal, FechaPago, HoraInicio, HoraFinal, Monto)
        VALUES
        (@Id_GrupoServicio, @Id_Alumno, @FechaInicio, @FechaFinal, @FechaPago, @HoraInicio, @HoraFinal, @Monto);

        SELECT 'OK' AS Mensaje;

    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

CREATE OR ALTER PROC dbo.modificar_servicioAlumno
(
    @Id_ServicioAlumno INT,
    @Id_GrupoServicio  INT,
    @Id_Alumno         INT,
    @FechaInicio       DATE,
    @FechaFinal        DATE = NULL,
    @FechaPago         DATE = NULL,
    @HoraInicio        TIME(7) = NULL,
    @HoraFinal         TIME(7) = NULL,
    @Monto             DECIMAL(6,2)
)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    BEGIN TRY

        IF NOT EXISTS (SELECT 1 FROM ServicioAlumno WHERE Id_ServicioAlumno=@Id_ServicioAlumno)
        BEGIN
            SELECT 'Registro no existe.' AS Mensaje; RETURN;
        END

        IF @FechaFinal IS NOT NULL AND @FechaFinal < @FechaInicio
        BEGIN
            SELECT 'FechaFinal no puede ser menor que FechaInicio.' AS Mensaje; RETURN;
        END

        IF @HoraInicio IS NOT NULL AND @HoraFinal IS NOT NULL AND @HoraFinal <= @HoraInicio
        BEGIN
            SELECT 'HoraFinal debe ser mayor que HoraInicio.' AS Mensaje; RETURN;
        END

        IF @Monto < 0
        BEGIN
            SELECT 'Monto no puede ser negativo.' AS Mensaje; RETURN;
        END

        -- Si cambiaste de grupo, validar aforo del nuevo grupo (sin contarte a ti mismo)
        DECLARE @GrupoActual INT;
        SELECT @GrupoActual = Id_GrupoServicio FROM ServicioAlumno WHERE Id_ServicioAlumno=@Id_ServicioAlumno;

        IF @GrupoActual <> @Id_GrupoServicio
        BEGIN
            DECLARE @Aforo INT;

            SELECT @Aforo = s.Aforo
            FROM GrupoServicio gs
            INNER JOIN Salon s ON s.Id_Salon = gs.Id_Salon
            WHERE gs.Id_GrupoServicio = @Id_GrupoServicio;

            DECLARE @Ocupados INT;

            SELECT @Ocupados = COUNT(*)
            FROM ServicioAlumno sa
            WHERE sa.Id_GrupoServicio = @Id_GrupoServicio
              AND (sa.FechaFinal IS NULL OR sa.FechaFinal >= CAST(GETDATE() AS DATE));

            IF @Ocupados >= @Aforo
            BEGIN
                SELECT CONCAT('Aforo completo en el nuevo grupo. Capacidad: ', @Aforo) AS Mensaje; RETURN;
            END
        END

        UPDATE ServicioAlumno
        SET
            Id_GrupoServicio = @Id_GrupoServicio,
            Id_Alumno        = @Id_Alumno,
            FechaInicio      = @FechaInicio,
            FechaFinal       = @FechaFinal,
            FechaPago        = @FechaPago,
            HoraInicio       = @HoraInicio,
            HoraFinal        = @HoraFinal,
            Monto            = @Monto
        WHERE Id_ServicioAlumno = @Id_ServicioAlumno;

        SELECT 'OK' AS Mensaje;

    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

CREATE OR ALTER PROC dbo.listar_servicioAlumno
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        sa.Id_ServicioAlumno,
        sa.Id_GrupoServicio,
        sa.Id_Alumno,

        a.Nombres + ' ' + a.ApPaterno + ' ' + a.ApMaterno AS NombreAlumno,

        gs.Id_Salon,
        s.Nombre AS NombreSalon,
        s.Aforo,

        gs.Id_Profesor,
        (u.Nombres + ' ' + u.ApPaterno + ' ' + u.ApMaterno) AS NombreProfesor,

        gs.Id_ServicioAdicional,
        sev.Nombre AS NombreServicio,

        gs.Periodo,

        sa.FechaInicio,
        sa.FechaFinal,
        sa.FechaPago,
        sa.HoraInicio,
        sa.HoraFinal,
        sa.Monto,

        CASE
            WHEN sa.FechaFinal IS NULL THEN 'ACTIVO'
            WHEN sa.FechaFinal >= CAST(GETDATE() AS DATE) THEN 'ACTIVO'
            ELSE 'FINALIZADO'
        END AS Estado
    FROM ServicioAlumno sa
    INNER JOIN Alumno a ON a.Id_Alumno = sa.Id_Alumno
    INNER JOIN GrupoServicio gs ON gs.Id_GrupoServicio = sa.Id_GrupoServicio
    INNER JOIN Salon s ON s.Id_Salon = gs.Id_Salon
    INNER JOIN Profesor p ON p.Id_Profesor = gs.Id_Profesor
    INNER JOIN Usuario u ON u.Id = p.Id_Profesor
    INNER JOIN ServicioAdicional sev ON sev.Id_ServicioAdicional = gs.Id_ServicioAdicional
    ORDER BY sa.Id_ServicioAlumno DESC;
END;
GO

CREATE OR ALTER PROC dbo.eliminar_servicioAlumno
(
    @Id_ServicioAlumno INT
)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        DELETE FROM ServicioAlumno WHERE Id_ServicioAlumno=@Id_ServicioAlumno;
        SELECT 'OK' AS Mensaje;
    END TRY
    BEGIN CATCH
        SELECT ERROR_MESSAGE() AS Mensaje;
    END CATCH
END;
GO

CREATE OR ALTER PROC dbo.buscarAlumno
(
    @Texto VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @t VARCHAR(100) = LTRIM(RTRIM(ISNULL(@Texto,'')));

    SELECT
        A.Id_Alumno,
        (A.Nombres + ' ' + A.ApPaterno + ' ' + A.ApMaterno) AS NombreCompleto,
        A.Documento
    FROM dbo.Alumno A
    WHERE
        @t = '' OR
        A.Nombres   LIKE '%' + @t + '%' OR
        A.ApPaterno LIKE '%' + @t + '%' OR
        A.ApMaterno LIKE '%' + @t + '%' OR
        A.Documento LIKE '%' + @t + '%'
    ORDER BY NombreCompleto;
END;
GO

CREATE OR ALTER PROC dbo.buscarGrupoServicio
(
    @Texto VARCHAR(100)
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        gs.Id_GrupoServicio,
        gs.Id_Salon,
        s.Nombre AS NombreSalon,

        gs.Id_Profesor,
        (u.Nombres + ' ' + u.ApPaterno + ' ' + u.ApMaterno) AS NombreProfesor,

        gs.Id_ServicioAdicional,
        sa.Nombre AS NombreServicio,

        gs.Periodo
    FROM GrupoServicio gs
    INNER JOIN Salon s ON s.Id_Salon = gs.Id_Salon
    INNER JOIN Profesor p ON p.Id_Profesor = gs.Id_Profesor
    INNER JOIN Usuario u ON u.Id = p.Id_Profesor
    INNER JOIN ServicioAdicional sa ON sa.Id_ServicioAdicional = gs.Id_ServicioAdicional
    WHERE
        s.Nombre LIKE '%' + @Texto + '%'
        OR sa.Nombre LIKE '%' + @Texto + '%'
        OR (u.Nombres + ' ' + u.ApPaterno + ' ' + u.ApMaterno) LIKE '%' + @Texto + '%'
        OR CAST(gs.Periodo AS VARCHAR(10)) LIKE '%' + @Texto + '%'
    ORDER BY gs.Id_GrupoServicio DESC;
END;
GO
