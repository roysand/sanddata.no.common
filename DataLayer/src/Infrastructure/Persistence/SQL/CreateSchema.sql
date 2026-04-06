-- =============================================================================
-- Azure SQL Server – Full Database Schema
-- Generated from EF Core entity/configuration classes
-- Project  : sanddata.no.common / DataLayer
-- Date     : 2026-04-02
-- =============================================================================

-- =============================================================================
-- 1. ApiKey
--    No external dependencies.
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'ApiKey' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[ApiKey] (
        [ApiKeyId]    UNIQUEIDENTIFIER  NOT NULL,
        [Key]         VARCHAR(40)       NOT NULL,
        [Admin]       BIT               NOT NULL,
        [ChangedDate] DATETIME2(7)      NOT NULL,
        [CreatedDate] DATETIME2(7)      NULL,
        CONSTRAINT [PK_ApiKey] PRIMARY KEY CLUSTERED ([ApiKeyId])
    );
END;
GO

-- =============================================================================
-- 2. Account
--    FK -> ApiKey
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'Account' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[Account] (
        [AccountId]   UNIQUEIDENTIFIER  NOT NULL,
        [AccountName] VARCHAR(30)       NOT NULL,
        [Active]      BIT               NOT NULL,
        [ApiKeyId]    UNIQUEIDENTIFIER  NULL,
        [ChangedDate] DATETIME2(7)      NOT NULL,
        [CreatedDate] DATETIME2(7)      NULL,
        CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([AccountId]),
        CONSTRAINT [FK_Account_ApiKey]
            FOREIGN KEY ([ApiKeyId]) REFERENCES [dbo].[ApiKey] ([ApiKeyId])
    );
END;
GO

-- =============================================================================
-- 3. AccountContact
--    FK -> Account  (NO ACTION on delete)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'AccountContact' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[AccountContact] (
        [AccountContactId]   UNIQUEIDENTIFIER  NOT NULL,
        [AccountId]          UNIQUEIDENTIFIER  NOT NULL,
        [ContactFirstName]   VARCHAR(100)      NOT NULL,
        [ContactLastName]    VARCHAR(100)      NOT NULL,
        [ContactEmail]       VARCHAR(100)      NOT NULL,
        [ContactMobilePhone] VARCHAR(20)       NULL,
        [ChangedDate]        DATETIME2(7)      NOT NULL,
        [CreatedDate]        DATETIME2(7)      NULL,
        CONSTRAINT [PK_AccountContact] PRIMARY KEY CLUSTERED ([AccountContactId]),
        CONSTRAINT [FK_AccountContact_Account]
            FOREIGN KEY ([AccountId]) REFERENCES [dbo].[Account] ([AccountId])
    );
END;
GO

-- =============================================================================
-- 4. Location
--    FK -> ApiKey
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'Location' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[Location] (
        [LocationId]      UNIQUEIDENTIFIER  NOT NULL,
        [IsActive]        BIT               NOT NULL,
        [LocationName]    VARCHAR(100)      NOT NULL,
        [LocationAddress] VARCHAR(100)      NULL,
        [SerialNumber]    VARCHAR(20)       NULL,
        [ApiKeyId]        UNIQUEIDENTIFIER  NULL,
        [ChangedDate]     DATETIME2(7)      NOT NULL,
        [CreatedDate]     DATETIME2(7)      NULL,
        CONSTRAINT [PK_Location] PRIMARY KEY CLUSTERED ([LocationId]),
        CONSTRAINT [FK_Location_ApiKey]
            FOREIGN KEY ([ApiKeyId]) REFERENCES [dbo].[ApiKey] ([ApiKeyId])
    );
END;
GO

