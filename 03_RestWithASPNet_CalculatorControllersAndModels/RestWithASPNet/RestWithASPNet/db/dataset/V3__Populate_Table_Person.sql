
IF NOT EXISTS (SELECT TOP 1 1 FROM dbo.person)
BEGIN
  SET IDENTITY_INSERT dbo.person ON 
  
  INSERT dbo.person (id, first_name, last_name, gender, address) VALUES (1, 'Jean', 'Calisso', 'Male', 'Tupã - São Paulo');
  
  INSERT dbo.person (id, first_name, last_name, gender, address) VALUES (2, 'João', 'Redó', 'Male', 'Tupã - São Paulo');
  
  INSERT dbo.person (id, first_name, last_name, gender, address) VALUES (3, 'Iaponilly', 'Redó', 'Female', 'Barueri - São Paulo');
  
  INSERT dbo.person (id, first_name, last_name, gender, address) VALUES (4, 'Matheus', 'Fiori', 'Male', 'Tupã - São Paulo');
  
  SET IDENTITY_INSERT dbo.person OFF

END
GO