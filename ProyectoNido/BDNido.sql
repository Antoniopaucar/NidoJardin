-- Crear la base de datos
CREATE DATABASE BDNido;
GO

USE BDNido;
GO

/* ==========================
   TABLAS DE SEGURIDAD
   ========================== */

CREATE TABLE Usuario (
    Id INT PRIMARY KEY IDENTITY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
	Clave VARCHAR(256) NOT NULL,
	Nombres VARCHAR(100) NOT NULL,
    ApPaterno VARCHAR(50) NOT NULL,
    ApMaterno VARCHAR(50) NOT NULL,
    Dni CHAR(8) UNIQUE,
    FechaNacimiento DATE,
    Sexo CHAR(1) CHECK (Sexo IN ('M','F')),
    Direccion VARCHAR(200),
    Telefono VARCHAR(20),
    Email VARCHAR(100),
	Activo BIT DEFAULT 1,
    Intentos INT DEFAULT 0,
    Bloqueado BIT DEFAULT 0,
    FechaBloqueo DATETIME NULL,
    UltimoIntento DATETIME NULL,
    UltimoLoginExitoso DATETIME NULL,
    FechaCreacion DATETIME DEFAULT GETDATE()
);

CREATE TABLE Rol (
    Id_Rol INT PRIMARY KEY IDENTITY,
    NombreRol VARCHAR(50) NOT NULL UNIQUE,
    Descripcion VARCHAR(200)
);

CREATE TABLE Permiso (
    Id_Permiso INT PRIMARY KEY IDENTITY,
    NombrePermiso VARCHAR(100) NOT NULL UNIQUE,
    Descripcion VARCHAR(200)
);

CREATE TABLE UsuarioRol (
    Id_Usuario INT NOT NULL,
    Id_Rol INT NOT NULL,
    PRIMARY KEY (Id_Usuario, Id_Rol),
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id),
    FOREIGN KEY (Id_Rol) REFERENCES Rol(Id_Rol)
);

CREATE TABLE RolPermiso (
    Id_Rol INT NOT NULL,
    Id_Permiso INT NOT NULL,
	Tipo varchar(20)
    PRIMARY KEY (Id_Rol, Id_Permiso),
    FOREIGN KEY (Id_Rol) REFERENCES Rol(Id_Rol),
    FOREIGN KEY (Id_Permiso) REFERENCES Permiso(Id_Permiso)
);

CREATE TABLE PermisoUsuario (
    Id_Usuario INT NOT NULL,
    Id_Permiso INT NOT NULL,
	TipoPermiso varchar(20)
    PRIMARY KEY (Id_Usuario, Id_Permiso),
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id),
    FOREIGN KEY (Id_Permiso) REFERENCES Permiso(Id_Permiso)
);

/* ==========================
   TABLAS DE UBICACIÓN Y PERSONAS
   ========================== */

CREATE TABLE Distrito (
    Id_Distrito INT PRIMARY KEY IDENTITY,
    Ubigeo VARCHAR(6) NOT NULL UNIQUE,
    Nombre VARCHAR(100) NOT NULL
);

CREATE TABLE Apoderado (
    Id_Apoderado INT PRIMARY KEY IDENTITY,
    Id_Distrito INT NOT NULL,
    Id_Usuario INT NULL,
    CopiaDni VARCHAR(100),
    FOREIGN KEY (Id_Distrito) REFERENCES Distrito(Id_Distrito),
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id)
);

CREATE TABLE Alumno (
    Id_Alumno INT PRIMARY KEY IDENTITY,
    Id_Apoderado INT NOT NULL,
    Nombres VARCHAR(100) NOT NULL,
    ApPaterno VARCHAR(50) NOT NULL,
    ApMaterno VARCHAR(50) NOT NULL,
    Dni CHAR(8) UNIQUE,
    FechaNacimiento DATE,
    Sexo CHAR(1) CHECK (Sexo IN ('M','F')),
    Activo BIT DEFAULT 1,
    Fotos VARCHAR(100),
    CopiaDni VARCHAR(100),
    PermisosPublicidad VARCHAR(100),
    CarnetSeguro VARCHAR(100),
    FOREIGN KEY (Id_Apoderado) REFERENCES Apoderado(Id_Apoderado)
);