-- =============================================================================
-- 5. AppUser
--    No external dependencies.
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'AppUser' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[AppUser] (
        [AppUserId]              UNIQUEIDENTIFIER  NOT NULL,
        [IsActive]               BIT               NOT NULL,
        [FirstName]              NVARCHAR(100)     NOT NULL,
        [LastName]               NVARCHAR(100)     NOT NULL,
        [HashedPassword]         NVARCHAR(MAX)     NOT NULL,
        [Email]                  NVARCHAR(100)     NOT NULL,
        [RefreshToken]           NVARCHAR(50)      NULL,
        [RefreshTokenExpiryTime] DATETIME2(7)      NULL,
        CONSTRAINT [PK_AppUser] PRIMARY KEY CLUSTERED ([AppUserId])
    );
END;
GO

-- =============================================================================
-- 6. Role
--    No external dependencies.
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'Role' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[Role] (
        [RoleId]          UNIQUEIDENTIFIER  NOT NULL,
        [RoleName]        VARCHAR(50)       NOT NULL,
        [RoleDescription] VARCHAR(250)      NOT NULL,
        [ChangedDate]     DATETIME2(7)      NOT NULL,
        [CreatedDate]     DATETIME2(7)      NULL,
        CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([RoleId])
    );
END;
GO

-- =============================================================================
-- 7. appuser_location  (many-to-many join table)
--    FK -> AppUser (RESTRICT), FK -> Location (RESTRICT)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'appuser_location' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[appuser_location] (
        [AppUserId]  UNIQUEIDENTIFIER  NOT NULL,
        [LocationId] UNIQUEIDENTIFIER  NOT NULL,
        CONSTRAINT [appuser_location_pk] PRIMARY KEY CLUSTERED ([AppUserId], [LocationId]),
        CONSTRAINT [FK_appuser_location_AppUser]
            FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([AppUserId]),
        CONSTRAINT [FK_appuser_location_Location]
            FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
    );
END;
GO

-- =============================================================================
-- 8. appuser_role  (many-to-many join table)
--    FK -> AppUser (CASCADE), FK -> Role (CASCADE)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'appuser_role' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[appuser_role] (
        [AppUserId] UNIQUEIDENTIFIER  NOT NULL,
        [RoleId]    UNIQUEIDENTIFIER  NOT NULL,
        CONSTRAINT [appuser_role_pk] PRIMARY KEY CLUSTERED ([AppUserId], [RoleId]),
        CONSTRAINT [FK_appuser_role]
            FOREIGN KEY ([AppUserId]) REFERENCES [dbo].[AppUser] ([AppUserId])
            ON DELETE CASCADE,
        CONSTRAINT [FK_role_appuser]
            FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([RoleId])
            ON DELETE CASCADE
    );
END;
GO

-- =============================================================================
-- 9. detail
--    FK -> Location (RESTRICT / NO ACTION)
--    ObisCodeId enum values:
--      1=PowerUsed, 2=MeterId, 3=MeterType, 4=ClockAndDate,
--      5=HourlyActiveImportEnergy, 6=HourlyActiveExportEnergy,
--      7=HourlyReactiveImportEnergy, 8=HourlyReactiveExportEnergy,
--      9=UL1PhaseVoltage, 10=UL2PhaseVoltage, 11=UL3PhaseVoltage,
--      12=IL1Current, 13=IL2Current, 14=IL3Current
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'detail' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[detail] (
        [Id]            UNIQUEIDENTIFIER  NOT NULL,
        [MeasurementId] UNIQUEIDENTIFIER  NOT NULL,
        [TimeStamp]     DATETIME2(3)      NOT NULL,
        [LocationId]    UNIQUEIDENTIFIER  NOT NULL,
        [Name]          VARCHAR(30)       NULL,
        [ObisCodeId]    INT               NOT NULL,
        [ObisCode]      VARCHAR(50)       NULL,
        [Unit]          VARCHAR(5)        NULL,
        [ValueStr]      VARCHAR(100)      NULL,
        [ValueNum]      DECIMAL(19, 5)    NOT NULL,
        CONSTRAINT [detail_pk] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_Detail_Location]
            FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Detail_TimeStamp' AND object_id = OBJECT_ID('dbo.detail')
)
BEGIN
    CREATE INDEX [IX_Detail_TimeStamp]
        ON [dbo].[detail] ([TimeStamp]);
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'nci_wi_detail_99F2D155AA826D3C5CC127D4720BFE87'
      AND object_id = OBJECT_ID('dbo.detail')
)
BEGIN
    CREATE INDEX [nci_wi_detail_99F2D155AA826D3C5CC127D4720BFE87]
        ON [dbo].[detail] ([ObisCodeId], [TimeStamp]);
