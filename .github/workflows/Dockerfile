FROM tjbeck/mssql_server

WORKDIR /src

COPY ../RLRMDB/BuildDatabase/CreateDatabase/buildDatabase.sql buildDatabase.sql
COPY ../RLRMDB/BuildDatabase/Procedures/udpInsertMaterialsFromCSV.sql udpInsertMaterialsFromCSV.sql

ENV sa_password=FR*@ger12
ENV ACCEPT_EULA=Y

