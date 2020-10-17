FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env

RUN apk add icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app

ADD ./src app

RUN dotnet restore app/JacksonVeroneze.ViaCep.API/JacksonVeroneze.ViaCep.API.csproj

RUN dotnet publish app/JacksonVeroneze.ViaCep.API/JacksonVeroneze.ViaCep.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "JacksonVeroneze.ViaCep.API.dll"]
