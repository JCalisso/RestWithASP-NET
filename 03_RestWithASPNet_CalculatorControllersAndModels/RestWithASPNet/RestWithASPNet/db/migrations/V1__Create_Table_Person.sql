--IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'person')
--BEGIN
  CREATE TABLE dbo.person (id         Integer Primary Key Identity(1,1)
                          ,first_name Varchar(80)  COLLATE Latin1_General_CI_AS NOT NULL 
                          ,last_name  Varchar(80)  COLLATE Latin1_General_CI_AS NOT NULL 
                          ,gender     Varchar(6)   COLLATE Latin1_General_CI_AS NOT NULL 
                          ,address    Varchar(100) COLLATE Latin1_General_CI_AS NOT NULL);
--END
--GO