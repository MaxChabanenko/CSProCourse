--INSERT INTO Logistics.dbo.Vehicle (ID, VehicleType, MaxCargoWeightKg, MaxCargoVolume)
--VALUES ('4E11AD6C-C1EE-4E91-974A-AB6DDC18D77C', 'Car', 100, 100);
--INSERT INTO Logistics.dbo.Vehicle (ID, VehicleType, MaxCargoWeightKg, MaxCargoVolume)
--VALUES ('6776A3ED-083C-412B-99C4-15C5EAADCCFC', 'Ship', 500, 500);
--INSERT INTO Logistics.dbo.Vehicle (ID, VehicleType, MaxCargoWeightKg, MaxCargoVolume)
--VALUES ('00F25467-420E-45F0-8048-DC3024C66EE4', 'Plane', 1000, 1000);
--INSERT INTO Logistics.dbo.Vehicle (ID, VehicleType, MaxCargoWeightKg, MaxCargoVolume)
--VALUES ('E8E1FDD3-3AC1-4471-835B-AC929A22EC33', 'Train', 2000, 2000);

--INSERT INTO Logistics.dbo.Warehouse (ID)
--VALUES ('8C7D073A-7F08-419D-BFE9-28B214F8ED11');
--INSERT INTO Logistics.dbo.Warehouse (ID)
--VALUES ('DFB56253-6201-4B83-8922-EE228AF6A670');

--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('B012A4A2-5FFC-49E8-A855-671DA030CE4F', '13887, Черкаська область, місто Черкаси, пров. Мельникова, 35', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','0663781770','0940151861');
--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('2ACA14A4-3071-462A-82D6-A112A33A93E3', '77514, Миколаївська область, місто Миколаїв, вул. Різницька, 31', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','+38(010)4903706','0940151861');
--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('15403B88-7518-4244-BD20-A9F1063FFFAD', '81669, Чернівецька область, місто Чернівці, вул. Пирогова, 02', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','0949634989','0940151861');
--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('0B5DC2BE-2705-4DF6-A399-C5E0FB4CDBE6', '91006, Волинська область, місто Луцьк, пров. Копиленка, 32', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','0911524520','0940151861');

--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('9A9C3005-BF28-4BB7-BDF1-CF2C9E57BB11', '06828, Дніпропетровська область, місто Дніпро, просп. І. Франкa, 13', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','380634933004','0940151861');
--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('716F4CE1-FBB8-467A-9EDF-EE99004A8976', '96420, Рівненська область, місто Рівне, вул. Б. Грінченка, 73', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','0984143100','0940151861');

--INSERT INTO Logistics.dbo.Invoice (ID, RecipientAddress, SenderAddress, RecipientPhoneNumber,SenderPhoneNumber)
--VALUES ('B00F5EDD-A8DD-4663-82C6-EF84165CBC1B', '03593, Сумська область, місто Суми, пров. Львівська, 54', '27689, Дніпропетровська область, місто Дніпро, вул. Урицького, 53','380944921414','0940151861');

INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), 'B012A4A2-5FFC-49E8-A855-671DA030CE4F','4E11AD6C-C1EE-4E91-974A-AB6DDC18D77C',NULL,100,100);
INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), '2ACA14A4-3071-462A-82D6-A112A33A93E3','6776A3ED-083C-412B-99C4-15C5EAADCCFC',NULL,100,100);
INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), '15403B88-7518-4244-BD20-A9F1063FFFAD','00F25467-420E-45F0-8048-DC3024C66EE4',NULL,100,100);
INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), '0B5DC2BE-2705-4DF6-A399-C5E0FB4CDBE6','E8E1FDD3-3AC1-4471-835B-AC929A22EC33',NULL,100,100);

INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), '9A9C3005-BF28-4BB7-BDF1-CF2C9E57BB11',NULL,'8C7D073A-7F08-419D-BFE9-28B214F8ED11',100,100);
INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), '716F4CE1-FBB8-467A-9EDF-EE99004A8976',NULL,'8C7D073A-7F08-419D-BFE9-28B214F8ED11',100,100);

INSERT INTO Logistics.dbo.Cargo(ID, InvoiceID, VehicleID, WarehouseID,Volume,Weight)
VALUES (NEWID(), 'B00F5EDD-A8DD-4663-82C6-EF84165CBC1B',NULL,'DFB56253-6201-4B83-8922-EE228AF6A670',100,100);





