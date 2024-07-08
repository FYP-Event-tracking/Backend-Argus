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

INSERT INTO userdata (userId, userName, userType, userAddress, userTelephone)
VALUES ('User1', 'Employee', 'User', '123 User St', '123-456-7890');
GO

INSERT INTO userdata (userId, userName, userType, userAddress, userTelephone)
VALUES ('Admin1', 'Administrator', 'Admin', '456 Admin Rd', '987-654-3210');
GO

CREATE TABLE userlogin (
    userId NVARCHAR(191) PRIMARY KEY,
    userType NVARCHAR(191) NOT NULL,
    userPassword NVARCHAR(191) NOT NULL,
);
GO

INSERT INTO userlogin (userId, userType, userPassword)
VALUES ('User1', 'User', '123');
GO

INSERT INTO userlogin (userId, userType, userPassword)
VALUES ('Admin', 'Admin', '123');
GO
