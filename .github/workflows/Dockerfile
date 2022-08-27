FROM tjbeck/mssql_server

WORKDIR /src

COPY ../BuildDatabase/CreateDatabase/buildDatabase.sql buildDatabase.sql
COPY ../BuildDatabase/Procedures/udpInsertMaterialsFromCSV.sql udpInsertMaterialsFromCSV.sql

ENV sa_password=FR*@ger12
ENV ACCEPT_EULA=Y

