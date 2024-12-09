CREATE DATABASE HccCafeteria;
USE HccCafeteria

CREATE TABLE Tb_HccMesas (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Lugares INT NOT NULL,
    Disponible BIT NOT NULL DEFAULT 1 -- 1 = Disponible, 0 = Ocupada
);
--Consulta mesas
SELECT * FROM Tb_HccMesas;

CREATE TABLE Tb_HccProductos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Nombre NVARCHAR(100) NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    Activo BIT NOT NULL DEFAULT 1 -- 1 = Activo, 0 = Borrado lógico
);
-- Consulta Productos
SELECT * FROM Tb_HccProductos;

CREATE TABLE Tb_HccOrdenes (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    MesaId INT NOT NULL FOREIGN KEY REFERENCES Tb_HccMesas(Id),
    Estatus INT NOT NULL CHECK (Estatus IN (1, 2, 3, 4, 5)), -- 1 = Nueva, 2 = Recibida, ...
    FechaCreacion DATETIME NOT NULL DEFAULT GETDATE()
);
--Consulta Ordenes

SELECT * FROM Tb_HccOrdenes;

CREATE TABLE Tb_HccDetallesOrden (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrdenId INT NOT NULL FOREIGN KEY REFERENCES Tb_HccOrdenes(Id),
    ProductoId INT NOT NULL FOREIGN KEY REFERENCES Tb_HccProductos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad > 0)
);

-- Consulta Detalles Orden
SELECT * FROM Tb_HccDetallesOrden;

CREATE TABLE Tb_HccAlmacen (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ProductoId INT NOT NULL FOREIGN KEY REFERENCES Tb_HccProductos(Id),
    Cantidad INT NOT NULL CHECK (Cantidad >= 0)
);

-- Consulta almacen
	SELECT * FROM Tb_HccAlmacen;

INSERT INTO Tb_HccMesas (Lugares, Disponible)
VALUES
    (4, 1), -- Mesa con 4 lugares disponible
    (2, 1), -- Mesa con 2 lugares disponible
    (6, 0);

	INSERT INTO Tb_HccProductos (Nombre, Precio, Activo)
VALUES
    ('Café Americano', 35.00, 1),
    ('Capuchino', 45.00, 1),
    ('Pastel de Chocolate', 50.00, 1);


	-- Inserts de prueba 
	INSERT INTO Tb_HccAlmacen (ProductoId, Cantidad)
VALUES
    (1, 50), -- 50 unidades de Café Americano
    (2, 30), -- 30 unidades de Capuchino
    (3, 20); -- 20 unidades de Pastel de Chocolate

	INSERT INTO Tb_HccOrdenes (MesaId, Estatus)
VALUES
    (1, 1), -- Orden en mesa 1, estado: Nueva
    (2, 2); -- Orden en mesa 2, estado: Recibida

	INSERT INTO Tb_HccDetallesOrden (OrdenId, ProductoId, Cantidad)
VALUES
    (1, 1, 2), -- 2 Café Americano en la Orden 1
    (1, 2, 1), -- 1 Capuchino en la Orden 1
    (2, 3, 2); -- 2 Pasteles de Chocolate en la Orden 2


	
	

	
	