CREATE OR ALTER PROC Tarifario_Insertar
    @Tipo CHAR(1),
    @Nombre VARCHAR(50),
    @Descripcion VARCHAR(200),
    @Periodo INT,
    @Valor DECIMAL(6,2)
AS
BEGIN
    INSERT INTO Tarifario (Tipo, Nombre, Descripcion, Periodo, Valor)
    VALUES (@Tipo, @Nombre, @Descripcion, @Periodo, @Valor);

    SELECT SCOPE_IDENTITY() AS Id_Tarifario;
END;
GO

CREATE OR ALTER PROC Tarifario_Actualizar
    @Id_Tarifario INT,
    @Tipo CHAR(1),
    @Nombre VARCHAR(50),
    @Descripcion VARCHAR(200),
    @Periodo INT,
    @Valor DECIMAL(6,2)
AS
BEGIN
    UPDATE Tarifario
    SET Tipo = @Tipo,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        Periodo = @Periodo,
        Valor = @Valor
    WHERE Id_Tarifario = @Id_Tarifario;
END;
GO

CREATE OR ALTER PROC Tarifario_Eliminar
    @Id_Tarifario INT
AS
BEGIN
    DELETE FROM Tarifario
    WHERE Id_Tarifario = @Id_Tarifario;
END;
GO

CREATE OR ALTER PROC Tarifario_Listar
AS
BEGIN
    SELECT Id_Tarifario, Tipo, Nombre, Descripcion, Periodo, Valor
    FROM Tarifario
    ORDER BY Nombre;
END;
GO

CREATE OR ALTER PROC Tarifario_Consultar
    @Id_Tarifario INT
AS
BEGIN
    SELECT *
    FROM Tarifario
    WHERE Id_Tarifario = @Id_Tarifario;
END;
GO


CREATE OR ALTER PROC Cuota_Insertar
    @Id_Tarifario      INT,
    @NroCuota          INT,
    @FechaPagoSugerido DATE,
    @Monto             DECIMAL(6,2) = NULL,
    @Descuento         DECIMAL(6,2) = NULL,
    @Adicional         DECIMAL(6,2) = NULL,
    @NombreCuota       VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Cuota (Id_Tarifario, NroCuota, FechaPagoSugerido, Monto, Descuento, Adicional, NombreCuota)
    VALUES (@Id_Tarifario, @NroCuota, @FechaPagoSugerido, @Monto, @Descuento, @Adicional, @NombreCuota);

    SELECT SCOPE_IDENTITY() AS Id_Cuota;
END;
GO

CREATE OR ALTER PROC Cuota_Actualizar
    @Id_Cuota          INT,
    @Id_Tarifario      INT,
    @NroCuota          INT,
    @FechaPagoSugerido DATE,
    @Monto             DECIMAL(6,2) = NULL,
    @Descuento         DECIMAL(6,2) = NULL,
    @Adicional         DECIMAL(6,2) = NULL,
    @NombreCuota       VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Cuota
    SET Id_Tarifario      = @Id_Tarifario,
        NroCuota          = @NroCuota,
        FechaPagoSugerido = @FechaPagoSugerido,
        Monto             = @Monto,
        Descuento         = @Descuento,
        Adicional         = @Adicional,
        NombreCuota       = @NombreCuota
    WHERE Id_Cuota = @Id_Cuota;
END;
GO

CREATE OR ALTER PROC Cuota_Eliminar
    @Id_Cuota INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Cuota
    WHERE Id_Cuota = @Id_Cuota;
END;
GO

CREATE OR ALTER PROC Cuota_Listar
    @Id_Tarifario INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        c.Id_Cuota,
        c.Id_Tarifario,
        c.NroCuota,
        c.FechaPagoSugerido,
        c.Monto,
        c.Descuento,
        c.Adicional,
        c.NombreCuota
    FROM Cuota c
    WHERE (@Id_Tarifario IS NULL OR c.Id_Tarifario = @Id_Tarifario)
    ORDER BY c.Id_Tarifario, c.NroCuota;
END;
GO

CREATE OR ALTER PROC Cuota_Consultar
    @Id_Cuota INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT *
    FROM Cuota
    WHERE Id_Cuota = @Id_Cuota;
END;
GO

select * from Cuota
select * from Matricula
exec MatriculaDetalle_ListarPorMatricula 3

