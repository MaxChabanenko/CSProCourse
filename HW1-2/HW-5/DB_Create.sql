--CREATE DATABASE Logistics;

CREATE TABLE Invoice (
ID uniqueidentifier PRIMARY KEY NOT NULL,
RecipientAddress varchar(255),
SenderAddress varchar(255),
RecipientPhoneNumber varchar(255),
SenderPhoneNumber varchar(255)
);

CREATE TABLE Vehicle (
ID uniqueidentifier PRIMARY KEY NOT NULL,
VehicleType varchar(10) NOT NULL CHECK (VehicleType IN('Car', 'Ship', 'Plane', 'Train')),
Number varchar(255),
MaxCargoWeightKg int NOT NULL,
MaxCargoWeightPnd float,
MaxCargoVolume float NOT NULL,
);

CREATE TABLE Warehouse (
ID uniqueidentifier PRIMARY KEY NOT NULL,
);

CREATE TABLE Cargo (
ID uniqueidentifier PRIMARY KEY NOT NULL,
InvoiceID uniqueidentifier FOREIGN KEY REFERENCES Logistics.dbo.Invoice(ID),
VehicleID uniqueidentifier FOREIGN KEY REFERENCES Logistics.dbo.Vehicle(ID) ,
WarehouseID uniqueidentifier FOREIGN KEY REFERENCES Logistics.dbo.Warehouse(ID) ,
Volume float NOT NULL,
Weight int NOT NULL ,
Code varchar(255)
);

ALTER TABLE Cargo
ADD CONSTRAINT CK_AttributeValue
CHECK
((VehicleID IS NULL AND WarehouseID IS NOT NULL)
OR
(VehicleID IS NOT NULL AND WarehouseID IS NULL));
