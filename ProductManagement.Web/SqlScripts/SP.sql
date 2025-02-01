-- Retrieve all products
CREATE PROCEDURE sp_GetAllProducts
AS
BEGIN
    SELECT * FROM Products;
END;
GO

-- Retrieve a product by its Id
CREATE PROCEDURE sp_GetProductById
    @Id INT
AS
BEGIN
    SELECT * FROM Products WHERE Id = @Id;
END;
GO

-- Insert a new product
CREATE PROCEDURE sp_InsertProduct
    @Name NVARCHAR(255),
    @Price DECIMAL(18,2),
    @Quantity INT
AS
BEGIN
    INSERT INTO Products (Name, Price, Quantity)
    VALUES (@Name, @Price, @Quantity);
END;
GO

-- Update an existing product
CREATE PROCEDURE sp_UpdateProduct
    @Id INT,
    @Name NVARCHAR(255),
    @Price DECIMAL(18,2),
    @Quantity INT
AS
BEGIN
    UPDATE Products
    SET Name = @Name, Price = @Price, Quantity = @Quantity
    WHERE Id = @Id;
END;
GO

-- Delete a product
CREATE PROCEDURE sp_DeleteProduct
    @Id INT
AS
BEGIN
    DELETE FROM Products WHERE Id = @Id;
END;
GO