CREATE TABLE Profesor (
    Id_Profesor INT PRIMARY KEY IDENTITY,
    Id_Usuario INT NULL,
    FechaIngreso DATE,
    TituloProfesional VARCHAR(100),
    Cv VARCHAR(100),
    EvaluacionPsicologica VARCHAR(100),
    Fotos VARCHAR(100),
    VerificacionDomiciliaria VARCHAR(100),
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id)
);

/* ==========================
   TABLAS DE GESTIÓN ESCOLAR
   ========================== */

CREATE TABLE Comunicado (
    Id_Comunicado INT PRIMARY KEY IDENTITY,
    Id_Usuario INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500),
    FechaCreacion DATETIME DEFAULT GETDATE(),
    FechaFinal DATETIME NULL,
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id)
);

CREATE TABLE Salon (
    Id_Salon INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(50) NOT NULL,
    Aforo INT,
    Dimensiones VARCHAR(50),
    Activo BIT DEFAULT 1
);

CREATE TABLE Nivel (
    Id_Nivel INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(50) NOT NULL,
    Descripcion VARCHAR(200)
);

CREATE TABLE GrupoAnual (
    Id_GrupoAnual INT PRIMARY KEY IDENTITY,
    Id_Salon INT NOT NULL,
    Id_Profesor INT NOT NULL,
    Id_Nivel INT NOT NULL,
	Periodo tinyint NOT NULL,
    FOREIGN KEY (Id_Salon) REFERENCES Salon(Id_Salon),
    FOREIGN KEY (Id_Profesor) REFERENCES Profesor(Id_Profesor),
    FOREIGN KEY (Id_Nivel) REFERENCES Nivel(Id_Nivel)
);

CREATE TABLE GrupoAnualHistorico (
    Id_GAHistorico INT PRIMARY KEY IDENTITY,
    Id_Salon INT NOT NULL,
    Id_Profesor INT NOT NULL,
    Id_Nivel INT NOT NULL,
	Periodo tinyint NOT NULL
);

CREATE TABLE ServicioAdicional (
    Id_ServicioAdicional INT PRIMARY KEY IDENTITY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200),
	Tipo char(1), --H - hora , D - dia, M - mes  --cuna por hora, comida por dia o por mes
    Costo DECIMAL(6,2) NOT NULL --costo por hora
);

CREATE TABLE GrupoServicio (
    Id_GrupoServicio INT PRIMARY KEY IDENTITY,
    Id_Salon INT NOT NULL,
    Id_Profesor INT NOT NULL,
    Id_ServicioAdicional INT NOT NULL,
	Periodo tinyint NOT NULL,
    FOREIGN KEY (Id_Salon) REFERENCES Salon(Id_Salon),
    FOREIGN KEY (Id_Profesor) REFERENCES Profesor(Id_Profesor),
    FOREIGN KEY (Id_ServicioAdicional) REFERENCES ServicioAdicional(Id_ServicioAdicional)
);

CREATE TABLE GrupoServicioHistorico (
    Id_GSHistorico INT PRIMARY KEY IDENTITY,
    Id_Salon INT NOT NULL,
    Id_Profesor INT NOT NULL,
    Id_ServicioAdicional INT NOT NULL,
	Periodo tinyint NOT NULL

);

CREATE TABLE ServicioAlumno (
    Id_ServicioAlumno INT PRIMARY KEY IDENTITY,
    Id_GrupoServicio INT NOT NULL,
    Id_Alumno INT NOT NULL,
    FechaInicio DATE,
    FechaFinal DATE,
    FechaPago DATE,
	HoraInicio time,
	HoraFinal time,
    Monto DECIMAL(6,2),
    FOREIGN KEY (Id_GrupoServicio) REFERENCES GrupoServicio(Id_GrupoServicio),
    FOREIGN KEY (Id_Alumno) REFERENCES Alumno(Id_Alumno)
);

CREATE TABLE Tarifario (
    Id_Tarifario INT PRIMARY KEY IDENTITY,
	Tipo char(1), ---D- descuento,A- adicional,M- matricula
    Nombre VARCHAR(100) NOT NULL,
	Descripcion VARCHAR(200),
	Periodo tinyint NOT NULL,
    Valor DECIMAL(6,2) NOT NULL
);

