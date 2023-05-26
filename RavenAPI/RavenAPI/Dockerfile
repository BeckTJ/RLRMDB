FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /RavenAPI

COPY . ./
RUN dotnet restore

RUN dotnet publish -c Release -o out


FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /RavenAPI
COPY --from=build-env /RavenAPI/out .
ENTRYPOINT ["dotnet", "RavenAPI.dll"]