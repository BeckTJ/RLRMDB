FROM tjbeck/mssql_server

RUN mkdir -p /usr/raven
WORKDIR /usr/raven

COPY ../../RavenDB/BuildDatabase/CreateDatabase /usr/raven/
COPY ../../RavenDB/DataFiles/BuildFiles /usr/raven/
COPY --chmod=765 ../../entrypoint.sh /usr/raven/
COPY --chmod=765 ../../import-data.sh /usr/raven/

EXPOSE 1433

CMD /bin/bash ./entrypoint.sh