CREATE TABLE Matricula (
    Id_Matricula INT PRIMARY KEY IDENTITY,
    Id_Alumno INT NOT NULL,
    Id_GrupoAnual INT NOT NULL,
	Id_Tarifario INT NOT NULL, -- TIENE Q SER AUTOMATICO O POR DEFECTO ( NO SE TIENE QUE SELECCIONAR)
    Codigo VARCHAR(20) NOT NULL UNIQUE,
    --Periodo tinyint NOT NULL,
    FechaMatricula DATE,
    FechaPago DATE,
    --MontoMatricula DECIMAL(6,2),
    FOREIGN KEY (Id_Alumno) REFERENCES Alumno(Id_Alumno),
    FOREIGN KEY (Id_GrupoAnual) REFERENCES GrupoAnual(Id_GrupoAnual),
	FOREIGN KEY (Id_Tarifario) REFERENCES Tarifario(Id_Tarifario)
);

CREATE TABLE Cuota (
    Id_Cuota INT PRIMARY KEY IDENTITY,
    Id_Matricula INT NOT NULL,
	Id_Tarifario INT NOT NULL, -- SI SELECCIONA DESCUENTO ANUAL SE GENERARA LAS 10 CUOTAS CON SU RESPECTIVO DESCUENTO 
    NroCuota INT NOT NULL,
    FechaPagoSugerido DATE,
    FechaPago DATE,
    Monto DECIMAL(6,2), --100
    Descuento DECIMAL(6,2) DEFAULT 0, -- 5
    Adicional DECIMAL(6,2) DEFAULT 0, -- fecha actual > fechaPagoSgerido =  (fecha actual - fechaPagoSgerido) * valor adicional tipo A
    FOREIGN KEY (Id_Matricula) REFERENCES Matricula(Id_Matricula),
	FOREIGN KEY (Id_Tarifario) REFERENCES Tarifario(Id_Tarifario)
);


--****************************** PROCEDIMIENTO, TRIGGERS Y OTROS *************************************

--CREATE proc [dbo].[ValidarUsuario]
--(@NombreUsuario varchar(50),@Clave varchar(256))
--as 
--declare @intentos tinyint
--declare @Bloqueado bit
--declare @Clavedb varchar(256)

--set @intentos=(Select Intentos from Usuario where NombreUsuario = @NombreUsuario)
--set @Bloqueado=(Select Bloqueado from Usuario where NombreUsuario = @NombreUsuario)
--set @Clavedb =(Select Clave from Usuario where NombreUsuario = @NombreUsuario)
 
--if(@Bloqueado=1)
--	begin
--		select 'Usuario Bloqueado' as resultado
--	end
--else
--	SET NOCOUNT ON;

--		if(@Clave != @Clavedb OR @Clavedb IS NULL)
--			if (@intentos >=3)
--				begin
--					update Usuario 
--					Set Intentos = Intentos+1,Bloqueado = 1
--					where NombreUsuario = @NombreUsuario
--					select 'Cuenta temporalmente bloqueada por múltiples intentos fallidos. Inténtelo más tarde.' as resultado
--				end
--			else
--				BEGIN
--					update Usuario 
--					Set Intentos =Intentos+1
--					where NombreUsuario = @NombreUsuario
--					select 'Usuario no encontrado y/o Contraseña incorrecta' as resultado
--				END
--		else
--			begin 
--				update Usuario
--				Set Intentos =0
--				where NombreUsuario = @NombreUsuario
--				select 'Todo OK' as resultado
--			end

