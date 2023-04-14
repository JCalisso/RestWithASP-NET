
IF NOT EXISTS (SELECT 1 FROM dbo.books)
BEGIN
  INSERT INTO dbo.books(author,launch_date,price,title)
  VALUES ('Michael C. Feathers', '20171129', 49.00, 'Working effectively with legacy code'),
         ('Ralph Johnson, Erich Gamma, John Vlissides e Richard Helm', '20171129', 45.00, 'Design Patterns'),
         ('Robert C. Martin', '20090110', 77.00, 'Clean Code'),
         ('Crockford', '20171107', 67.00, 'JavaScript'),
         ('Steve McConnell', '20171107', 58.00, 'Code complete'),
         ('Martin Fowler e Kent Beck', '20171107', 88.00, 'Refactoring'),
         ('Eric Freeman, Elisabeth Freeman, Kathy Sierra, Bert Bates', '20171107', 110.00, 'Head First Design Patterns'),
         ('Eric Evans', '20171107', 92.00, 'Domain Driven Design'),
         ('Brian Goetz e Tim Peierls', '20171107 ', 80.00, 'Java Concurrency in Practice'),
         ('Susan Cain', '20171107', 123.00, 'O poder dos quietos'),
         ('Roger S. Pressman', '20171107', 56.00, 'Engenharia de Software: uma abordagem profissional'),
         ('Viktor Mayer-Schonberger e Kenneth Kukier', '20171107', 54.00, 'Big Data: como extrair volume, variedade, velocidade e valor da avalanche de informação cotidiana'),
         ('Richard Hunter e George Westerman', '20171107', 95.00, 'O verdadeiro valor de TI'),
         ('Marc J. Schiller', '20171107', 45.00, 'Os 11 segredos de líderes de TI altamente influentes'),
         ('Aguinaldo Aragon Fernandes e Vladimir Ferraz de Abreu', '20171107', 54.00, 'Implantando a governança de TI')
END