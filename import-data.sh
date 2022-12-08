sleep 90s

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P FR*@ger12 -i buildDatabase.sql

/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P FR*@ger12 -i buildProcedures.sql