CREATE PROCEDURE [dbo].[ValidarUsuario]
(
    @NombreUsuario VARCHAR(50),
    @Clave VARCHAR(256)
)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @Intentos TINYINT,
            @Bloqueado BIT,
            @ClaveDB VARCHAR(256),
            @IdUsuario INT;

    SELECT 
        @Intentos = Intentos,
        @Bloqueado = Bloqueado,
        @ClaveDB = Clave,
        @IdUsuario = Id
    FROM Usuario
    WHERE NombreUsuario = @NombreUsuario;

    IF @IdUsuario IS NULL
    BEGIN
        SELECT 
            CAST(0 AS BIT) AS Exito, 
            'Usuario no encontrado y/o Contraseña incorrecta' AS Mensaje,
            NULL AS Id_Usuario, NULL AS Id_Rol, NULL AS Id_Permiso;
        RETURN;
    END

    -- 🧱 Verificar si está bloqueado
    IF @Bloqueado = 1
    BEGIN
        SELECT 
            CAST(0 AS BIT) AS Exito, 
            'Usuario bloqueado' AS Mensaje,
            @IdUsuario AS Id_Usuario, NULL AS Id_Rol, NULL AS Id_Permiso;
        RETURN;
    END

    -- 🔑 Validar contraseña
    IF (@Clave <> @ClaveDB OR @ClaveDB IS NULL)
    BEGIN
        -- Intento fallido → actualizar fecha de intento
        IF (@Intentos >= 3)
        BEGIN
            UPDATE Usuario 
            SET Intentos = Intentos + 1,
                Bloqueado = 1,
                FechaBloqueo = GETDATE(),
                UltimoIntento = GETDATE()
            WHERE Id = @IdUsuario;

            SELECT 
                CAST(0 AS BIT) AS Exito, 
                'Cuenta temporalmente bloqueada por múltiples intentos fallidos.' AS Mensaje,
                @IdUsuario AS Id_Usuario, NULL AS Id_Rol, NULL AS Id_Permiso;
        END
        ELSE
        BEGIN
            UPDATE Usuario 
            SET Intentos = Intentos + 1,
                UltimoIntento = GETDATE()
            WHERE Id = @IdUsuario;

            SELECT 
                CAST(0 AS BIT) AS Exito, 
                'Usuario no encontrado y/o Contraseña incorrecta' AS Mensaje,
                @IdUsuario AS Id_Usuario, NULL AS Id_Rol, NULL AS Id_Permiso;
        END
        RETURN;
    END

    --Clave correcta → resetear intentos y actualizar fechas
    UPDATE Usuario 
    SET Intentos = 0,
        UltimoLoginExitoso = GETDATE(),
        UltimoIntento = GETDATE()
    WHERE Id = @IdUsuario;

    --Devolver todos los roles y permisos del usuario
    SELECT DISTINCT 
        CAST(1 AS BIT) AS Exito,
        'Bienvenido' AS Mensaje,
        u.Id AS Id_Usuario,
        ur.Id_Rol,
        COALESCE(pu.Id_Permiso, rp.Id_Permiso) AS Id_Permiso
    FROM Usuario u
    LEFT JOIN UsuarioRol ur ON u.Id = ur.Id_Usuario
    LEFT JOIN RolPermiso rp ON ur.Id_Rol = rp.Id_Rol
    LEFT JOIN PermisoUsuario pu ON u.Id = pu.Id_Usuario
    WHERE u.Id = @IdUsuario;
END

CREATE PROCEDURE ValidarContraseniaSegura
    @Usuario VARCHAR(50),
    @Contrasena VARCHAR(256),
    @EsValido BIT OUTPUT,
    @Mensaje VARCHAR(200) OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ContrasenaLower VARCHAR(256) = LOWER(@Contrasena)
    DECLARE @UsuarioLower VARCHAR(50)
    DECLARE @NombresLower VARCHAR(100)
    DECLARE @ApPaternoLower VARCHAR(100)
    DECLARE @ApMaternoLower VARCHAR(100)
    DECLARE @DireccionLower VARCHAR(100)
    DECLARE @TelefonoLower VARCHAR(20)
    DECLARE @EmailLower VARCHAR(100)

    -- Obtener datos del usuario
    SELECT 
        @UsuarioLower = LOWER(NombreUsuario),
        @NombresLower = LOWER(ISNULL(Nombres, '')),
        @ApPaternoLower = LOWER(ISNULL(ApPaterno, '')),
        @ApMaternoLower = LOWER(ISNULL(ApMaterno, '')),
        @DireccionLower = LOWER(ISNULL(Direccion, '')),
        @TelefonoLower = LOWER(ISNULL(Telefono, '')),
        @EmailLower = LOWER(ISNULL(Email, ''))
    FROM Usuario
    WHERE NombreUsuario = @Usuario

    -- Validación
    IF @UsuarioLower LIKE '%' + @ContrasenaLower + '%'
        OR @NombresLower LIKE '%' + @ContrasenaLower + '%'
        OR @ApPaternoLower LIKE '%' + @ContrasenaLower + '%'
        OR @ApMaternoLower LIKE '%' + @ContrasenaLower + '%'
        OR @DireccionLower LIKE '%' + @ContrasenaLower + '%'
        OR @TelefonoLower LIKE '%' + @ContrasenaLower + '%'
        OR @EmailLower LIKE '%' + @ContrasenaLower + '%'
    BEGIN
        SET @EsValido = 0
        SET @Mensaje = 'La contraseña no debe contener datos personales (usuario, nombre, apellidos, dirección, teléfono o correo electrónico).'
    END
    ELSE
    BEGIN
        SET @EsValido = 1
        SET @Mensaje = 'Contraseña válida.'
    END
