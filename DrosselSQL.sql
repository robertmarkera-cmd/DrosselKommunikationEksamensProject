USE DrosselDB
GO

CREATE TABLE Costumer (
  Cvr int PRIMARY KEY NOT NULL,
  CompanyName NVarChar(100) NOT NULL,
  Email NVarChar(100) NOT NULL,
  PhoneNumber int,
  Address NVarChar(150),
  Logo varbinary(max),
  ContactPerson NVarChar(100),
  HourlyCost int
);