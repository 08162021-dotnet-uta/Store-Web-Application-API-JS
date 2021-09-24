EXEC sp_rename 'Customer.Customer.Name', 'fName', 'COLUMN';

ALTER TABLE Customer.Customer
ADD lName varchar(100);

SELECT SUBSTRING(fName,1,CHARINDEX(' ',fName)) AS firstName, LEN(SUBSTRING(fName,1,CHARINDEX(' ',fName))) AS [length]
FROM Customer.Customer;

SELECT LTRIM(SUBSTRING(fName,CHARINDEX(' ',fName),100)) as lastName, LEN(LTRIM(SUBSTRING(fName,CHARINDEX(' ',fName),100))) AS [length]
FROM Customer.Customer;

UPDATE Customer.Customer
SET lName = LTRIM(SUBSTRING(fName,CHARINDEX(' ',fName),100));

UPDATE Customer.Customer
SET fName = SUBSTRING(fName,1,CHARINDEX(' ',fName));

SELECT *
FROM Customer.Customer;

ALTER TABLE Store.Product
ADD Quantity smallint;

SELECT *
FROM Store.Product;

UPDATE Store.Product
SET Quantity = 3
WHERE Price > 99;

UPDATE Store.Product
SET Quantity = 10
WHERE Price < 100;

UPDATE Store.Product
SET Quantity = 35
WHERE Price < 30;

SELECT * FROM Customer.Customer WHERE FName = 'Jeffrey' AND LName = 'Wright';

SELECT * FROM Customer.Customer;

SELECT * FROM Store.StoreOrder;

SELECT * FROM Store.OrderProduct;