END

--*****************************************************************************

create proc [dbo].[listar_usuarios]
as
select  * from Usuario

ALTER proc [dbo].[listar_comunicados]
as
select  c.Id_Comunicado,
		c.Id_Usuario,
		u.NombreUsuario,
        u.Nombres,
        u.ApPaterno,
        u.ApMaterno,
		c.Nombre,
		c.Descripcion,
		c.FechaCreacion,
		c.FechaFinal
from Comunicado as c inner join
Usuario as u on c.Id_Usuario = u.Id
where c.FechaFinal >= CAST(GETDATE() as DATE) or c.FechaFinal is null
order by c.FechaCreacion desc

create proc [dbo].[listar_distritos]
as
select  * from Distrito

create proc [dbo].[listar_nivel]
as
select  * from Nivel

create proc [dbo].[listar_salon]
as
select  * from Salon

create proc [dbo].[listar_roles]
as
select  * from Rol

create proc [dbo].[listar_permisos]
as
select  * from Permiso

create proc [dbo].[listar_usuario_rol]
as
select  ur.Id_Usuario,
		u.NombreUsuario,
		ur.Id_Rol,
		r.NombreRol
from UsuarioRol as ur 
inner join Usuario as u on ur.Id_Usuario = u.Id
inner join Rol as r on ur.Id_Rol = r.Id_Rol

--*****************************************************************************
CREATE PROCEDURE sp_BuscarIdEnTablas
    @IdBuscado INT,
    @ListaTablasCampos NVARCHAR(MAX) -- formato: 'Tabla1:Campo1,Tabla2:Campo2,Tabla3:Campo3'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @sql NVARCHAR(MAX) = N'';
    DECLARE @tabla NVARCHAR(200), @campo NVARCHAR(200), @pos INT;
    DECLARE @resultado BIT = 0;

    WHILE LEN(@ListaTablasCampos) > 0
    BEGIN
        -- Extrae el siguiente par tabla:campo
        SET @pos = CHARINDEX(',', @ListaTablasCampos);
        IF @pos > 0
        BEGIN
            SET @tabla = LEFT(@ListaTablasCampos, @pos - 1);
            SET @ListaTablasCampos = SUBSTRING(@ListaTablasCampos, @pos + 1, LEN(@ListaTablasCampos));
        END
        ELSE
        BEGIN
            SET @tabla = @ListaTablasCampos;
            SET @ListaTablasCampos = '';
        END

        -- Divide la parte tabla:campo
        SET @campo = PARSENAME(REPLACE(@tabla, ':', '.'), 1);
        SET @tabla = PARSENAME(REPLACE(@tabla, ':', '.'), 2);

        -- Construye SQL dinámico para verificar si existe el ID
        SET @sql = N'
        IF EXISTS (SELECT 1 FROM ' + QUOTENAME(@tabla) + ' WHERE ' + QUOTENAME(@campo) + ' = @IdBuscado)
        BEGIN
            SET @resultado = 1;
        END';

        EXEC sp_executesql @sql, N'@IdBuscado INT, @resultado BIT OUTPUT', @IdBuscado = @IdBuscado, @resultado = @resultado OUTPUT;

        -- Si ya lo encontró, termina
        IF @resultado = 1
            BREAK;
    END

    SELECT CASE WHEN @resultado = 1 THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Existe;
END;
GO

create proc [dbo].[eliminar_usuarios]
(@id int)
as
delete from Usuario
where Id = @id

create proc [dbo].[eliminar_comunicados]
(@Id int)
as
delete from Comunicado
where Id_Comunicado = @Id

create proc [dbo].[eliminar_distritos]
(@Id int)
as
delete from Distrito
where Id_Distrito = @Id

create proc [dbo].[eliminar_nivel]
(@Id int)
as
delete from Nivel
where Id_Nivel = @Id

create proc [dbo].[eliminar_salon]
(@Id int)
as
delete from Salon
where Id_Salon = @Id

create proc [dbo].[eliminar_roles]
(@Id int)
as
delete from Rol
where Id_Rol = @Id

