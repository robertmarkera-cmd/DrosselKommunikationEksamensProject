--CREATE DATABASE DrosselDB;
--GO

--USE DrosselDB;
--GO


--CREATE TABLE CUSTOMER
--(
--Cvr NvarChar(8) PRIMARY KEY,
--CompanyName NvarChar(100) NOT NULL,
--Email NvarChar(100) NOT NULL,
--PhoneNumber NvarChar(20),
--Address NvarChar(150),
--Logo VARBINARY(max),
--ContactPerson NvarChar(100) NOT NULL
--);

--CREATE TABLE TEMPLATE
--(
--TemplateID Int IDENTITY(1,1) PRIMARY KEY,
--Introduction NvarChar(2000) NOT NULL,
--TimeTable NvarChar(2000) NOT NULL,
--AboutUs Bit NOT NULL,
--Title NvarChar(100) NOT NULL,
--DrosselMail NvarChar(150) NOT NULL,
--DrosselPhoneNumber NvarChar(20) NOT NULL,
--DrosselLogo VarBinary(max) NOT NULL
--);

--CREATE TABLE QUOTE
--(
--QuoteID Int IDENTITY(1,1) PRIMARY KEY,
--Date DateTime2 NOT NULL,
--HourlyCost Int NOT NULL,
--TotalPrice Float NOT NULL,
--Cvr NVarChar(8) NOT NULL FOREIGN KEY REFERENCES CUSTOMER(Cvr),
--Template Int NOT NULL FOREIGN KEY REFERENCES TEMPLATE(TemplateID)
--);

--CREATE TABLE FIXEDPRICEPRODUCT
--(
--FixedPriceProductID Int IDENTITY(1,1) PRIMARY KEY,
--Name NvarChar(80),
--Description NvarChar(1200),
--Price Int,
--Frequency Int
--);

--CREATE TABLE FIXEDPRICEPRODUCT_QUOTE
--(
--FixedPriceProductID Int,
--QuoteID Int,
--CONSTRAINT PK_FixedPriceProductQuote 
--		PRIMARY KEY (FixedPriceProductID, QuoteID),
--CONSTRAINT FK_FixedPriceProductQuote_FixedPriceProduct 
--		FOREIGN KEY (FixedPriceProductID)
--		REFERENCES FixedPriceProduct (FixedPriceProductID),
--CONSTRAINT FK_FixedPriceProductQuote_Quote 
--		FOREIGN KEY (QuoteID)
--		REFERENCES Quote(QuoteID)
--);

--CREATE TABLE VARIABLEPRICEPRODUCT
--(
--VariablePriceProductID Int IDENTITY(1,1) PRIMARY KEY,
--Name NVarChar(80) NOT NULL,
--Description NVarChar(1200) NOT NULL
--);

--CREATE TABLE TIMESPENT
--(
--QuoteID Int,
--VariablePriceProductID int,
--HoursUsed Int,
--CONSTRAINT PK_QuoteVariablePriceProduct 
--		PRIMARY KEY (QuoteID, VariablePriceProductID),
--CONSTRAINT FK_QuoteVariablePriceProduct_Quote 
--		FOREIGN KEY (QuoteID)
--		REFERENCES Quote(QuoteID),
--CONSTRAINT FK_QuoteVariablePriceProduct_VariablePriceProduct 
--		FOREIGN KEY (VariablePriceProductID)
--		REFERENCES VARIABLEPRICEPRODUCT (VariablePriceProductID)
--);




--CREATE PROCEDURE GetLatestHourlyCostByCvr
--    @Cvr NVARCHAR(8)
--AS
--BEGIN
--    SET NOCOUNT ON;

--    SELECT TOP 1
--        HourlyCost
--    FROM QUOTE
--    WHERE Cvr = @Cvr
--    ORDER BY [Date] DESC;
--END;

 --DUMMY DATA

