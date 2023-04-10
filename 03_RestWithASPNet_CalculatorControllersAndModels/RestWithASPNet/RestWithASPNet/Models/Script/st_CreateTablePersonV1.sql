CREATE PROCEDURE dbo.st_CreateTablePersonV1
AS
BEGIN
  -- Validando se a tabela person existe, se existir é feito um backup dela, e dropada caso o backup for feito corretamente;
  IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'person')
  BEGIN
    SELECT [address]
          ,[first_name]
          ,[gender]
          ,[last_name]
    INTO dbo.person_bkp
    FROM dbo.person;

    IF EXISTS (SELECT 1 FROM sys.tables WHERE name = 'person_bkp')
    BEGIN
      DROP TABLE dbo.person;
    END
  END

  IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'person')
  BEGIN
    CREATE TABLE dbo.person (id         Integer Primary Key Identity(1,1)
                            ,first_name Varchar(80)  COLLATE Latin1_General_CI_AS NOT NULL 
                            ,last_name  Varchar(80)  COLLATE Latin1_General_CI_AS NOT NULL 
                            ,gender     Varchar(6)   COLLATE Latin1_General_CI_AS NOT NULL 
                            ,address    Varchar(100) COLLATE Latin1_General_CI_AS NOT NULL);
  END
END