create proc [dbo].[eliminar_usuario_rol]
(@Id_Usuario int,
@Id_Rol int)
as
delete from UsuarioRol
where Id_Usuario = @Id_Usuario AND Id_Rol = @Id_Rol

--****************************************************************************************************

CREATE proc [dbo].[insertar_usuarios]
(@NombreUsuario varchar(50),
@Clave varchar(256),
@Nombres varchar(100),
@ApPaterno varchar(50),
@ApMaterno varchar(50),
@Dni char(8),
@FechaNacimiento date,
@Sexo char(1),
@Direccion varchar(200),
@Telefono varchar(20),
@Email varchar(100))
as
insert Usuario (NombreUsuario,Clave,Nombres,ApPaterno,ApMaterno,Dni,FechaNacimiento,Sexo,Direccion,Telefono,Email)
values (@NombreUsuario,@Clave,@Nombres,@ApPaterno,@ApMaterno,@Dni,@FechaNacimiento,@Sexo,@Direccion,@Telefono,@Email)

CREATE proc [dbo].[insertar_comunicados]
(@Id_Usuario int,
@Nombre varchar(100),
@Descripcion varchar(500),
@FechaFinal date)
as
insert Comunicado (Id_Usuario,Nombre,Descripcion,FechaFinal)
values (@Id_Usuario,@Nombre,@Descripcion,@FechaFinal)

CREATE proc [dbo].[insertar_distritos]
(@Ubigeo varchar(6),
@Nombre varchar(100))
as
insert Distrito(Ubigeo,Nombre)
values (@Ubigeo,@Nombre)

CREATE proc [dbo].[insertar_nivel]
(@Nombre varchar(50),
@Descripcion varchar(200))
as
insert Nivel(Nombre,Descripcion)
values (@Nombre,@Descripcion)

CREATE proc [dbo].[insertar_salon]
(@Nombre varchar(50),
@Aforo int,
@Dimensiones varchar(50))
as
insert Salon(Nombre,Aforo,Dimensiones)
values (@Nombre,@Aforo,@Dimensiones)

CREATE proc [dbo].[insertar_roles]
(@NombreRol varchar(50),
@Descripcion varchar(200))
as
insert Rol(NombreRol,Descripcion)
values (@NombreRol,@Descripcion)

CREATE proc [dbo].[insertar_permisos]
(@NombrePermiso varchar(100),
@Descripcion varchar(200))
as
insert Permiso(NombrePermiso,Descripcion)
values (@NombrePermiso,@Descripcion)

create proc [dbo].[insertar_usuario_rol]
(@Id_Usuario int,
@Id_Rol int)
as
insert UsuarioRol(Id_Usuario,id_Rol)
values (@Id_Usuario,@Id_Rol)

--********************************************************************************************************************

CREATE PROCEDURE [dbo].[modificar_usuarios]
    @Id int,
    @NombreUsuario nvarchar(50),
    @Clave nvarchar(256),
    @Nombres varchar(100),
    @ApPaterno varchar(50),
    @ApMaterno varchar(50),
    @Dni char(8),
    @FechaNacimiento date,
    @Sexo char(1),
    @Direccion varchar(200),
    @Telefono varchar(20),
    @Email varchar(100),
    @Activo bit
AS
BEGIN
    UPDATE Usuario
    SET 
        NombreUsuario = @NombreUsuario,
        Clave = CASE 
                    WHEN @Clave IS NOT NULL AND @Clave <> '' 
                    THEN @Clave 
                    ELSE Clave 
                END,
        Nombres = @Nombres,
        ApPaterno = @ApPaterno,
        ApMaterno = @ApMaterno,
        Dni = @Dni,
        FechaNacimiento = @FechaNacimiento,
        Sexo = @Sexo,
        Direccion = @Direccion,
        Telefono = @Telefono,
        Email = @Email,
        Activo = @Activo
    WHERE Id = @Id
END

create PROCEDURE [dbo].[modificar_comunicados]
    @Id int,
    @Id_Usuario int,
	@Nombre varchar(100),
	@Descripcion varchar(500),
	@FechaFinal date
AS
BEGIN
    UPDATE Comunicado
    SET 
        Id_Usuario = @Id_Usuario,
        Nombre = @Nombre,
        Descripcion = @Descripcion,
        FechaFinal = @FechaFinal
    WHERE Id_Comunicado = @Id
