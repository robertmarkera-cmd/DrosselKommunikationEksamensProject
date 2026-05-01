--CREATE DATABASE [DrosselDB];
--USE DrosselDB;


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
TemplateID Int PRIMARY KEY,
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
QuoteID Int PRIMARY KEY,
Date DateTime2 NOT NULL,
HourlyCost Int NOT NULL,
TotalPrice Float NOT NULL
);

CREATE TABLE FIXEDPRICEPRODUCT
(
FixedPriceProductID Int PRIMARY KEY,
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
VariablePriceProductID Int PRIMARY KEY,
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
