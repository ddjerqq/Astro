﻿FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
RUN apt update
RUN apt install -y curl

RUN curl -sLO https://github.com/tailwindlabs/tailwindcss/releases/latest/download/tailwindcss-linux-x64
RUN chmod +x tailwindcss-linux-x64
RUN mv tailwindcss-linux-x64 /usr/bin/tailwindcss

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
COPY --from=base /usr/bin/tailwindcss /usr/bin/tailwindcss

WORKDIR /src

COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Presentation/Presentation.csproj", "Presentation/"]
COPY ["WebAPI/WebAPI.csproj", "WebAPI/"]

RUN dotnet restore "Domain/Domain.csproj"
RUN dotnet restore "Application/Application.csproj"
RUN dotnet restore "Infrastructure/Infrastructure.csproj"
RUN dotnet restore "Presentation/Presentation.csproj"
RUN dotnet restore "WebAPI/WebAPI.csproj"

COPY . .

RUN dotnet build "Domain/Domain.csproj" -c Release -o /app/build
RUN dotnet build "Application/Application.csproj" -c Release -o /app/build
RUN dotnet build "Infrastructure/Infrastructure.csproj" -c Release -o /app/build
RUN dotnet build "Presentation/Presentation.csproj" -c Release -o /app/build
RUN dotnet build "WebAPI/WebAPI.csproj" -c Release -o /app/build

RUN dotnet publish "Domain/Domain.csproj" -c Release -o /app/publish
RUN dotnet publish "Application/Application.csproj" -c Release -o /app/publish
RUN dotnet publish "Infrastructure/Infrastructure.csproj" -c Release -o /app/publish
RUN dotnet publish "Presentation/Presentation.csproj" -c Release -o /app/publish
RUN dotnet publish "WebAPI/WebAPI.csproj" -c Release -o /app/publish

WORKDIR /src/WebAPI
RUN /usr/bin/tailwindcss -i ./wwwroot/app.css -o ./wwwroot/app.min.css --minify

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=10s CMD curl --silent --fail https://localhost/health || exit 1

ENTRYPOINT ["dotnet", "WebAPI.dll"]
