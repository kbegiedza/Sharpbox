FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
RUN curl -fsSL https://deb.nodesource.com/setup_lts.x | bash - && \
    apt install -y \
    nodejs

WORKDIR /app

COPY Sharpbox.sln ./
COPY src/Sharpbox.Client/*.csproj ./src/Sharpbox.Client/
COPY src/Sharpbox.Demo/*.csproj ./src/Sharpbox.Demo/
COPY src/Sharpbox.Graceful/*.csproj ./src/Sharpbox.Graceful/
COPY src/Sharpbox.Grains/*.csproj ./src/Sharpbox.Grains/
COPY src/Sharpbox.Performance/*.csproj ./src/Sharpbox.Performance/
COPY src/Sharpbox.Silo/*.csproj ./src/Sharpbox.Silo/

RUN dotnet restore --no-cache

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build-env /app/out .