IF NOT EXISTS (SELECT 1 FROM users WHERE [user_name] = 'jean')
BEGIN
  INSERT INTO [users] ([user_name]
                      ,[password]
                      ,[full_name]
                      ,[refresh_token]
                      ,[refresh_token_expiry_time]) 
  VALUES ('jean'
         ,'240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9'
         ,'JEAN VITOR C. CALISSO'
         ,'h9lzVOoLlBoTbcQrh/e16/aIj+4p6C67lLdDbBRMsjE='
         ,GETDATE());
END
GO