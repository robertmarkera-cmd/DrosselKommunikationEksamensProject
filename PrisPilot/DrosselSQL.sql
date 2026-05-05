CREATE DATABASE DrosselDB;
GO

USE DrosselDB;
GO


CREATE TABLE CUSTOMER
(
Cvr NvarChar(8) PRIMARY KEY,
CompanyName NvarChar(100) NOT NULL,
Email NvarChar(100) NOT NULL,
PhoneNumber NvarChar(20),
Address NvarChar(150),
Logo VARBINARY(max),
ContactPerson NvarChar(100) NOT NULL
);

CREATE TABLE TEMPLATE
(
TemplateID Int IDENTITY(1,1) PRIMARY KEY,
Introduction NvarChar(2000) NOT NULL,
TimeTable NvarChar(2000) NOT NULL,
AboutUs Bit NOT NULL,
Title NvarChar(100) NOT NULL,
DrosselMail NvarChar(150) NOT NULL,
DrosselPhoneNumber NvarChar(20) NOT NULL,
DrosselLogo VarBinary(max) NOT NULL
);

CREATE TABLE QUOTE
(
QuoteID Int IDENTITY(1,1) PRIMARY KEY,
Date DateTime2 NOT NULL,
HourlyCost Int NOT NULL,
TotalPrice Float NOT NULL,
Cvr NVarChar(8) NOT NULL FOREIGN KEY REFERENCES CUSTOMER(Cvr),
Template Int NOT NULL FOREIGN KEY REFERENCES TEMPLATE(TemplateID)
);

CREATE TABLE FIXEDPRICEPRODUCT
(
FixedPriceProductID Int IDENTITY(1,1) PRIMARY KEY,
Name NvarChar(80),
Description NvarChar(1200),
Price Int,
Frequency Int
);

CREATE TABLE FIXEDPRICEPRODUCT_QUOTE
(
FixedPriceProductID Int,
QuoteID Int,
CONSTRAINT PK_FixedPriceProductQuote 
		PRIMARY KEY (FixedPriceProductID, QuoteID),
CONSTRAINT FK_FixedPriceProductQuote_FixedPriceProduct 
		FOREIGN KEY (FixedPriceProductID)
		REFERENCES FixedPriceProduct (FixedPriceProductID),
CONSTRAINT FK_FixedPriceProductQuote_Quote 
		FOREIGN KEY (QuoteID)
		REFERENCES Quote(QuoteID)
);

CREATE TABLE VARIABLEPRICEPRODUCT
(
VariablePriceProductID Int IDENTITY(1,1) PRIMARY KEY,
Name NVarChar(80) NOT NULL,
Description NVarChar(1200) NOT NULL
);

CREATE TABLE TIMESPENT
(
QuoteID Int,
VariablePriceProductID int,
HoursUsed Int,
CONSTRAINT PK_QuoteVariablePriceProduct 
		PRIMARY KEY (QuoteID, VariablePriceProductID),
CONSTRAINT FK_QuoteVariablePriceProduct_Quote 
		FOREIGN KEY (QuoteID)
		REFERENCES Quote(QuoteID),
CONSTRAINT FK_QuoteVariablePriceProduct_VariablePriceProduct 
		FOREIGN KEY (VariablePriceProductID)
		REFERENCES VARIABLEPRICEPRODUCT (VariablePriceProductID)
);




CREATE PROCEDURE GetLatestHourlyCostByCvr
    @Cvr NVARCHAR(8)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP 1
        HourlyCost
    FROM QUOTE
    WHERE Cvr = @Cvr
    ORDER BY [Date] DESC;
END;

-- DUMMY DATA

INSERT INTO CUSTOMER
(Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson)
VALUES
('10000001', 'Nordic Solutions ApS', 'kontakt@nordic.dk', '20112233', 'Havnegade 1, København', NULL, 'Lars Jensen'),
('10000002', 'Blue Ocean Consult', 'info@blueocean.dk', '22334455', 'Strandvejen 10, Aarhus', NULL, 'Mette Sørensen'),
('10000003', 'GreenTech Systems', 'mail@greentech.dk', '33445566', 'Industrivej 5, Odense', NULL, 'Thomas Holm'),
('10000004', 'NextGen Software', 'support@nextgen.dk', '44556677', 'IT Parken 3, Aalborg', NULL, 'Julie Madsen'),
('10000005', 'Alpha Industries', 'kontakt@alpha.dk', '55667788', 'Fabriksvej 12, Esbjerg', NULL, 'Henrik Poulsen'),
('10000006', 'Beta Consulting', 'info@beta.dk', '66778899', 'Rådhuspladsen 2, Roskilde', NULL, 'Camilla Nielsen'),
('10000007', 'Delta Services', 'kontakt@delta.dk', '77889900', 'Servicevej 9, Vejle', NULL, 'Peter Kristensen'),
('10000008', 'Skyline IT', 'mail@skylineit.dk', '88990011', 'Cloud Street 7, Silkeborg', NULL, 'Nina Larsen'),
('10000009', 'Core Dynamics', 'info@core.dk', '99001122', 'Business Park 4, Herning', NULL, 'Anders Bach'),
('10000010', 'Future Works', 'kontakt@futureworks.dk', '10111213', 'Innovationsvej 8, Lyngby', NULL, 'Sofie Lund');


INSERT INTO TEMPLATE
(Introduction, TimeTable, AboutUs, Title, DrosselMail, DrosselPhoneNumber, DrosselLogo)
VALUES
('Intro tekst', 'Tidsplan 1', 1, 'Standard Quote', 'kontakt@drossel.dk', '12345678', 0x01),
('Intro tekst', 'Tidsplan 2', 1, 'Enterprise Quote', 'kontakt@drossel.dk', '12345678', 0x01);

INSERT INTO QUOTE
([Date], HourlyCost, TotalPrice, Cvr, Template)
VALUES
('2024-01-10', 850, 34000, '10000001', 1),
('2024-03-15', 900, 45000, '10000001', 1),

('2024-02-05', 750, 30000, '10000002', 1),
('2024-04-01', 800, 40000, '10000002', 2),

('2024-02-28', 1000, 60000, '10000003', 2),

('2024-01-18', 1100, 72000, '10000004', 1),

('2024-03-02', 700, 28000, '10000005', 1),

('2024-01-05', 680, 25000, '10000006', 1),
('2024-04-10', 720, 30000, '10000006', 2),

('2024-04-20', 950, 52000, '10000010', 1);

-- Test stored procedure

EXEC GetLatestHourlyCostByCvr @Cvr = '10000002';
