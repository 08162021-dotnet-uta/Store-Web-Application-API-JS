USE StoreApplicationDB;
GO

SELECT *
FROM Customer.Customer;

SELECT *
FROM Store.StoreOrder;

SELECT *
FROM Store.Store;

SELECT *
FROM Store.Product;

SELECT *
FROM Store.StoreOrder;

SELECT *
FROM Store.OrderProduct;

/*INSERT INTO Customer.Customer ([Name])
VALUES ('Jeffrey Wright'), ('Michael Wright'), ('Catherine Wright'), ('Kyle Hill'), ('Ultimate Man');

INSERT INTO Store.Store ([Name], City, [State])
VALUES ('Best Buy', 'Lafayette', 'IN'), ('IKEA', 'Fishers', 'IN'), ('GameStop', 'Indianapolis', 'IN');

INSERT INTO Store.Product ([Name], [Description], Price)
VALUES ('Bluetooth Speaker', 'Premium speaker with 13 hours of battery life on a single charge.', 199.99),
('Lightning Cable', 'Charging cable for iPhone, iPad, and iPod Touch', 9.99),
('External Battery', 'It connects to your phone for charging. Enough for one full charge.', 19.99),
('Wall Charger', '2.1A USB-A wall charger for charging your devices', 14.99),
('Phone Case', 'Durable phone case for iPhone 12', 39.99), 
('Lagkapten / Tillsag', 'A strong and lightweight desk', 69.99),
('Markus', 'Office chair with armrests', 199.99),
('Drönjöns', 'Desk organizer', 9.99),
('Huvudroll', 'IKEA''s famous Swedish Meatballs in a bag', 9.99),
('Mario Kart 8 Deluxe', 'Fun kart racing game from Nintendo', 59.99),
('Nintendo Switch Lite', 'Fun and lightweight gaming system from Nintendo', 199.99),
('Pikachu Plush', 'A cute 24 inch plush of Pikachu', 39.99);

INSERT INTO Store.StoreInventory(StoreId, ProductId)
VALUES((SELECT StoreId FROM Store.Store WHERE [Name] = 'Best Buy'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Bluetooth Speaker')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'Best Buy'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Lightning Cable')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'Best Buy'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'External Battery')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'Best Buy'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Wall Charger')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'Best Buy'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Phone Case')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'IKEA'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Lagkapten / Tillsag')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'IKEA'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Markus')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'IKEA'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Drönjöns')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'IKEA'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Huvudroll')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'GameStop'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Mario Kart 8 Deluxe')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'GameStop'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Nintendo Switch Lite')),
((SELECT StoreId FROM Store.Store WHERE [Name] = 'GameStop'), (SELECT ProductId FROM Store.Product WHERE [Name] = 'Pikachu Plush'));

--Making an order (part 1)
INSERT INTO Store.StoreOrder (CustomerId, StoreId, OrderDate)
VALUES(
(SELECT CustomerId FROM Customer.Customer WHERE [Name] = 'Ultimate Man'),
(SELECT StoreId FROM Store.Store WHERE [Name] = 'Best Buy'),
GETDATE());

--Making an order (part 2)
INSERT INTO Store.OrderProduct (OrderId, ProductId, Quantity)
VALUES(
(SELECT OrderId FROM Store.StoreOrder WHERE OrderId = (SELECT MAX(OrderId) FROM Store.StoreOrder)),
(SELECT ProductId FROM Store.Product WHERE [Name] = 'Bluetooth Speaker'), 2
);

INSERT INTO Store.OrderProduct (OrderId, ProductId, Quantity)
VALUES(
(SELECT OrderId FROM Store.StoreOrder WHERE OrderId = (SELECT MAX(OrderId) FROM Store.StoreOrder)),
(SELECT ProductID FROM Store.Product WHERE [Name] = 'Lightning Cable'), 1
);*/

--Query Example
SELECT s.[Name] AS StoreName, p.[Name] as ProductName
FROM Store.Store AS s
INNER JOIN Store.StoreInventory AS si ON si.StoreId = s.StoreId
RIGHT JOIN Store.Product AS p ON si.ProductId = p.ProductId --Right join purpose: All prouct records have a product id.
WHERE p.Price = (SELECT MAX(Price) from Store.Product);

--Query Example 2
SELECT o.OrderId, s.[Name] AS StoreName, c.[Name] AS CustomerName, o.OrderDate
FROM Store.StoreOrder AS o
INNER JOIN Customer.Customer AS c ON o.CustomerId = c.CustomerId
INNER JOIN Store.Store AS s ON o.StoreId = s.StoreId
WHERE s.[Name] = 'Best Buy';

SELECT p.[Name] AS ProductName, op.Quantity, p.Price
FROM Store.OrderProduct AS op
INNER JOIN Store.Product AS p ON op.ProductId = p.ProductId
WHERE op.OrderID IN (SELECT o.OrderID FROM Store.StoreOrder AS o INNER JOIN Store.Store AS s ON o.StoreId = s.StoreId WHERE s.[Name] = 'Best Buy');

SELECT *
FROM Store.OrderProduct;

SELECT * FROM Store.OrderProduct WHERE ProductId = 1 AND OrderId = 1;

/*DELETE FROM Store.StoreOrder WHERE OrderId NOT IN (SELECT OrderId FROM Store.OrderProduct)*/