END;
GO

-- =============================================================================
-- 10. minute
--     Composite PK (TimeStamp, LocationId), FK -> Location (NO ACTION)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'minute' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[minute] (
        [TimeStamp]  DATETIME2(3)    NOT NULL,
        [LocationId] UNIQUEIDENTIFIER NOT NULL,
        [Unit]       VARCHAR(5)      NULL,
        [ValueNum]   DECIMAL(19, 5)  NULL,
        [Count]      SMALLINT        NULL,
        CONSTRAINT [minute_pk] PRIMARY KEY CLUSTERED ([TimeStamp], [LocationId]),
        CONSTRAINT [FK_Minute_Location]
            FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Minute_TimeStamp' AND object_id = OBJECT_ID('dbo.minute')
)
BEGIN
    CREATE INDEX [IX_Minute_TimeStamp]
        ON [dbo].[minute] ([TimeStamp]);
END;
GO

-- =============================================================================
-- 11. hour
--     Composite PK (TimeStamp, LocationId), FK -> Location (NO ACTION)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'hour' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[hour] (
        [TimeStamp]  DATETIME2(3)     NOT NULL,
        [LocationId] UNIQUEIDENTIFIER  NOT NULL,
        [Unit]       VARCHAR(5)        NULL,
        [ValueNum]   DECIMAL(19, 5)    NULL,
        [Count]      SMALLINT          NULL,
        CONSTRAINT [hour_pk] PRIMARY KEY CLUSTERED ([TimeStamp], [LocationId]),
        CONSTRAINT [FK_Hour_Location]
            FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Hour_TimeStamp' AND object_id = OBJECT_ID('dbo.hour')
)
BEGIN
    CREATE INDEX [IX_Hour_TimeStamp]
        ON [dbo].[hour] ([TimeStamp]);
END;
GO

-- =============================================================================
-- 12. day
--     Composite PK (Date, LocationId), FK -> Location (NO ACTION)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'day' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[day] (
        [Date]       DATETIME2(3)     NOT NULL,
        [LocationId] UNIQUEIDENTIFIER  NOT NULL,
        [Unit]       VARCHAR(5)        NULL,
        [ValueNum]   DECIMAL(19, 5)    NULL,
        [Count]      SMALLINT          NULL,
        [PriceNOK]   DECIMAL(19, 5)    NULL,
        CONSTRAINT [day_pk] PRIMARY KEY CLUSTERED ([Date], [LocationId]),
        CONSTRAINT [FK_Day_Location]
            FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Day_Date' AND object_id = OBJECT_ID('dbo.day')
)
BEGIN
    CREATE INDEX [IX_Day_Date]
        ON [dbo].[day] ([Date]);
END;
GO

