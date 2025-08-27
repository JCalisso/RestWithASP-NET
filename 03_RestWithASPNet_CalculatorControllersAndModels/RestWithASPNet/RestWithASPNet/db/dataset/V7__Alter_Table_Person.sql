IF NOT EXISTS (SELECT 1 FROM sys.tables a INNER JOIN sys.columns b ON b.object_id = a.object_id WHERE a.name = 'person' and b.name = 'enabled')
BEGIN
  ALTER TABLE dbo.person ADD enabled BIT NOT NULL DEFAULT 1
END
GO