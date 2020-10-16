FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build-env
WORKDIR /app

ADD src/Services/Rate/Interest.Rate.API ./src/Services/Rate/Interest.Rate.API/
ADD src/BuildingBlocks/Interest.Core ./src/BuildingBlocks/Interest.Core
RUN ls
RUN dotnet restore src/Services/Rate/Interest.Rate.API/Interest.Rate.API.csproj

RUN dotnet publish src/Services/Rate/Interest.Rate.API -c Release -o out

# Build da imagem
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-alpine
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Interest.Rate.API.dll"]
