-- Hämtar kundens namn och staden de bor i genom en JOIN mellan Customers och Address.

SELECT 
	Customers.FirstName, Customers.LastName, Address.City
FROM 
	Customers
INNER JOIN 
Address
ON Customers.AddressId = Address.AddressId;

-- Hämtar antal bokningar per kund med en GROUP BY och COUNT.

SELECT 
	Bookings.CustomerId, COUNT(Bookings.BookingId) AS TotalBookings
FROM 
	Bookings
GROUP BY 
	Bookings.CustomerId;



-- Hämtar alla kunder som har gjort en bokning med ArrivalDate efter ett visst datum, med hjälp av en SUBQUERY.
SELECT Customers.FirstName, Customers.LastName
FROM 
	Customers
WHERE 
	Customers.CustomerId IN (
    SELECT DISTINCT 
		CustomerId
    FROM 
		Bookings
    WHERE ArrivalDate > '2025-01-01'
);

-- Hämtar alla rum större eller lika med 20 m².
SELECT * 
	FROM 
	Rooms
WHERE Size >= 20;


-- Hämtar bokningar med kundinformation där ankomstdatum är inom 2025.
SELECT 
	Bookings.BookingId, Customers.FirstName, Customers.LastName
FROM 
	Bookings
	INNER JOIN Customers ON Bookings.CustomerId = Customers.CustomerId
WHERE Bookings.ArrivalDate BETWEEN '2025-01-01' AND '2025-12-31';


-- Hämtar alla rum som inte är bokade för ett visst datum med hjälp av en SUBQUERY.
SELECT 
	* 
FROM 
	Rooms
WHERE 
	RoomId NOT IN (
    SELECT DISTINCT RoomId
    FROM 
		Bookings
    WHERE ArrivalDate <= '2025-01-01' AND DepartureDate >= '2025-02-22'
);

-- Sorterar kunder alfabetiskt efter efternamn och förnamn.
SELECT 
	* 
FROM 
	Customers
ORDER BY LastName ASC, FirstName ASC;

-- Hämtar rumsnummer, pris och tillhörande bokning, sorterat efter pris i fallande ordning.
SELECT 
	Rooms.RoomNumber, Rooms.Price, Bookings.BookingId
FROM 
	Rooms
	LEFT JOIN 
	Bookings ON Rooms.BookingId = Bookings.BookingId
	ORDER BY Rooms.Price DESC;


-- Hämtar antal bokningar per kund.
SELECT 
	Customers.CustomerId, Customers.FirstName, Customers.LastName, 
COUNT
	(Bookings.BookingId) 
AS 
	BookingCount
FROM 
	Customers
	LEFT JOIN Bookings ON Customers.CustomerId = Bookings.CustomerId
GROUP BY Customers.CustomerId, Customers.FirstName, Customers.LastName;

--: Hämtar genomsnittlig storlek för rumstyper som är bokade under 2024.

SELECT 
	RoomType, AVG(Size) AS AverageSize
FROM 
	Rooms
WHERE 
	RoomId IN (
    SELECT DISTINCT RoomId
    FROM Bookings
    WHERE ArrivalDate >= '2024-01-01'
)

--Hämtar kunder och summan av deras fakturerade belopp, sorterat i fallande ordning efter intäkter.

SELECT 
	Customers.FirstName, Customers.LastName, 
	SUM(Invoices.TotalSum) 
AS 
	TotalySpent
FROM 
	Customers
	INNER JOIN Bookings ON Customers.CustomerId = Bookings.CustomerId
	INNER JOIN Invoices ON Bookings.BookingId = Invoices.BookingId
GROUP BY 
	Customers.FirstName, Customers.LastName
ORDER BY TotalySpent DESC;

