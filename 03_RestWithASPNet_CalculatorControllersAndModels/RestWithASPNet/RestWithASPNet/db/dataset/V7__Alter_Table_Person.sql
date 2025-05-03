IF NOT EXISTS (SELECT 1 FROM sys.tables a INNER JOIN sys.columns b ON b.object_id = a.object_id WHERE a.name = 'person' and b.name = 'enabled')
BEGIN

  --// insere o ID e o endere�o numa tabela tempor�ria
  SELECT id
        ,[address]
  INTO #tmp_person
  FROM dbo.person

  --// remove a coluna endere�o
  ALTER TABLE dbo.person DROP COLUMN [address]

  --// adiciona a coluna ativo logo ap�s a coluna g�nero
  ALTER TABLE dbo.person ADD [enabled] BIT NOT NULL DEFAULT 1

  --// adiciona novamente a coluna endere�o
  ALTER TABLE dbo.person add [address] Varchar(100) COLLATE Latin1_General_CI_AS NOT NULL DEFAULT '';

  --// atualiza a tabela f�sica novamente pra conter o endere�o 
  UPDATE dbo.person
  SET person.[address] = tmp.[address]
  FROM dbo.person 
       INNER JOIN #tmp_person tmp
       ON tmp.id = person.id

END