END

create PROCEDURE [dbo].[modificar_distritos]
    @Id int,
    @Ubigeo varchar(6),
	@Nombre varchar(100)
AS
BEGIN
    UPDATE Distrito
    SET 
        Ubigeo = @Ubigeo,
        Nombre = @Nombre
    WHERE Id_Distrito = @Id
END

create PROCEDURE [dbo].[modificar_nivel]
    @Id int,
	@Nombre varchar(50),
	@Descripcion varchar(200)
AS
BEGIN
    UPDATE Nivel
    SET 
		Nombre = @Nombre,
        Descripcion = @Descripcion
    WHERE Id_Nivel = @Id
END

CREATE PROCEDURE [dbo].[modificar_salon]
    @Id int,
	@Nombre varchar(50),
	@Aforo int,
	@Dimensiones varchar(50),
	@Activo bit
AS
BEGIN
    UPDATE Salon
    SET 
		Nombre = @Nombre,
		Aforo = @Aforo,
        Dimensiones = @Dimensiones,
		Activo = @Activo
    WHERE Id_Salon = @Id
END

create PROCEDURE [dbo].[modificar_roles]
    @Id int,
	@NombreRol varchar(50),
	@Descripcion varchar(200)
AS
BEGIN
    UPDATE Rol
    SET 
		NombreRol = @NombreRol,
        Descripcion = @Descripcion
    WHERE Id_Rol = @Id
END

create PROCEDURE [dbo].[modificar_permisos]
    @Id int,
	@NombrePermiso varchar(100),
	@Descripcion varchar(200)
AS
BEGIN
    UPDATE Permiso
    SET 
		NombrePermiso = @NombrePermiso,
        Descripcion = @Descripcion
    WHERE Id_Permiso = @Id
END

create PROCEDURE [dbo].[modificar_usuario_rol]
    @Id_Usuario int,
	@Id_Rol int
AS
BEGIN
    UPDATE UsuarioRol
    SET 
        Id_Usuario = @Id_Usuario,
		Id_Rol = @Id_Rol
    WHERE Id_Usuario = @Id_Usuario AND Id_Rol = @Id_Rol
END

--*************************************************************************

INSERT INTO Usuario 
(NombreUsuario, Clave, Nombres, ApPaterno, ApMaterno, Dni, FechaNacimiento, Sexo, Direccion, Telefono, Email)
VALUES 
('hquiroz', '03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4', 'Hebert', 'Quiroz', 'Lopez', '12345678', '1995-04-12', 'M', 'Av. Grau 123', '987654321', 'hebert.q@gmail.com');

--1234

INSERT INTO Usuario 
(NombreUsuario, Clave, Nombres, ApPaterno, ApMaterno, Dni, FechaNacimiento, Sexo, Direccion, Telefono, Email)
VALUES 
('mramirez', '8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918', 'María', 'Ramírez', 'Pérez', '87654321', '1998-09-23', 'F', 'Jr. Ayacucho 456', '999888777', 'maria.rp@hotmail.com');

--admin

INSERT INTO Usuario 
(NombreUsuario, Clave, Nombres, ApPaterno, ApMaterno, Dni, FechaNacimiento, Sexo, Direccion, Telefono, Email)
VALUES 
('jtorres', '6b75b260dcae8ba6b04d481b0312d54ffac5c0a3cb4e44334f4b5e58b7f457ee', 'Juan', 'Torres', 'Gómez', '45678912', '1992-02-05', 'M', 'Calle Lima 890', '912345678', 'juan.tg@gmail.com');
--clave2025

--usuario: ManoloRX, Everaldo, Molinero, Mercedes, Joseluna, MarioLuis, Lucianeka
--CLAVE: Manolog@l1 - Pacu&#5689,Ever@ldo10,Mol&nero15, Meche$&569, Kokem@nia12, MarioLuis$1 ,Lucia&456

--Visible="false"
select * from Usuario

select * from Comunicado

select * from UsuarioRol
select * from Permiso


select * from Nivel

----------------------------DOCENTE------------------
CREATE PROCEDURE [dbo].[modificar_profesor]
    @Id_Usuario INT,
    @FechaIngreso DATE,
    @TituloProfesional VARCHAR(100),
    @Cv VARCHAR(100),
    @EvaluacionPsicologica VARCHAR(100),
    @Fotos VARCHAR(100),
    @VerificacionDomiciliaria VARCHAR(100)
