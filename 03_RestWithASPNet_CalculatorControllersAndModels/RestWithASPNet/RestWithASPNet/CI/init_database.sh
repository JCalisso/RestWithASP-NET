# !/bin/bash
SQL_DIR="/home/tmp/db/" 
SQL_USER="SA" 
SQL_PASS="P@ssw0rd" 
SQL_DB="rest_with_asp_net" 
SQL_HOST="localhost,1433" 
# echo "$SQL_HOST $SQL_USER $SQL_PASS $SQL_DB"
# /opt/mssql-tools/bin/sqlcmd -S 127.0.0.1,1433 -U sa -P P@ssw0rd -Q "SELECT name FROM sys.databases"
/opt/mssql/bin/sqlservr & 
sleep 30 
# Wait for SQL Server to start
until /opt/mssql-tools18/bin/sqlcmd -S "$SQL_HOST" -No -U "$SQL_USER" -P "$SQL_PASS" -Q "SELECT 1" >/dev/null 2>&1; do 
  echo "Aguardando o SQL Server iniciar..." 
  sleep 5 
done 
echo "Iniciando o script de inicialização do banco de dados..." 
/opt/mssql-tools18/bin/sqlcmd -S "$SQL_HOST" -No -U "$SQL_USER" -d master -P "$SQL_PASS" -Q "IF DB_ID('$SQL_DB') IS NULL BEGIN CREATE DATABASE [$SQL_DB] END" 
echo "Banco de dados $SQL_DB criado." 
for sql_file in $(find "$SQL_DIR" -name "*.sql" | sort --version-sort); do 
  echo "Executando $sql_file..." 
  /opt/mssql-tools18/bin/sqlcmd -S "$SQL_HOST" -No -d "$SQL_DB" -U "$SQL_USER" -P "$SQL_PASS" -d "$SQL_DB" -i "$sql_file" 
  if [ $? -eq 0 ]; then 
    echo "$sql_file executado com sucesso" 
  else 
    echo "Erro ao executar $sql_file, tentando novamente em 1s..." 
    sleep 1 
  fi 
done 
wait 