--INSERT INTO CUSTOMER
--(Cvr, CompanyName, Email, PhoneNumber, Address, Logo, ContactPerson)
--VALUES
--('10000001', 'Nordic Solutions ApS', 'kontakt@nordic.dk', '20112233', 'Havnegade 1, København', NULL, 'Lars Jensen'),
--('10000002', 'Blue Ocean Consult', 'info@blueocean.dk', '22334455', 'Strandvejen 10, Aarhus', NULL, 'Mette Sørensen'),
--('10000003', 'GreenTech Systems', 'mail@greentech.dk', '33445566', 'Industrivej 5, Odense', NULL, 'Thomas Holm'),
--('10000004', 'NextGen Software', 'support@nextgen.dk', '44556677', 'IT Parken 3, Aalborg', NULL, 'Julie Madsen'),
--('10000005', 'Alpha Industries', 'kontakt@alpha.dk', '55667788', 'Fabriksvej 12, Esbjerg', NULL, 'Henrik Poulsen'),
--('10000006', 'Beta Consulting', 'info@beta.dk', '66778899', 'Rådhuspladsen 2, Roskilde', NULL, 'Camilla Nielsen'),
--('10000007', 'Delta Services', 'kontakt@delta.dk', '77889900', 'Servicevej 9, Vejle', NULL, 'Peter Kristensen'),
--('10000008', 'Skyline IT', 'mail@skylineit.dk', '88990011', 'Cloud Street 7, Silkeborg', NULL, 'Nina Larsen'),
--('10000009', 'Core Dynamics', 'info@core.dk', '99001122', 'Business Park 4, Herning', NULL, 'Anders Bach'),
--('10000010', 'Future Works', 'kontakt@futureworks.dk', '10111213', 'Innovationsvej 8, Lyngby', NULL, 'Sofie Lund');


--INSERT INTO TEMPLATE
--(Introduction, TimeTable, AboutUs, Title, DrosselMail, DrosselPhoneNumber, DrosselLogo)
--VALUES
--('Intro tekst', 'Tidsplan 1', 1, 'Standard Quote', 'kontakt@drossel.dk', '12345678', 0x01),
--('Intro tekst', 'Tidsplan 2', 1, 'Enterprise Quote', 'kontakt@drossel.dk', '12345678', 0x01);

--INSERT INTO QUOTE
--([Date], HourlyCost, TotalPrice, Cvr, Template)
--VALUES
--('2024-01-10', 850, 34000, '10000001', 1),
--('2024-03-15', 900, 45000, '10000001', 1),

--('2024-02-05', 750, 30000, '10000002', 1),
--('2024-04-01', 800, 40000, '10000002', 2),

--('2024-02-28', 1000, 60000, '10000003', 2),

--('2024-01-18', 1100, 72000, '10000004', 1),

--('2024-03-02', 700, 28000, '10000005', 1),

--('2024-01-05', 680, 25000, '10000006', 1),
--('2024-04-10', 720, 30000, '10000006', 2),

--('2024-04-20', 950, 52000, '10000010', 1);