AS
BEGIN
    UPDATE Profesor
    SET FechaIngreso = @FechaIngreso,
        TituloProfesional = @TituloProfesional,
        Cv = @Cv,
        EvaluacionPsicologica = @EvaluacionPsicologica,
        Fotos = @Fotos,
        VerificacionDomiciliaria = @VerificacionDomiciliaria
    WHERE Id_Usuario = @Id_Usuario;
END

CREATE PROCEDURE [dbo].[obtener_datos_profesor_por_usuario]
    @Id_Usuario INT
AS
BEGIN
    SELECT 
        u.Id,
        u.NombreUsuario,
        u.Nombres,
        u.ApPaterno,
        u.ApMaterno,
        u.Dni,
        u.FechaNacimiento,
        u.Sexo,
        u.Direccion,
        u.Email,
        p.FechaIngreso,
        p.TituloProfesional,
        p.Cv,
        p.EvaluacionPsicologica,
        p.Fotos,
        p.VerificacionDomiciliaria
    FROM Usuario u
    LEFT JOIN Profesor p ON u.Id = p.Id_Usuario
    WHERE u.Id = @Id_Usuario;
END
alter PROCEDURE [dbo].[actualizar_datos_docente]
    @Id_Usuario              INT,           -- docente logueado
    @Nombres                 VARCHAR(100),
    @ApPaterno               VARCHAR(50),
    @ApMaterno               VARCHAR(50),
    @Dni                     CHAR(8),
    @FechaNacimiento         DATE,
    @Sexo                    CHAR(1),      -- 'M' / 'F'
    @Direccion               VARCHAR(200),
    @Email                   VARCHAR(100),

    @FechaIngreso            DATE,
    @TituloProfesional       VARCHAR(100),
    @Cv                      VARCHAR(100),
    @EvaluacionPsicologica   VARCHAR(100),
    @Fotos                   VARCHAR(100),
    @VerificacionDomiciliaria VARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        BEGIN TRAN;

        -- 1) Actualizar datos personales en USUARIO
        UPDATE Usuario
        SET
            Nombres         = @Nombres,
            ApPaterno       = @ApPaterno,
            ApMaterno       = @ApMaterno,
            Dni             = @Dni,
            FechaNacimiento = @FechaNacimiento,
            Sexo            = @Sexo,
            Direccion       = @Direccion,
            Email           = @Email
        WHERE Id = @Id_Usuario;

        -- 2) Actualizar datos de PROFESOR (documentos y fecha ingreso)
        -- Usar NULLIF para convertir cadenas vacías a NULL, luego COALESCE para mantener valores existentes
        -- Si el parámetro es NULL o cadena vacía, mantiene el valor existente
        -- Si el parámetro tiene un valor, actualiza con ese valor
        UPDATE Profesor
        SET
            FechaIngreso            = COALESCE(@FechaIngreso, FechaIngreso),
            TituloProfesional       = COALESCE(NULLIF(@TituloProfesional, ''), TituloProfesional),
            Cv                      = COALESCE(NULLIF(@Cv, ''), Cv),
            EvaluacionPsicologica   = COALESCE(NULLIF(@EvaluacionPsicologica, ''), EvaluacionPsicologica),
            Fotos                   = COALESCE(NULLIF(@Fotos, ''), Fotos),
            VerificacionDomiciliaria = COALESCE(NULLIF(@VerificacionDomiciliaria, ''), VerificacionDomiciliaria)
        WHERE Id_Usuario = @Id_Usuario;

        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;

        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO


CREATE PROCEDURE [dbo].[modificar_profesor]
    @Id_Usuario INT,
    @FechaIngreso DATE,
    @TituloProfesional VARCHAR(100),
    @Cv VARCHAR(100),
    @EvaluacionPsicologica VARCHAR(100),
    @Fotos VARCHAR(100),
    @VerificacionDomiciliaria VARCHAR(100)
AS
BEGIN
    UPDATE Profesor
    SET FechaIngreso = @FechaIngreso,
        TituloProfesional = @TituloProfesional,
        Cv = @Cv,
        EvaluacionPsicologica = @EvaluacionPsicologica,
        Fotos = @Fotos,
        VerificacionDomiciliaria = @VerificacionDomiciliaria
    WHERE Id_Usuario = @Id_Usuario;
END
GO


