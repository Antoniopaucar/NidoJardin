select * from Apoderado
select *  from Usuario
select * from Profesor
select  * from Alumno
select * from Comunicado



CREATE OR ALTER PROC dbo.mov_login_apoderado
    @UsuarioODocumento VARCHAR(100),
    @Clave             VARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        U.Id AS Id_Usuario,
        U.NombreUsuario,
        U.Documento,
        (U.Nombres + ' ' + U.ApPaterno + ' ' + U.ApMaterno) AS NombreCompleto,
        U.Activo,
        U.Bloqueado,
        UR.Id_Rol
    FROM dbo.Usuario U
    INNER JOIN dbo.UsuarioRol UR
        ON UR.Id_Usuario = U.Id
       AND UR.Id_Rol = 3  -- Apoderado
    WHERE
        (U.NombreUsuario = @UsuarioODocumento OR U.Documento = @UsuarioODocumento)
        AND U.Clave = @Clave
        AND U.Activo = 1
        AND (U.Bloqueado = 0 OR U.Bloqueado IS NULL);
END
GO

CREATE OR ALTER PROC dbo.mov_listar_hijos_por_apoderado
    @Id_Apoderado INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        A.Id_Alumno AS Id,
        (A.Nombres + ' ' + A.ApPaterno + ' ' + A.ApMaterno) AS NombreCompleto
    FROM dbo.Alumno A
    WHERE A.Id_Apoderado = @Id_Apoderado
      AND A.Activo = 1
    ORDER BY A.Nombres, A.ApPaterno, A.ApMaterno;
END
GO

CREATE OR ALTER PROC dbo.mov_listar_cuotas_por_matricula
    @Id_Matricula INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        MD.Id_MatriculaDetalle,
        ISNULL(MD.NroCuota, 0)           AS NroCuota,
        MD.NombreCuota,
        MD.FechaVencimiento,
        MD.TotalLinea,
        MD.FechaPago,
        MD.EstadoPago,
        CASE
            WHEN MD.Activo = 0 THEN 'ANULADO'
            WHEN MD.EstadoPago = 'C' THEN 'PAGADO'
            WHEN MD.EstadoPago = 'P' THEN 'PENDIENTE'
            ELSE 'OTRO'
        END AS EstadoTexto
    FROM MatriculaDetalle MD
    WHERE MD.Id_Matricula = @Id_Matricula
    ORDER BY ISNULL(MD.NroCuota, 0);
END
GO

CREATE OR ALTER PROC dbo.mov_listar_comunicados_apoderado
    @Id_Usuario INT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Hoy DATE = CAST(GETDATE() AS DATE);

    SELECT
        C.Id_Comunicado,
        C.Nombre          AS Titulo,
        C.Descripcion,
        C.FechaCreacion,
        C.FechaFinal,

        -- Estado visto / nuevo
        CASE 
            WHEN CV.Id_Comunicado IS NULL THEN 0 
            ELSE 1 
        END AS Visto,

        CASE 
            WHEN CV.Id_Comunicado IS NULL THEN 'NUEVO' 
            ELSE 'VISTO' 
        END AS EstadoTexto

    FROM dbo.Comunicado C
    INNER JOIN dbo.UsuarioRol UR
        ON UR.Id_Rol = 3               -- Apoderado
       AND UR.Id_Usuario = @Id_Usuario
    LEFT JOIN dbo.ComunicadoVisto CV
        ON CV.Id_Comunicado = C.Id_Comunicado
       AND CV.Id_Usuario    = @Id_Usuario
    WHERE
        -- Vigencia del comunicado
        (C.FechaFinal IS NULL OR CAST(C.FechaFinal AS DATE) >= @Hoy)
    ORDER BY
        CASE WHEN CV.Id_Comunicado IS NULL THEN 0 ELSE 1 END ASC, -- primero NUEVOS
        C.FechaCreacion DESC,
        C.Id_Comunicado DESC;
END
GO

CREATE OR ALTER PROC dbo.mov_marcar_comunicado_visto
    @Id_Comunicado INT,
    @Id_Usuario    INT
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    IF NOT EXISTS (
        SELECT 1
        FROM dbo.ComunicadoVisto
        WHERE Id_Comunicado = @Id_Comunicado
          AND Id_Usuario    = @Id_Usuario
    )
    BEGIN
        INSERT INTO dbo.ComunicadoVisto (Id_Comunicado, Id_Usuario, FechaVisto)
        VALUES (@Id_Comunicado, @Id_Usuario, GETDATE());
    END
END
GO

CREATE OR ALTER PROC dbo.mov_obtener_matricula_actual_por_alumno
    @Id_Alumno INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        M.Id_Matricula,
        M.Id_Alumno,
        M.FechaMatricula,
        M.Total,
        M.Estado
    FROM dbo.Matricula M
    WHERE M.Id_Alumno = @Id_Alumno
      AND M.Estado = 'A'   -- activa/vigente
    ORDER BY M.FechaMatricula DESC;
END
GO

CREATE OR ALTER PROC dbo.mov_resumen_cuotas_por_matricula
    @Id_Matricula INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        -- Total SOLO de cuotas activas
        SUM(CASE 
                WHEN Activo = 1 
                THEN TotalLinea 
                ELSE 0 
            END) AS Total,

        -- Pagado: solo cuotas activas y cobradas
        SUM(CASE 
                WHEN Activo = 1 
                 AND EstadoPago = 'C'
                THEN TotalLinea 
                ELSE 0 
            END) AS Pagado,

        -- Pendiente: solo cuotas activas y pendientes
        SUM(CASE 
                WHEN Activo = 1 
                 AND EstadoPago = 'P'
                THEN TotalLinea 
                ELSE 0 
            END) AS Pendiente
    FROM MatriculaDetalle
    WHERE Id_Matricula = @Id_Matricula;
END
GO

select * from Matricula

CREATE INDEX IX_Usuario_NombreUsuario ON dbo.Usuario(NombreUsuario);
CREATE INDEX IX_Usuario_Documento     ON dbo.Usuario(Documento);

-- Hijos por apoderado
CREATE INDEX IX_Alumno_IdApoderado_Activo ON dbo.Alumno(Id_Apoderado, Activo);

-- Matrícula por alumno (última)
CREATE INDEX IX_Matricula_IdAlumno_Fecha ON dbo.Matricula(Id_Alumno, FechaMatricula DESC, Id_Matricula DESC);

-- Cuotas por matrícula
CREATE INDEX IX_MatriculaDetalle_IdMatricula_Nro ON dbo.MatriculaDetalle(Id_Matricula, NroCuota);

-- Comunicados vigentes y por rol
CREATE INDEX IX_Comunicado_Rol_Fecha ON dbo.Comunicado(Id_Rol, FechaCreacion DESC);

-- Visto por usuario y comunicado
CREATE INDEX IX_ComunicadoVisto_Usuario_Comunicado ON dbo.ComunicadoVisto(Id_Usuario, Id_Comunicado);
go

exec mov_login_apoderado 'hquiroz2','03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4'
EXEC dbo.mov_listar_hijos_por_apoderado @Id_Apoderado = 6;
EXEC dbo.mov_obtener_matricula_actual_por_alumno 4;
EXEC dbo.mov_resumen_cuotas_por_matricula @Id_Matricula = 4;
EXEC dbo.mov_listar_cuotas_por_matricula @Id_Matricula = 4;

select *from Matricula
SELECT * FROM Apoderado
SELECT * FROM Alumno
SELECT * FROM MatriculaDetalle WHERE Id_Matricula= 4