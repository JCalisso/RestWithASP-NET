IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'books')
BEGIN
  CREATE TABLE dbo.books (id          Integer Primary Key Identity(1,1)
                          ,author      Varchar(80)  COLLATE Latin1_General_CI_AS NOT NULL 
                          ,title       Varchar(100) COLLATE Latin1_General_CI_AS NOT NULL
                          ,launch_date Datetime                                  NOT NULL 
                          ,price       Numeric(18,2)                             NOT NULL) ;
END
GO