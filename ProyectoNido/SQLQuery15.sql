CREATE OR ALTER PROC dbo.Cat_Categoria_BuscarPorNombre
    @TextoBuscar VARCHAR(150) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id        AS Id,
        c.Nombre    AS Nombre
    FROM dbo.Categoria c
    WHERE (@TextoBuscar IS NULL OR LTRIM(RTRIM(@TextoBuscar)) = '')
       OR c.Nombre LIKE '%' + @TextoBuscar + '%'
    ORDER BY c.Nombre;
END;
GO

CREATE OR ALTER PROC dbo.Cat_Categoria_ListarCombo
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.Id     AS Id,
        c.Nombre AS Nombre
    FROM dbo.Categoria c
    ORDER BY c.Nombre;
END;
GO

CREATE OR ALTER PROC dbo.Cat_Categoria_Insertar
    @Nombre VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM dbo.Categoria WHERE Nombre = @Nombre)
        RAISERROR('Ya existe una categoría con ese nombre.', 16, 1);

    INSERT INTO dbo.Categoria(Nombre) VALUES (@Nombre);

    SELECT SCOPE_IDENTITY() AS Id;
END;
GO

CREATE OR ALTER PROC dbo.Cat_Categoria_Actualizar
    @Id INT,
    @Nombre VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM dbo.Categoria WHERE Id = @Id)
        RAISERROR('Categoría no existe.', 16, 1);

    IF EXISTS (SELECT 1 FROM dbo.Categoria WHERE Nombre = @Nombre AND Id <> @Id)
        RAISERROR('Ya existe otra categoría con ese nombre.', 16, 1);

    UPDATE dbo.Categoria
    SET Nombre = @Nombre
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROC dbo.Cat_Categoria_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    -- Evita borrar si está usada por Insumo
    IF EXISTS (SELECT 1 FROM dbo.Insumo WHERE IdCategoria = @Id)
        RAISERROR('No se puede eliminar: la categoría está usada en Insumo.', 16, 1);

    DELETE FROM dbo.Categoria WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROC dbo.Cat_Categoria_ObtenerPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre
    FROM dbo.Categoria
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROC dbo.Cat_Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT Id, Nombre
    FROM dbo.Categoria
    ORDER BY Nombre;
END;
GO


CREATE OR ALTER PROC dbo.UnidadMedida_BuscarPorNombre
    @TextoBuscar VARCHAR(150) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        u.IdUnidad AS Id,
        CONCAT(u.Nombre, IIF(u.Abreviatura IS NULL OR u.Abreviatura = '', '', ' (' + u.Abreviatura + ')')) AS Nombre
    FROM dbo.Unidad_Medida u
    WHERE (@TextoBuscar IS NULL OR LTRIM(RTRIM(@TextoBuscar)) = '')
       OR u.Nombre LIKE '%' + @TextoBuscar + '%'
       OR u.Abreviatura LIKE '%' + @TextoBuscar + '%'
    ORDER BY u.Nombre;
END;
GO

exec Cat_Categoria_BuscarPorNombre 'L'

CREATE OR ALTER PROC UnidadMedida_BuscarPorNombre
    @Texto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        IdUnidad AS Id,
        Nombre
    FROM Unidad_Medida
    WHERE (@Texto IS NULL OR Nombre LIKE '%' + @Texto + '%')
    ORDER BY Nombre;
END;
GO
----------------------------------------------------------

CREATE OR ALTER PROC Categoria_BuscarPorNombre
    @Texto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        Id AS Id,
        Nombre
    FROM Categoria
    WHERE (@Texto IS NULL OR @Texto = '' OR Nombre LIKE '%' + @Texto + '%')
    ORDER BY Nombre;
END;
GO

CREATE OR ALTER PROC Insumo_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        i.id,
        i.Nombre,
        i.IdUnidad,
        um.Nombre AS Unidad,
        i.IdCategoria,
        c.Nombre AS Categoria,
        i.Precio_sugerido,
        i.Activo
    FROM Insumo i
    INNER JOIN Unidad_Medida um ON um.IdUnidad = i.IdUnidad
    INNER JOIN Categoria c ON c.Id = i.IdCategoria
    ORDER BY i.Nombre;
END;
GO

CREATE OR ALTER PROC Insumo_BuscarPorNombre
    @Texto VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        i.id,
        i.Nombre,
        i.IdUnidad,
        um.Nombre AS Unidad,
        i.IdCategoria,
        c.Nombre AS Categoria,
        i.Precio_sugerido,
        i.Activo
    FROM Insumo i
    INNER JOIN Unidad_Medida um ON um.IdUnidad = i.IdUnidad
    INNER JOIN Categoria c ON c.Id = i.IdCategoria
    WHERE (@Texto IS NULL OR @Texto = '' OR i.Nombre LIKE '%' + @Texto + '%')
    ORDER BY i.Nombre;
END;
GO

CREATE OR ALTER PROC Insumo_Insertar
    @Nombre         VARCHAR(100),
    @IdUnidad       INT,
    @IdCategoria    INT,
    @PrecioSugerido DECIMAL(10,2),
    @Activo         BIT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Insumo(Nombre, IdUnidad, IdCategoria, Precio_sugerido, Activo)
    VALUES(@Nombre, @IdUnidad, @IdCategoria, @PrecioSugerido, @Activo);

    SELECT SCOPE_IDENTITY() AS IdGenerado;
END;
GO

CREATE OR ALTER PROC Insumo_Actualizar
    @IdInsumo       INT,
    @Nombre         VARCHAR(100),
    @IdUnidad       INT,
    @IdCategoria    INT,
    @PrecioSugerido DECIMAL(10,2),
    @Activo         BIT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Insumo
    SET Nombre = @Nombre,
        IdUnidad = @IdUnidad,
        IdCategoria = @IdCategoria,
        Precio_sugerido = @PrecioSugerido,
        Activo = @Activo
    WHERE id = @IdInsumo;
END;
GO

CREATE OR ALTER PROC Insumo_Eliminar
    @IdInsumo INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Insumo
    WHERE id = @IdInsumo;
END;
GO

