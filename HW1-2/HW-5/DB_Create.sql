IF DB_ID('Logistics') IS NULL
CREATE DATABASE Logistics;

GO

USE Logistics;

CREATE TABLE Cargo (
ID uniqueidentifier PRIMARY KEY NOT NULL,
InvoiceID uniqueidentifier,
WarehouseID  int,
VehicleID  int,
Volume float NOT NULL,
Weight int NOT NULL ,
Code varchar(255)
);

CREATE TABLE Invoice (
ID uniqueidentifier PRIMARY KEY NOT NULL,
RecipientAddress varchar(255),
SenderAddress varchar(255),
RecipientPhoneNumber varchar(255),
SenderPhoneNumber varchar(255)
);

CREATE TABLE Vehicle (
ID int PRIMARY KEY NOT NULL IDENTITY(1,1),
VehicleType varchar(10) NOT NULL CHECK (VehicleType IN('Car', 'Ship', 'Plane', 'Train')),
Number varchar(255),
MaxCargoWeightKg int NOT NULL,
MaxCargoWeightPnd float,
MaxCargoVolume float NOT NULL,
);

CREATE TABLE Warehouse (
ID int PRIMARY KEY NOT NULL IDENTITY(1,1),
);

GO

ALTER TABLE Cargo
ADD CONSTRAINT FK_CargoInvoice
FOREIGN KEY (InvoiceID) REFERENCES Logistics.dbo.Invoice(ID);

ALTER TABLE Cargo
ADD CONSTRAINT FK_CargoVehicle
FOREIGN KEY (VehicleID) REFERENCES Logistics.dbo.Vehicle(ID);

ALTER TABLE Cargo
ADD CONSTRAINT FK_CargoWarehouse
FOREIGN KEY (WarehouseID) REFERENCES Logistics.dbo.Warehouse(ID);

ALTER TABLE Cargo
ADD CONSTRAINT CK_AttributeValue
CHECK
((VehicleID IS NULL AND WarehouseID IS NOT NULL)
OR
(VehicleID IS NOT NULL AND WarehouseID IS NULL));
