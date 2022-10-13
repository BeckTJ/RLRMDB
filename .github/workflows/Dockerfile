FROM tjbeck/mssql_server

RUN mkdir -p /usr/raven
WORKDIR /usr/raven

COPY ../../RavenDB/BuildDatabase/CreateDatabase /usr/raven/
COPY ../../RavenDB/DataFiles/BuildFiles /usr/raven/

RUN chmod +x /usr/work/import-data.sh

EXPOSE 1433

CMD /bin/bash ./entrypoint.sh