
  ALTER TABLE dbo.person ADD enabled BIT;
  go
  ALTER TABLE dbo.person ADD CONSTRAINT df_person_enabled DEFAULT 1 FOR enabled;
  go
  ALTER TABLE dbo.person ADD CONSTRAINT ck_person_enabled CHECK (enabled IN (0,1));
  go
  UPDATE dbo.person SET enabled = 1;
  go