-- =============================================================================
-- 13. price
--     Non-clustered PK, unique index on PricePeriod, FK -> Location (NO ACTION)
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'price' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[price] (
        [PriceId]     UNIQUEIDENTIFIER  NOT NULL,
        [PricePeriod] DATETIME2(3)      NOT NULL,
        [Modified]    DATETIME2(3)      NOT NULL,
        [LocationId]  UNIQUEIDENTIFIER  NOT NULL,
        [Currency]    VARCHAR(5)        NOT NULL,
        [Unit]        VARCHAR(5)        NOT NULL,
        [Average]     DECIMAL(19, 5)    NULL,
        [Max]         DECIMAL(19, 5)    NULL,
        [Min]         DECIMAL(19, 5)    NOT NULL,
        [InDomain]    VARCHAR(20)       NOT NULL,
        [OutDomain]   VARCHAR(20)       NOT NULL,
        CONSTRAINT [price_pk] PRIMARY KEY NONCLUSTERED ([PriceId]),
        CONSTRAINT [FK_Price_Location]
            FOREIGN KEY ([LocationId]) REFERENCES [dbo].[Location] ([LocationId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Price_Id' AND object_id = OBJECT_ID('dbo.price')
)
BEGIN
    CREATE INDEX [IX_Price_Id]
        ON [dbo].[price] ([PriceId]);
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Price_PricePeriod' AND object_id = OBJECT_ID('dbo.price')
)
BEGIN
    CREATE UNIQUE INDEX [IX_Price_PricePeriod]
        ON [dbo].[price] ([PricePeriod]);
END;
GO

-- =============================================================================
-- 14. price_detail
--     Non-clustered PK, FK -> price
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'price_detail' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[price_detail] (
        [PriceDetailId] UNIQUEIDENTIFIER  NOT NULL,
        [PriceId]       UNIQUEIDENTIFIER  NOT NULL,
        [PricePeriod]   DATETIME2(3)      NOT NULL,
        [Amount]        DECIMAL(19, 5)    NOT NULL,
        CONSTRAINT [price_detail_pk] PRIMARY KEY NONCLUSTERED ([PriceDetailId]),
        CONSTRAINT [FK_PriceDetail_Price]
            FOREIGN KEY ([PriceId]) REFERENCES [dbo].[price] ([PriceId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Price_detail_PricePeriod' AND object_id = OBJECT_ID('dbo.price_detail')
)
BEGIN
    CREATE INDEX [IX_Price_detail_PricePeriod]
        ON [dbo].[price_detail] ([PricePeriod]);
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_Price_detail_id' AND object_id = OBJECT_ID('dbo.price_detail')
)
BEGIN
    CREATE INDEX [IX_Price_detail_id]
        ON [dbo].[price_detail] ([PriceDetailId]);
END;
GO

-- =============================================================================
-- 15. exchange_rate
--     Non-clustered PK, unique index on ExchangeRateId, index on period
--     ExchangeRateType: 1 = EUR
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'exchange_rate' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[exchange_rate] (
        [ExchangeRateId]     UNIQUEIDENTIFIER  NOT NULL,
        [ExchangeRatePeriod] DATETIME2(3)      NOT NULL,
        [ExchangeRate]       DECIMAL(19, 5)    NULL,
        [ExchangeRateType]   INT               NOT NULL,
        CONSTRAINT [exchange_rate_pk] PRIMARY KEY NONCLUSTERED ([ExchangeRateId])
    );
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'IX_exchange_rate_exchangerateperiod'
      AND object_id = OBJECT_ID('dbo.exchange_rate')
)
BEGIN
    CREATE INDEX [IX_exchange_rate_exchangerateperiod]
        ON [dbo].[exchange_rate] ([ExchangeRatePeriod]);
END;
GO

IF NOT EXISTS (
    SELECT 1 FROM sys.indexes
    WHERE name = 'uk_exchange_rate' AND object_id = OBJECT_ID('dbo.exchange_rate')
)
BEGIN
    CREATE UNIQUE INDEX [uk_exchange_rate]
        ON [dbo].[exchange_rate] ([ExchangeRateId]);
END;
GO

-- =============================================================================
-- 16. raw
--     Keyless entity (HasNoKey). Stores raw AMS telegram data.
-- =============================================================================
IF NOT EXISTS (
    SELECT 1 FROM sys.tables
    WHERE name = 'raw' AND schema_id = SCHEMA_ID('dbo')
)
BEGIN
    CREATE TABLE [dbo].[raw] (
        [MeasurementId] UNIQUEIDENTIFIER  NOT NULL,
        [TimeStamp]     DATETIME2(3)      NOT NULL,
        [Location]      VARCHAR(50)       NOT NULL,
        [Raw]           VARCHAR(5000)     NOT NULL,
        [IsNew]         BIT               NOT NULL
    );
END;
GO

