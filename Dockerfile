# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /build

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o /publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install SQLite tool for debugging
RUN apt-get update && apt-get install -y sqlite3 curl && rm -rf /var/lib/apt/lists/*

# Create persistent data directory (IMPORTANT!)
RUN mkdir -p /data && chmod 755 /data

# Copy published app
COPY --from=build /publish .

# Environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production
ENV SQLITE_FILE=/data/app.db

EXPOSE 8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=10s --retries=3 \
    CMD curl -f http://localhost:8080/ || exit 1

ENTRYPOINT ["dotnet", "BepNha.Web.dll"]