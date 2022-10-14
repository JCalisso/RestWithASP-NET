IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'person' )
BEGIN
  CREATE TABLE person (id Integer PRIMARY KEY  NOT NULL Identity(1,1)
                      ,address    Varchar(100) NOT NULL
                      ,first_name Varchar(80)  NOT NULL
                      ,gender     Varchar(6)   NOT NULL
                      ,last_name  Varchar(80)  NOT NULL)
END