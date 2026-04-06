-- =============================================================================
-- Azure SQL Server – Seed data for dbo.detail
-- Purpose : Insert the minimum prerequisite data needed to add rows to dbo.detail
-- Depends : Run CreateSchema.sql first
-- Date    : 2026-04-02
-- =============================================================================

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

IF OBJECT_ID(N'dbo.Location', N'U') IS NULL
    THROW 50001, 'Table dbo.Location does not exist. Run CreateSchema.sql first.', 1;
GO

IF OBJECT_ID(N'dbo.detail', N'U') IS NULL
    THROW 50002, 'Table dbo.detail does not exist. Run CreateSchema.sql first.', 1;
GO

BEGIN TRANSACTION;

DECLARE @LocationId UNIQUEIDENTIFIER = '11111111-1111-1111-1111-111111111111';
DECLARE @ChangedDate DATETIME2(7) = SYSUTCDATETIME();
DECLARE @CreatedDate DATETIME2(7) = SYSUTCDATETIME();

-- -----------------------------------------------------------------------------
-- 1. Seed the required parent row in dbo.Location.
--    ApiKeyId is nullable, so no ApiKey row is required for inserting detail rows.
-- -----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1
    FROM dbo.Location
    WHERE LocationId = @LocationId
)
BEGIN
    INSERT INTO dbo.Location (
        LocationId,
        IsActive,
        LocationName,
        LocationAddress,
        SerialNumber,
        ApiKeyId,
        ChangedDate,
        CreatedDate
    )
    VALUES (
        @LocationId,
        1,
        'pihl-4787',
        'Sjusjøveien 4787, 2612 Sjusjøen, Norway',
        'SN-DETAIL-001',
        NULL,
        @ChangedDate,
        @CreatedDate
    );
END;

-- -----------------------------------------------------------------------------
-- 2. Seed sample rows into dbo.detail.
--    Uses fixed GUIDs so the script is safe to run multiple times.
-- -----------------------------------------------------------------------------
IF NOT EXISTS (
    SELECT 1 FROM dbo.detail
    WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1'
)
BEGIN
    INSERT INTO dbo.detail (
        Id,
        MeasurementId,
        TimeStamp,
        LocationId,
        Name,
        ObisCodeId,
        ObisCode,
        Unit,
        ValueStr,
        ValueNum
    )
    VALUES (
        'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa1',
        'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb1',
        '2026-04-01T12:00:00.000',
        @LocationId,
        'Active power',
        1,
        '1-0:1.7.0.255',
        'kW',
        '2.154',
        2.15400
    );
END;

IF NOT EXISTS (
    SELECT 1 FROM dbo.detail
    WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2'
)
BEGIN
    INSERT INTO dbo.detail (
        Id,
        MeasurementId,
        TimeStamp,
        LocationId,
        Name,
        ObisCodeId,
        ObisCode,
        Unit,
        ValueStr,
        ValueNum
    )
    VALUES (
        'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa2',
        'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb2',
        '2026-04-01T12:01:00.000',
        @LocationId,
        'Active power',
        1,
        '1-0:1.7.0.255',
        'kW',
        '2.287',
        2.28700
    );
END;

IF NOT EXISTS (
    SELECT 1 FROM dbo.detail
    WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3'
)
BEGIN
    INSERT INTO dbo.detail (
        Id,
        MeasurementId,
        TimeStamp,
        LocationId,
        Name,
        ObisCodeId,
        ObisCode,
        Unit,
        ValueStr,
        ValueNum
    )
    VALUES (
        'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa3',
        'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb3',
        '2026-04-01T12:02:00.000',
        @LocationId,
        'Voltage L1',
        9,
        '1-0:32.7.0.255',
        'V',
        '230.4',
        230.40000
    );
END;

IF NOT EXISTS (
    SELECT 1 FROM dbo.detail
    WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4'
)
BEGIN
    INSERT INTO dbo.detail (
        Id,
        MeasurementId,
        TimeStamp,
        LocationId,
        Name,
        ObisCodeId,
        ObisCode,
        Unit,
        ValueStr,
        ValueNum
    )
    VALUES (
        'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa4',
        'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb4',
        '2026-04-01T12:03:00.000',
        @LocationId,
        'Current L1',
        12,
        '1-0:31.7.0.255',
        'A',
        '8.73',
        8.73000
    );
END;

IF NOT EXISTS (
    SELECT 1 FROM dbo.detail
    WHERE Id = 'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5'
)
BEGIN
    INSERT INTO dbo.detail (
        Id,
        MeasurementId,
        TimeStamp,
        LocationId,
        Name,
        ObisCodeId,
        ObisCode,
        Unit,
        ValueStr,
        ValueNum
    )
    VALUES (
        'aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaa5',
        'bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbb5',
        '2026-04-01T12:04:00.000',
        @LocationId,
        'Hourly active import energy',
        5,
        '1-0:1.8.0.255',
        'kWh',
        '15432.117',
        15432.11700
    );
END;

COMMIT TRANSACTION;
GO

-- -----------------------------------------------------------------------------
-- 3. Verify the inserted seed data.
-- -----------------------------------------------------------------------------
SELECT l.LocationId,
       l.LocationName,
       l.SerialNumber
FROM dbo.Location l
WHERE l.LocationId = '11111111-1111-1111-1111-111111111111';
GO

SELECT d.Id,
       d.MeasurementId,
       d.TimeStamp,
       d.LocationId,
       d.Name,
       d.ObisCodeId,
       d.ObisCode,
       d.Unit,
       d.ValueStr,
       d.ValueNum
FROM dbo.detail d
WHERE d.LocationId = '11111111-1111-1111-1111-111111111111'
ORDER BY d.TimeStamp;
GO

