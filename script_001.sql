
IF NOT EXISTS (SELECT 1 FROM  sys.databases WHERE name = 'rest_with_asp_net_udemy' )
BEGIN
  CREATE DATABASE rest_with_asp_net_udemy
END
GO

USE rest_with_asp_net_udemy
GO

IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'person' )
BEGIN
  CREATE TABLE person (id BigInt PRIMARY KEY  NOT NULL Identity(1,1)
                      ,address    Varchar(100) NOT NULL
                      ,first_name Varchar(80)  NOT NULL
                      ,gender     Varchar(6)   NOT NULL
                      ,last_name  Varchar(80)  NOT NULL)
END
GO

--// First row
IF NOT EXISTS (SELECT 1 FROM dbo.person WHERE first_name = 'Jean' AND last_name = 'Calisso')
BEGIN
  INSERT INTO dbo.person (address
                         ,first_name
                         ,gender
                         ,last_name)
  VALUES ('Tupã - São Paulo'
         ,'Jean'
         ,'Male'
         ,'Calisso')
END
GO

select * from person