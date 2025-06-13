FROM mcr.microsoft.com/mssql/server:2022-latest
# Expõe a porta padrão do SQL Server
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=P@ssw0rd

# RUN mkdir -p /home/database/migrations/
# RUN mkdir -p /home/database/dataset/

RUN echo "Copiando os arquivos de migração e dataset para o contêiner"
COPY ["/RestWithASPNet/db/migrations/", "/home/database/migrations"]
COPY ["/RestWithASPNet/db/dataset/", "/home/database/dataset"]

RUN echo "Copiando o arquivo shell para executar os scripts"
COPY ["/RestWithASPNet/CI/init_database.sh", "/docker-entrypoint-initdb.d/init_database.sh"]

RUN echo "Dando permissão de execução para o script init_database.sh"
# USER root
# RUN chmod a+x /docker-entrypoint-initdb.d/

EXPOSE 1433

# RUN chmod +x /docker-entrypoint-initdb.d/

# RUN echo "Executando o script init_database.sh ao iniciar o contêiner"
# ENTRYPOINT ["/docker-entrypoint-initdb.d/", "-C"]
# CMD /bin/bash -c "/docker-entrypoint-initdb.d/init_database.sh"

# RUN echo "O contêiner está pronto para ser iniciado com o SQL Server"