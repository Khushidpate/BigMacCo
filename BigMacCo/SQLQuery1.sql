CREATE DATABASE BigMacCo;
GO

USE BigMacCo;
GO


CREATE TABLE Stock (
    StockCode INT IDENTITY(1,1) PRIMARY KEY,
    ItemName NVARCHAR(100),
    SupplierName NVARCHAR(100),
    UnitCost DECIMAL(18, 2),
    NumberRequired INT,
    CurrentStockLevel INT
);


INSERT INTO Stock (ItemName, SupplierName, UnitCost, NumberRequired, CurrentStockLevel) VALUES
( 'Pen', 'Staples', 2.00, 10, 5),
( 'Binder', 'Walmart', 5.00, 20, 2),
( 'Notepad', 'Staples', 1.00, 50, 10),
( 'Pencil', 'Walmart', 0.50, 20, 5),
( 'Bookmark', 'Staples', 1.00, 15, 10);


select * from Stock