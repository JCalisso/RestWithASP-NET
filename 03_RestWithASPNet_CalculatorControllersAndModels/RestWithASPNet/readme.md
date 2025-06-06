# Desenvolvimento e teste em outras plataformas

## MacOS

> Para executar a API utilizando Docker para o banco de dados MS SQL Server é necessário alguns ajustes.

1. Realizar a criação do ambiente com Docker Image
    * 2022-latest. <br>
    `` docker pull mcr.microsoft.com/mssql/server:2022-latest ``

    * Para inicialização do container: <br>
    ``docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest``

    * Para utilizar o **sqlcmd** via terminal, instalar o mesmo via brew install: <br>
    `` brew install sqlcmd `` <br>
    Para iniciar o **sqlcmd**:
    `` sqlcmd -S <ip address,port> -U <login_name> -P <yourpassword> -No ``

2. Para iniciar o projeto da API em Development.
    * Defina a variável de ambiente do .NET como Development: <br>
    `` export ASPNETCORE_ENVIRONMENT="Development" `` -- ajustar para "Production" após executar as migrations 

3. Para utilizar o GIT é necessário utilizar um PAT (Personal Access Token).
>    - Em GitHub \ Settings \ Developer Settings \ Personal Access Tokens \ Fine-Grained tokens.    \* *Cadastrar um Token novo se necessário*
>
>    - Durante o *commit/push*, adicionar o PAT no lugar da senha. 

<br>

---
### Fontes

[Hub Docker](https://hub.docker.com/r/microsoft/mssql-server) <br>
[Homebrew Formulae](https://formulae.brew.sh/formula/sqlcmd)