--INSERT INTO VARIABLEPRICEPRODUCT
--(Name, Description)
--VALUES
--('Hjælp til LinkedIn for virksomheder', 'Skal du have hjælp til LinkedIn? Måske har du en virksomhedsprofil på LinkedIn og overvejer, hvordan man bruger LinkedIn optimalt. Måske bruger du allerede LinkedIn, men kunne ønske dig en decideret LinkedIn-strategi, så du ikke arbejder i blinde. Eller måske overvejer du, om din virksomhed overhovedet skal være på LinkedIn. Uanset hvad kan vi hjælpe dig med LinkedIn.
--I Drossel Kommunikation har vi flere kunder, hvor vi står for fuld varetagelse, dvs. vi skriver tekster, tager billeder, poster og monitorerer. Vi har altså stor erfaring med virksomhedsprofiler på LinkedIn, og derfor kan vi hjælpe – om det er med gode råd eller fuld varetagelse.'),

--('Krisekommunikation','Når krisen rammer, er det ofte for sent at begynde at planlægge, hvordan man bedst håndterer kommunikationen. Mange virksomheder undervurderer, hvor vigtigt det er at have en strategi for krisekommunikation, før problemerne opstår. Uden en plan risikerer man at miste kontrollen over fortællingen, hvilket kan skade både omdømme og forretning.
--Hos Drossel Kommunikation forstår vi, hvordan man navigerer i komplekse kommunikationsudfordringer, både internt og eksternt, og vi kan hjælpe jer med at være forberedte, inden krisen opstår. Uanset om det handler om intern krisekommunikation, håndtering af medier eller kommunikation på sociale medier, sørger vi for, at din virksomhed står stærkt i en svær situation.'),

--('Kursus i Canva','Canva er på alles læber - endnu en grund til, at vi laver et Canva-kursus. Og hvis du tænker: Hvad er Canva?, så er det det superintuitive værktøj, hvor virksomheder nemt kan opsætte præsentationsmateriale. Mega simpelt.
--Her kan du lægge brandfarver og logoer ind, lave templates, opsætte flyers, sociale opslag, rapporter og alt muligt andet, så det hele spiller visuelt.
--Men hvad hjælper alt det, hvis du ikke kan få det til at fungere optimalt? Det er her, vores kursus i Canva kommer ind i billedet.'),
--('Medietræning','Skal du indimellem udtale dig til medierne? Det kan være nervepirrende, når en journalist stiller spørgsmål, og særligt når det budskab, man gerne vil have ud, ikke bliver leveret så overbevisende. Med træning til medierne og den rette viden kan du slippe frygten og gå til et interview med overskud og bedre chance for at få dit budskab tydeligt igennem.'),
--('Pressemeddelelser','Ønsker du at øge kendskabet til din virksomhed eller en kommende begivenhed?
--En velskrevet og skarpt vinklet pressemeddelelse sendt til de rette medier kan være et effektivt redskab. Hvor annoncer ofte skaber en øjeblikkelig effekt, er presseomtale en del af en langsigtet strategi, der opbygger troværdighed og kendskab.'),
--('Miljømarkedsføring','Vi kan hjælpe dig, så du undgår greenwashing og greenhushing! 
--Miljømarkedsføring er en vigtig del af at drive en ansvarlig virksomhed i dag. Men hvordan sikrer du, at din kommunikation om bæredygtighed og miljøindsatser er både troværdig og lovlig? Mange virksomheder ender enten med at overdrive deres grønne budskaber (greenwashing) eller helt undlade at kommunikere dem (greenhushing) af frygt for at sige noget forkert.
--Vi hjælper jer med at navigere i reglerne og kommunikere jeres miljømæssige initiativer på en gennemsigtig og strategisk måde. Vi laver ikke annoncer, men vi hjælper jer med at formidle jeres grønne markedsføring klart og effektivt, så jeres budskaber skaber værdi og engagement hos kunder, investorer og samarbejdspartnere.'),
--('Taleskrivning','Skal du i gang med at skrive en tale? Måske skal du skrive en tale til alle dine ansatte? Måske skal du tale til medierne? Måske skal du ud at holde et oplæg eller i gang med den store lejlighedstale? 
--Uanset hvilken tale du skal skrive, kan vi hjælpe dig i Drossel Kommunikation. Hos os kan du både få skrevet en god og overbevisende tale og få rådgivning i, hvordan du bedst holder talen og udnytter din stemmes fulde potentiale. Desuden har vi en stor forståelse for de forskellige steder og situationer, en tale skal holdes i, og vi kan hjælpe dig med at tilpasse din tale til både stedet, publikum og situationen.'),
--('Tekstforfatter casehistorier','Det kan være svært at skrive for sin egen virksomhed. En ting er at finde de rette formuleringer, men som intern kan man nemt blive blind for, hvad der er svært at forstå for eksterne. 
--I stedet for at give op, kan du få stor værdi af at finde en dygtig tekstforfatter. På den måde kan du holde dine kunder og interessenter opdateret om, hvad der rører sig i din virksomhed, og dele din faglighed på en klar og professionel måde.
--Casehistorier og nyheder på din hjemmeside er en effektiv måde at dele viden og indsigter og fortælle, hvad der gør din virksomhed unik. Gode, velskrevede historier er med til at skabe tillid og troværdighed, samtidig med at de viser verden, hvem I er.');


--INSERT INTO FIXEDPRICEPRODUCT
--(Name, Description, Price, Frequency)
--VALUES
--('Fast varetagelse af LinkedIn, 1 opslag pr. uge', 'Ved fast varetagelse af jeres LinkedIn gør vi det simpelt og ligetil for jer at være aktive
--på LinkedIn på ugentlig basis.
--Ved ‘fast varetagelse af LinkedIn’ menes, at vi står for at:
--Opdatere ‘about’ -teksten
--Skifte cover-og profilbillede efter behov
--Skrive 1 opslag pr. uge alt efter den aftalte model. Dette vil nogle gange
--inkludere et kort interview med en ansat, en case eller anden relevant person.
--Tage/udvælge billeder til opslag eller koordinere billeder med jer.
--Moderere, dvs. svare på kommentarer, kommentere andres opslag og videresende
--henvendelser til vores kontaktperson hos jer.
--Aktivt lave indsatser på udvalgte områder, eksempelvis opsøgende kommentarer og
--likes på udvalgte virksomhedsprofiler.
--Vi skriver og planlægger alt, men er også afhængige af jeres inputs. I er eksperterne og
--har den særlige viden, vi skal kommunikere om. Derfor sendes alt til gennemsyn og intet
--publiceres uden, at det er godkendt af jer.', 7500, 1),

--('Fast varetagelse af LinkedIn, 2 opslag pr. uge', 'Ved fast varetagelse af jeres LinkedIn gør vi det simpelt og ligetil for jer at være aktive
--på LinkedIn på ugentlig basis.
--Ved ‘fast varetagelse af LinkedIn’ menes, at vi står for at:
--Opdatere ‘about’ -teksten
--Skifte cover-og profilbillede efter behov
--Skrive 2 opslag pr. uge alt efter den aftalte model. Dette vil nogle gange
--inkludere et kort interview med en ansat, en case eller anden relevant person.
--Tage/udvælge billeder til opslag eller koordinere billeder med jer.
--Moderere, dvs. svare på kommentarer, kommentere andres opslag og videresende
--henvendelser til vores kontaktperson hos jer.
--Aktivt lave indsatser på udvalgte områder, eksempelvis opsøgende kommentarer og
--likes på udvalgte virksomhedsprofiler.
--Vi skriver og planlægger alt, men er også afhængige af jeres inputs. I er eksperterne og
--har den særlige viden, vi skal kommunikere om. Derfor sendes alt til gennemsyn og intet
--publiceres uden, at det er godkendt af jer.', 10500, 2),

--('Kernefortælling', 'Ved at analysere jeres virksomhed og gennemføre interviews med både jer og udvalgte
--kunder, potentielle kunder, konsulenthuse og medarbejdere, kan vi identificere den
--kernefortælling, der præcist afspejler jer, og der hvor I gerne vil hen.
--Kernefortællingen vil være et redskab, I kan bruge, når I taler med eksisterende og nye
--kunder, og når vi skal producere indhold til jeres forskellige kommunikationskanaler.
--Den vil være en retningslinje, så kun den kommunikation, der harmonerer med
--fortællingen, kommer ud. Vi vil udarbejde cirka én side med tekst med selve
--kernefortællingen i flere længder, argumentationen bag samt kerneord. Og
--kernefortællingen danner grundlag for strategien på LinkedIn, hjemmeside og
--nyhedsbrev.
--Dette vil indebære:
--Interview med ledelsen
--Interview med medarbejdere
--Interview med kunder/facility managers
--Interview med konsulenthus
--Analyse af den indsamlede data
--Produktion af tekst til kernefortælling
--Rettelser og tilpasning', 18950, 1);

-- Test stored procedure
--EXEC GetLatestHourlyCostByCvr @Cvr = '10000002';
