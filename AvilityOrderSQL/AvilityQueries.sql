--a. Write a SQL query that will produce a reverse-sorted list (alphabetically by name) of customers (first and last names) whose last name begins with the letter ‘S.’
SELECT
	c.FirstName,
	c.LastName
FROM
	dbo.Customer c WITH(NOLOCK)
WHERE
	c.LastName LIKE 's%'
ORDER BY
	c.FirstName ASC;

-- b. Write a SQL query that will show the total value of all orders each customer has placed in the past six months. Any customer without any orders should show a $0 value.
SELECT
	c.FirstName,
	c.LastName,
	o.OrderDate,
	CASE 
		WHEN o.OrderDate > DATEADD(m, -6, GetDate()) AND o.OrderDate <= GETDATE() THEN ISNULL(ol.Quantity,0)
		ELSE 0
	END AS Quantity,
	CASE 
		WHEN o.OrderDate > DATEADD(m, -6, GetDate()) AND o.OrderDate <= GETDATE() THEN ISNULL(ol.Cost, 0)
		ELSE 0
	END AS CostPerUnit,
	CASE 
		WHEN o.OrderDate > DATEADD(m, -6, GetDate()) AND o.OrderDate <= GETDATE() THEN ISNULL(ol.Quantity * ol.Cost, 0)
		ELSE 0
	END AS TotalCost
FROM
	Customer c WITH(NOLOCK)
		LEFT JOIN [Order] o WITH(NOLOCK)
	ON c.CustID = o.CustomerID
		LEFT JOIN OrderLine ol WITH(NOLOCK)
	ON o.OrderID = ol.OrdID;

-- c. Amend the query from the previous question to only show those customers who have a total order value of more than $100 and less than $500 in the past six months.
SELECT FirstName, LastName, OrderDate, Quantity, CostPerUnit, TotalCost FROM(
SELECT
	c.FirstName,
	c.LastName,
	o.OrderDate,
	CASE 
		WHEN o.OrderDate > DATEADD(m, -6, GetDate()) AND o.OrderDate <= GETDATE() THEN ISNULL(ol.Quantity,0)
		ELSE 0
	END AS Quantity,
	CASE 
		WHEN o.OrderDate > DATEADD(m, -6, GetDate()) AND o.OrderDate <= GETDATE() THEN ISNULL(ol.Cost, 0)
		ELSE 0
	END AS CostPerUnit,
	CASE 
		WHEN o.OrderDate > DATEADD(m, -6, GetDate()) AND o.OrderDate <= GETDATE() THEN ISNULL(ol.Quantity * ol.Cost, 0)
		ELSE 0
	END AS TotalCost
FROM
	Customer c WITH(NOLOCK)
		LEFT JOIN [Order] o WITH(NOLOCK)
	ON c.CustID = o.CustomerID
		LEFT JOIN OrderLine ol WITH(NOLOCK)
	ON o.OrderID = ol.OrdID) as x
WHERE
	TotalCost > 100 AND TotalCost < 500;