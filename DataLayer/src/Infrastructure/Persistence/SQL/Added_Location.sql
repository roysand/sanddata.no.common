-- First upgrade table Location with proper indexes and primary key
IF NOT EXISTS (
    SELECT 1 
    FROM sys.indexes 
    WHERE object_id = OBJECT_ID('dbo.location') 
    AND is_primary_key = 1
)
BEGIN
    -- Add primary key if it doesn't exist
ALTER TABLE dbo.location
    ADD CONSTRAINT PK_Location PRIMARY KEY (LocationId);
END




-- 1. Add the new LocationId column
ALTER TABLE dbo.minute
    ADD LocationId UNIQUEIDENTIFIER NULL;

ALTER TABLE hour
    ADD LocationId UNIQUEIDENTIFIER NULL;

ALTER TABLE hour
    ADD CONSTRAINT FK_Hour_Location
        FOREIGN KEY (LocationId) REFERENCES location(LocationId);

BEGIN TRAN
UPDATE h
SET d.LocationId = l.LocationId
    FROM dbo.hour h
JOIN dbo.location l ON l.LocationName = h.Location;
COMMIT TRAN

ALTER TABLE dbo.Hour
ALTER COLUMN LocationId UNIQUEIDENTIFIER NOT NULL;


ALTER TABLE dbo.hour
DROP COLUMN Location;    
     
-- 2. Update LocationId with values converted from the old Location column if possible (manual step required)
begin tran
update m
set m.LocationId = loc.LocationId
    from minute m
join Location loc on loc.LocationName = m.Location

select * from Minute

commit tran

-- 3. Drop the old Location column
ALTER TABLE dbo.minute
DROP COLUMN Location;

-- 4. Alter LocationId to NOT NULL
ALTER TABLE dbo.minute
ALTER COLUMN LocationId UNIQUEIDENTIFIER NOT NULL;

-- 5. Add composite primary key
ALTER TABLE dbo.minute
    ADD CONSTRAINT minute_pk PRIMARY KEY (TimeStamp, LocationId);

-- 6. Add foreign key constraint
ALTER TABLE dbo.minute
    ADD CONSTRAINT FK_Minute_Location FOREIGN KEY (LocationId) REFERENCES dbo.location(Id) ON DELETE NO ACTION;

-- 7. Add index on TimeStamp
CREATE INDEX IX_Minute_TimeStamp ON dbo.minute (TimeStamp);


--- DETAIL ----
ALTER TABLE dbo.detail
    ADD LocationId UNIQUEIDENTIFIER NULL;

BEGIN TRAN
UPDATE d
SET d.LocationId = l.LocationId
    FROM dbo.detail d
JOIN dbo.location l ON l.LocationName = d.Location;
COMMIT TRAN

ALTER TABLE dbo.detail
DROP COLUMN Location;

ALTER TABLE dbo.detail
ALTER COLUMN LocationId UNIQUEIDENTIFIER NOT NULL;

ALTER TABLE dbo.detail
    ADD CONSTRAINT FK_Detail_Location FOREIGN KEY (LocationId) REFERENCES dbo.location(LocationId) ON DELETE NO ACTION;

CREATE procedure dbo.GenerateMinuteStatistics
    as
begin
    declare @startDate as DateTime
    declare @stopDate as datetime
    declare @minMeasDate as datetime
    declare @maxMeasDate as datetime

    set @minMeasDate = (select min(timestamp) from detail)
    set @maxMeasDate = (select max(timestamp) from detail)
    set @startDate = (select max(timestamp) from minute)

    if (@startDate is null)
begin
        set @startDate = @minMeasDate
end
else
begin
        set @startDate = DateAdd(minute, +1, @startDate)
end

    set @stopDate = DateAdd(minute, -1, @maxMeasDate)
    set @stopDate = DATETIMEFROMPARTS(year(@stopDate), month(@stopDate), day(@stopDate), DATEPART(hour, @stopDate), DATEPART(minute, @stopDate), 0, 0)

    insert into minute (TimeStamp, LocationId, Unit, ValueNum, count)
select
    DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp), DATEPART(hour, d.timestamp), DATEPART(minute, d.TimeStamp), 0, 0) as TimeStamp,
        d.LocationId,
        'kW' as Unit,
        avg(d.valueNum) as ValueNum,
        count(*) as count
from dbo.detail d
where d.ObisCodeId = 1
  and d.TimeStamp between @startDate and @stopDate
group by
    d.LocationId,
    DATETIMEFROMPARTS(year(d.TimeStamp), month(d.TimeStamp), day(d.TimeStamp), DATEPART(hour, d.timestamp), DATEPART(minute, d.TimeStamp), 0, 0)
order by TimeStamp desc, LocationId asc, Unit asc
end
go