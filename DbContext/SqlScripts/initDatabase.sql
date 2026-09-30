USE [sql-travelapp];
GO

CREATE OR ALTER VIEW vwDatabaseCounted AS
    SELECT(SELECT COUNT(*) FROM Attractions WHERE Seeded = 1) as NrSeededAttractions,
        (SELECT COUNT(*) FROM Attractions WHERE Seeded = 0) as NrUnseededAttractions,
        (SELECT COUNT(*) FROM Countries WHERE Seeded = 1) as NrSeededCountries,
        (SELECT COUNT(*) FROM Countries WHERE Seeded = 0) as NrUnseededCountries,
        (SELECT COUNT(*) FROM Cities WHERE Seeded = 1) as NrSeededCities,
        (SELECT COUNT(*) FROM Cities WHERE Seeded = 0) as NrUnseededCities,
        (SELECT COUNT(*) FROM Reviews  WHERE Seeded = 1) as NrSeededReviews,
        (SELECT COUNT(*) FROM Reviews  WHERE Seeded = 0) as NrUnseededReviews,
        (SELECT COUNT(*) FROM Users WHERE Seeded = 1) as NrSeededUsers,
        (SELECT COUNT(*) FROM Users WHERE Seeded = 0) as NrUnseededUsers;

GO

--Stored procedure for deleting alla seeded data 
CREATE OR ALTER PROC spDeleteAllSeed

    @seededParam BIT = 1
    AS 
    SET NOCOUNT ON;

    DELETE FROM Reviews WHERE Seeded = @seededParam;
    DELETE FROM Attractions WHERE Seeded = @seededParam;
    DELETE FROM Users WHERE Seeded = @seededParam;
    DELETE FROM Cities WHERE Seeded = @seededParam;
    DELETE FROM Countries WHERE Seeded = @seededParam;

    SELECT * FROM vwDataBaseCounted
    GO