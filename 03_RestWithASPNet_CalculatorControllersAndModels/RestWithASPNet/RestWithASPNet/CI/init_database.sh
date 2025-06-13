#!/bin/bash

SQL_DIR="/home/database"
SQL_USER="sa"
SQL_PASS="P@ssw0rd"
SQL_DB="rest_with_asp_net"
SQL_HOST="localhost"

# Aguarda o SQL Server ficar disponível
echo "Aguardando SQL Server iniciar em $SQL_HOST..."
until /opt/mssql-tools/bin/sqlcmd -S "$SQL_HOST" -U "$SQL_USER" -P "$SQL_PASS" -Q "SELECT 1" &> /dev/null
do
  echo "Aguardando conexão com SQL Server..."
  sleep 2
done

/opt/mssql-tools/bin/sqlcmd -S "$SQL_HOST" -U "$SQL_USER" -P "$SQL_PASS" -d master -Q "IF DB_ID('$SQL_DB') IS NULL CREATE DATABASE [$SQL_DB]"

for sql_file in $(find "$SQL_DIR" -name "*.sql" | sort --version-sort); do
  echo "Executando $sql_file..."
  /opt/mssql-tools/bin/sqlcmd -S "$SQL_HOST" -U "$SQL_USER" -P "$SQL_PASS" -d "$SQL_DB" -i "$sql_file"
  if [ $? -eq 0 ]; then
    echo "$sql_file executado com sucesso"
  else
    echo "Erro ao executar $sql_file, tentando novamente em 1s..."
    sleep 1
  fi
done