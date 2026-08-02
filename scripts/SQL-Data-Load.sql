INSERT INTO Customers (CustomerName)
VALUES
    ('Jim Corp'),
    ('Tim Incorporated'),
    ('Steve Industries');

INSERT INTO Products (ProductName, Description, UnitPrice, IsActive)
VALUES
    ('Sorel', 'Spicy liquor', 34.99, 1),
    ('Belvedere vodka', 'Rye vodka', 45.00, 1),
    ('Angels Envy Rye', 'Rye aged in rum casks', 79.99, 1);

INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
VALUES (1011, GETUTCDATE(), 9.99);

INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
VALUES (1003, 1011, 1, 9.99, 9.99);

DELETE FROM Customers;
DELETE FROM Products;
DELETE FROM Orders;
DELETE FROM OrderDetails;

