--IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'users')
--BEGIN
  CREATE TABLE dbo.users (id                        Integer Primary Key Identity(1,1)
                         ,[user_name]               Varchar(50)  COLLATE Latin1_General_CI_AS NOT NULL 
                         ,[password]                Varchar(130) COLLATE Latin1_General_CI_AS NOT NULL
                         ,full_name                 Varchar(120) COLLATE Latin1_General_CI_AS NOT NULL 
                         ,refresh_token             VARCHAR(500) COLLATE Latin1_General_CI_AS     NULL 
						 ,refresh_token_expiry_time Datetime                                      NULL
						 ,UNIQUE ([user_name])) ;
--END
--GO