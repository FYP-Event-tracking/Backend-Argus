-- Create the userdb database
CREATE DATABASE userdb;
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

CREATE TABLE userlogin (
    userId NVARCHAR(191) PRIMARY KEY,
    userType NVARCHAR(191) NOT NULL,
    userPassword NVARCHAR(191) NOT NULL,
);
GO
