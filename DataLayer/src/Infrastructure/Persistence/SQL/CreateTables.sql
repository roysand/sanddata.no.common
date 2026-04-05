-- =============================================================================
-- CreateTables.sql
-- Azure SQL Server - Idempotent table creation script
-- Strategy: DROP table if it exists, then CREATE
-- Drop order respects foreign key dependencies (children first, parents last)
-- =============================================================================

PRINT 'Starting table drop sequence (children first)...';

-- -------------------------------------------------------------------------
-- 1. Drop junction / child tables that reference multiple parents
-- -------------------------------------------------------------------------

IF OBJECT_ID('dbo.appuser_role', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.appuser_role...';
    DROP TABLE dbo.appuser_role;
END

IF OBJECT_ID('dbo.appuser_location', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.appuser_location...';
    DROP TABLE dbo.appuser_location;
END

-- -------------------------------------------------------------------------
-- 2. Drop tables that depend on Account / ApiKey / Location
-- -------------------------------------------------------------------------

IF OBJECT_ID('dbo.AccountContact', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.AccountContact...';
    DROP TABLE dbo.AccountContact;
END

IF OBJECT_ID('dbo.detail', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.detail...';
    DROP TABLE dbo.detail;
END

IF OBJECT_ID('dbo.minute', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.minute...';
    DROP TABLE dbo.minute;
END

IF OBJECT_ID('dbo.hour', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.hour...';
    DROP TABLE dbo.hour;
END

IF OBJECT_ID('dbo.day', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.day...';
    DROP TABLE dbo.day;
END

IF OBJECT_ID('dbo.price_detail', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.price_detail...';
    DROP TABLE dbo.price_detail;
END

IF OBJECT_ID('dbo.price', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.price...';
    DROP TABLE dbo.price;
END

-- -------------------------------------------------------------------------
-- 3. Drop tables that depend on ApiKey
-- -------------------------------------------------------------------------

IF OBJECT_ID('dbo.Account', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.Account...';
    DROP TABLE dbo.Account;
END

IF OBJECT_ID('dbo.Location', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.Location...';
    DROP TABLE dbo.Location;
END

-- -------------------------------------------------------------------------
-- 4. Drop root / standalone tables
-- -------------------------------------------------------------------------

IF OBJECT_ID('dbo.AppUser', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.AppUser...';
    DROP TABLE dbo.AppUser;
END

IF OBJECT_ID('dbo.Role', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.Role...';
    DROP TABLE dbo.Role;
END

IF OBJECT_ID('dbo.ApiKey', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.ApiKey...';
    DROP TABLE dbo.ApiKey;
END

IF OBJECT_ID('dbo.exchange_rate', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.exchange_rate...';
    DROP TABLE dbo.exchange_rate;
END

IF OBJECT_ID('dbo.raw', 'U') IS NOT NULL
BEGIN
    PRINT 'Dropping dbo.raw...';
    DROP TABLE dbo.raw;
END

PRINT 'All tables dropped. Starting table creation sequence...';
GO

-- =============================================================================
-- CREATE TABLES (parents first, then children)
-- =============================================================================

-- -------------------------------------------------------------------------
-- ApiKey  (no dependencies)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.ApiKey...';
CREATE TABLE dbo.ApiKey
(
    Id          UNIQUEIDENTIFIER NOT NULL,
    [Key]       VARCHAR(40)      NOT NULL,
    Admin       BIT              NOT NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT PK_ApiKey PRIMARY KEY (Id)
);
GO

-- -------------------------------------------------------------------------
-- AppUser  (no dependencies)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.AppUser...';
CREATE TABLE dbo.AppUser
(
    Id                     UNIQUEIDENTIFIER NOT NULL,
    IsActive               BIT              NOT NULL,
    FirstName              NVARCHAR(100)    NOT NULL,
    LastName               NVARCHAR(100)    NOT NULL,
    HashedPassword         NVARCHAR(MAX)    NOT NULL,
    Email                  NVARCHAR(100)    NOT NULL,
    RefreshToken           NVARCHAR(50)     NULL,
    RefreshTokenExpiryTime DATETIME2        NULL,
    ChangedDate            DATETIME2        NOT NULL,
    CreatedDate            DATETIME2        NULL,
    CONSTRAINT PK_AppUser PRIMARY KEY (Id)
);
GO

-- -------------------------------------------------------------------------
-- Role  (no dependencies)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.Role...';
CREATE TABLE dbo.Role
(
    Id              UNIQUEIDENTIFIER NOT NULL,
    RoleName        VARCHAR(50)      NOT NULL,
    RoleDescription VARCHAR(250)     NOT NULL,
    ChangedDate     DATETIME2        NOT NULL,
    CreatedDate     DATETIME2        NULL,
    CONSTRAINT PK_Role PRIMARY KEY (Id)
);
GO

-- -------------------------------------------------------------------------
-- Account  (depends on ApiKey)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.Account...';
CREATE TABLE dbo.Account
(
    Id          UNIQUEIDENTIFIER NOT NULL,
    AccountName VARCHAR(30)      NOT NULL,
    Active      BIT              NOT NULL,
    ApiKeyId    UNIQUEIDENTIFIER NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT PK_Account PRIMARY KEY (Id),
    CONSTRAINT FK_Account_ApiKey FOREIGN KEY (ApiKeyId) REFERENCES dbo.ApiKey (Id)
);
GO

-- -------------------------------------------------------------------------
-- Location  (depends on ApiKey)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.Location...';
CREATE TABLE dbo.Location
(
    Id              UNIQUEIDENTIFIER NOT NULL,
    IsActive        BIT              NOT NULL,
    LocationName    VARCHAR(100)     NOT NULL,
    LocationAddress VARCHAR(100)     NULL,
    SerialNumber    VARCHAR(20)      NULL,
    ApiKeyId        UNIQUEIDENTIFIER NULL,
    ChangedDate     DATETIME2        NOT NULL,
    CreatedDate     DATETIME2        NULL,
    CONSTRAINT PK_Location_Id PRIMARY KEY (Id),
    CONSTRAINT FK_Location_ApiKey FOREIGN KEY (ApiKeyId) REFERENCES dbo.ApiKey (Id)
);
GO

-- -------------------------------------------------------------------------
-- AccountContact  (depends on Account)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.AccountContact...';
CREATE TABLE dbo.AccountContact
(
    Id                 UNIQUEIDENTIFIER NOT NULL,
    AccountId          UNIQUEIDENTIFIER NOT NULL,
    ContactFirstName   VARCHAR(100)     NOT NULL,
    ContactLastName    VARCHAR(100)     NOT NULL,
    ContactEmail       VARCHAR(100)     NOT NULL,
    ContactMobilePhone VARCHAR(20)      NULL,
    ChangedDate        DATETIME2        NOT NULL,
    CreatedDate        DATETIME2        NULL,
    CONSTRAINT PK_AccountContact PRIMARY KEY (Id),
    CONSTRAINT FK_AccountContact_Account
        FOREIGN KEY (AccountId) REFERENCES dbo.Account (Id)
);
GO

-- -------------------------------------------------------------------------
-- appuser_location  (depends on AppUser, Location)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.appuser_location...';
CREATE TABLE dbo.appuser_location
(
    AppUserId   UNIQUEIDENTIFIER NOT NULL,
    LocationId  UNIQUEIDENTIFIER NOT NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT appuser_location_pk PRIMARY KEY (AppUserId, LocationId),
    CONSTRAINT FK_appuser_location_AppUser
        FOREIGN KEY (AppUserId) REFERENCES dbo.AppUser (Id),
    CONSTRAINT FK_appuser_location_Location
        FOREIGN KEY (LocationId) REFERENCES dbo.Location (Id)
);
GO

-- -------------------------------------------------------------------------
-- appuser_role  (depends on AppUser, Role)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.appuser_role...';
CREATE TABLE dbo.appuser_role
(
    AppUserId   UNIQUEIDENTIFIER NOT NULL,
    RoleId      UNIQUEIDENTIFIER NOT NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT appuser_role_pk PRIMARY KEY (AppUserId, RoleId),
    CONSTRAINT FK_appuser_role
        FOREIGN KEY (AppUserId) REFERENCES dbo.AppUser (Id) ON DELETE CASCADE,
    CONSTRAINT FK_role_appuser
        FOREIGN KEY (RoleId) REFERENCES dbo.Role (Id) ON DELETE CASCADE
);
GO

-- -------------------------------------------------------------------------
-- detail  (depends on Location)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.detail...';
CREATE TABLE dbo.detail
(
    Id          UNIQUEIDENTIFIER NOT NULL,
    MeasurementId UNIQUEIDENTIFIER NOT NULL,
    TimeStamp   DATETIME2(3)     NOT NULL,
    LocationId  UNIQUEIDENTIFIER NOT NULL,
    Name        VARCHAR(30)      NULL,
    ObisCodeId  INT              NOT NULL,
    ObisCode    VARCHAR(50)      NULL,
    Unit        VARCHAR(5)       NULL,
    ValueStr    VARCHAR(100)     NULL,
    ValueNum    DECIMAL(19, 5)   NOT NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT detail_pk PRIMARY KEY (Id),
    CONSTRAINT FK_Detail_Location
        FOREIGN KEY (LocationId) REFERENCES dbo.Location (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Detail_TimeStamp
    ON dbo.detail (TimeStamp);

CREATE INDEX nci_wi_detail_99F2D155AA826D3C5CC127D4720BFE87
    ON dbo.detail (ObisCodeId, TimeStamp);
GO

-- -------------------------------------------------------------------------
-- minute  (depends on Location)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.minute...';
CREATE TABLE dbo.minute
(
    TimeStamp   DATETIME2(3)     NOT NULL,
    LocationId  UNIQUEIDENTIFIER NOT NULL,
    Unit        VARCHAR(5)       NULL,
    ValueNum    DECIMAL(19, 5)   NULL,
    Count       SMALLINT         NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT minute_pk PRIMARY KEY (TimeStamp, LocationId),
    CONSTRAINT FK_Minute_Location
        FOREIGN KEY (LocationId) REFERENCES dbo.Location (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Minute_TimeStamp
    ON dbo.minute (TimeStamp);
GO

-- -------------------------------------------------------------------------
-- hour  (depends on Location)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.hour...';
CREATE TABLE dbo.hour
(
    TimeStamp   DATETIME2(3)     NOT NULL,
    LocationId  UNIQUEIDENTIFIER NOT NULL,
    Unit        VARCHAR(5)       NULL,
    ValueNum    DECIMAL(19, 5)   NULL,
    Count       SMALLINT         NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT hour_pk PRIMARY KEY (TimeStamp, LocationId),
    CONSTRAINT FK_Hour_Location
        FOREIGN KEY (LocationId) REFERENCES dbo.Location (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Hour_TimeStamp
    ON dbo.hour (TimeStamp);
GO

-- -------------------------------------------------------------------------
-- day  (depends on Location)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.day...';
CREATE TABLE dbo.day
(
    Date        DATETIME2(3)     NOT NULL,
    LocationId  UNIQUEIDENTIFIER NOT NULL,
    Unit        VARCHAR(5)       NULL,
    ValueNum    DECIMAL(19, 5)   NULL,
    Count       SMALLINT         NULL,
    PriceNOK    DECIMAL(19, 5)   NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT day_pk PRIMARY KEY (Date, LocationId),
    CONSTRAINT FK_Day_Location
        FOREIGN KEY (LocationId) REFERENCES dbo.Location (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Day_Date
    ON dbo.day (Date);
GO

-- -------------------------------------------------------------------------
-- price  (depends on Location)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.price...';
CREATE TABLE dbo.price
(
    Id          UNIQUEIDENTIFIER NOT NULL,
    PricePeriod DATETIME2(3)     NOT NULL,
    Modified    DATETIME2(3)     NOT NULL,
    LocationId  UNIQUEIDENTIFIER NOT NULL,
    Currency    VARCHAR(5)       NOT NULL,
    Unit        VARCHAR(5)       NOT NULL,
    Average     DECIMAL(19, 5)   NULL,
    [Max]       DECIMAL(19, 5)   NULL,
    [Min]       DECIMAL(19, 5)   NOT NULL,
    InDomain    VARCHAR(20)      NOT NULL,
    OutDomain   VARCHAR(20)      NOT NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT price_pk PRIMARY KEY NONCLUSTERED (Id),
    CONSTRAINT FK_Price_Location
        FOREIGN KEY (LocationId) REFERENCES dbo.Location (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Price_Id
    ON dbo.price (Id);

CREATE UNIQUE INDEX IX_Price_PricePeriod
    ON dbo.price (PricePeriod);
GO

-- -------------------------------------------------------------------------
-- price_detail  (depends on price)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.price_detail...';
CREATE TABLE dbo.price_detail
(
    Id          UNIQUEIDENTIFIER NOT NULL,
    PriceId     UNIQUEIDENTIFIER NOT NULL,
    PricePeriod DATETIME2(3)     NOT NULL,
    Amount      DECIMAL(19, 5)   NOT NULL,
    ChangedDate DATETIME2        NOT NULL,
    CreatedDate DATETIME2        NULL,
    CONSTRAINT price_detail_pk PRIMARY KEY NONCLUSTERED (Id),
    CONSTRAINT FK_PriceDetail_Price
        FOREIGN KEY (PriceId) REFERENCES dbo.price (Id) ON DELETE NO ACTION
);

CREATE INDEX IX_Price_detail_PricePeriod
    ON dbo.price_detail (PricePeriod);

CREATE INDEX IX_Price_detail_id
    ON dbo.price_detail (Id);
GO

-- -------------------------------------------------------------------------
-- exchange_rate  (no dependencies)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.exchange_rate...';
CREATE TABLE dbo.exchange_rate
(
    Id                 UNIQUEIDENTIFIER NOT NULL,
    ExchangeRatePeriod DATETIME2(3)     NOT NULL,
    ExchangeRate       DECIMAL(19, 5)   NULL,
    ExchangeRateType   INT              NOT NULL,
    ChangedDate        DATETIME2        NOT NULL,
    CreatedDate        DATETIME2        NULL,
    CONSTRAINT exchange_rate_pk PRIMARY KEY NONCLUSTERED (Id)
);

CREATE INDEX IX_exchange_rate_exchangerateperiod
    ON dbo.exchange_rate (ExchangeRatePeriod);

CREATE UNIQUE INDEX uk_exchange_rate
    ON dbo.exchange_rate (Id);
GO

-- -------------------------------------------------------------------------
-- raw  (no key, no dependencies)
-- -------------------------------------------------------------------------
PRINT 'Creating dbo.raw...';
CREATE TABLE dbo.raw
(
    MeasurementId UNIQUEIDENTIFIER NOT NULL,
    TimeStamp     DATETIME2(3)     NOT NULL,
    Location      VARCHAR(50)      NOT NULL,
    Raw           VARCHAR(5000)    NOT NULL,
    IsNew         BIT              NOT NULL
);
GO

PRINT 'All tables created successfully.';
