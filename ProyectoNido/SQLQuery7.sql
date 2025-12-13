CREATE OR ALTER PROC Categoria_Insertar
    @Nombre VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    -- Validar duplicado
    IF EXISTS (SELECT 1 FROM Categoria WHERE Nombre = @Nombre)
    BEGIN
        RAISERROR('La categoría ya existe.', 16, 1);
        RETURN;
    END

    INSERT INTO Categoria (Nombre)
    VALUES (@Nombre);

    -- retornar el ID generado
    SELECT SCOPE_IDENTITY() AS NuevoId;
END;
GO

CREATE OR ALTER PROC Categoria_Actualizar
    @Id INT,
    @Nombre VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Id = @Id)
    BEGIN
        RAISERROR('La categoría no existe.', 16, 1);
        RETURN;
    END

    -- validar duplicado
    IF EXISTS (SELECT 1 FROM Categoria WHERE Nombre = @Nombre AND Id <> @Id)
    BEGIN
        RAISERROR('Ya existe otra categoría con ese nombre.', 16, 1);
        RETURN;
    END

    UPDATE Categoria
    SET Nombre = @Nombre
    WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROC Categoria_Eliminar
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT 1 FROM Categoria WHERE Id = @Id)
    BEGIN
        RAISERROR('La categoría no existe.', 16, 1);
        RETURN;
    END

    DELETE FROM Categoria WHERE Id = @Id;
END;
GO

CREATE OR ALTER PROC Categoria_Listar
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Nombre
    FROM Categoria
    ORDER BY Nombre;
END;
GO

CREATE OR ALTER PROC Categoria_Buscar
    @Texto VARCHAR(150)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        Id,
        Nombre
    FROM Categoria
    WHERE Nombre LIKE '%' + @Texto + '%'
    ORDER BY Nombre;
END;
GO
SELECT * FROM Cargo



