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

-- Copilot data load script completed successfully.
INSERT INTO Customers (CustomerName)
VALUES
('Acme Corp'),
('Contoso Industries'),
('Jim Test Customer'),
('Northwind Traders');

INSERT INTO Products (ProductName, Description, UnitPrice, IsActive)
VALUES
('Widget A', 'Basic widget', 9.99, 1),
('Widget B', 'Advanced widget', 19.99, 1),
('Gadget Pro', 'High-end gadget', 49.99, 1),
('Mini Tool', 'Compact utility tool', 5.49, 1);

SELECT CustomerId, CustomerName FROM Customers;
SELECT ProductId, ProductName FROM Products;
SELECT OrderId FROM Orders;

-- Orders for existing customers
INSERT INTO Orders (CustomerId, OrderDate, TotalAmount)
VALUES
(1011, GETUTCDATE(), 9.99),
(1012, GETUTCDATE(), 19.99),
(1013, GETUTCDATE(), 29.99),
(1014, GETUTCDATE(), 49.99),
(1015, GETUTCDATE(), 5.49),
(1016, GETUTCDATE(), 15.99),
(1017, GETUTCDATE(), 25.00);

-- Order details (after checking actual OrderId and ProductId values)
INSERT INTO OrderDetails (OrderId, ProductId, Quantity, UnitPrice, LineTotal)
VALUES
(1003, 1013, 2, 9.99, 19.98),   -- Widget A
(1004, 1014, 1, 19.99, 19.99),  -- Widget B
(1005, 1015, 1, 49.99, 49.99),  -- Gadget Pro
(1006, 1016, 3, 5.49, 16.47),   -- Mini Tool
(1007, 1010, 1, 29.99, 29.99),  -- Sorel
(1008, 1011, 2, 39.99, 79.98),  -- Belvedere vodka
(1009, 1012, 1, 59.99, 59.99);  -- Angels Envy Rye


