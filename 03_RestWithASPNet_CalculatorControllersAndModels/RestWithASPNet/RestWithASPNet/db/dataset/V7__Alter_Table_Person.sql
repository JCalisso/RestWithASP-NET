IF NOT EXISTS (SELECT 1 FROM sys.tables a INNER JOIN sys.columns b ON b.object_id = a.object_id WHERE a.name = 'person' and b.name = 'enabled')
BEGIN

  --// insere o ID e o endereço numa tabela temporária
  SELECT id
        ,[address]
  INTO #tmp_person
  FROM dbo.person

  --// remove a coluna endereço
  ALTER TABLE dbo.person DROP COLUMN [address]

  --// adiciona a coluna ativo logo após a coluna gênero
  ALTER TABLE dbo.person ADD [enabled] BIT NOT NULL DEFAULT 1

  --// adiciona novamente a coluna endereço
  ALTER TABLE dbo.person add [address] Varchar(100) COLLATE Latin1_General_CI_AS NOT NULL DEFAULT '';

  --// atualiza a tabela física novamente pra conter o endereço 
  UPDATE dbo.person
  SET person.[address] = tmp.[address]
  FROM dbo.person 
       INNER JOIN #tmp_person tmp
       ON tmp.id = person.id

END