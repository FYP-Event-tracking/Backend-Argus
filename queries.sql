-- Create the userdb database
CREATE DATABASE userdb;
GO

-- Create the logdb database
CREATE DATABASE logdb;
GO

-- Switch to userdb database and create the userdata table
USE userdb;
GO

CREATE TABLE userdata (
    userId NVARCHAR(191) PRIMARY KEY,
    userName NVARCHAR(191) NOT NULL,
    userType NVARCHAR(191) NOT NULL,
    userAddress NVARCHAR(191) NOT NULL,
    userTelephone NVARCHAR(191) NOT NULL
);
GO

-- Switch to logdb database and create the logdata table
USE logdb;
GO

CREATE TABLE logdata (
    logId NVARCHAR(191) PRIMARY KEY,
    boxId NVARCHAR(191) NOT NULL,
    itemType NVARCHAR(191) NOT NULL,
    userId NVARCHAR(191) NOT NULL,
    totalCount INT NOT NULL,
    startTime DATETIME NOT NULL,
    endTime DATETIME NOT NULL,
    fullLogFile NVARCHAR(MAX) NOT NULL
);
GO
