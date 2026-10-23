USE GastosDB;

CREATE TABLE Gastos (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Descripcion NVARCHAR(100) NOT NULL,
    Monto DECIMAL(10,2) NOT NULL,
    Categoria NVARCHAR(50) NOT NULL
);

INSERT INTO Gastos (Descripcion, Monto, Categoria) VALUES
('Almuerzo', 120.00, 'Comida'),
('Gasolina', 200.00, 'Transporte'),
('Internet', 50.50, 'Servicios');

SELECT * FROM Gastos;