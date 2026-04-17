# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project file and restore dependencies
COPY ["BepNha.Web/BepNha.Web.csproj", "BepNha.Web/"]
RUN dotnet restore "BepNha.Web/BepNha.Web.csproj"

# Copy source code
COPY ["BepNha.Web/", "BepNha.Web/"]

# Build and publish
WORKDIR "/src/BepNha.Web"
RUN dotnet build "BepNha.Web.csproj" -c Release -o /app/build
RUN dotnet publish "BepNha.Web.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Install sqlite3 (if needed for migrations)
RUN apt-get update && apt-get install -y sqlite3 && rm -rf /var/lib/apt/lists/*

# Copy published app from build stage
COPY --from=build /app/publish .

# Create data directory for SQLite persistence
RUN mkdir -p /data

# Expose port
EXPOSE 8080

# Set environment
ENV ASPNETCORE_ENVIRONMENT=Production
ENV ASPNETCORE_URLS=http://+:8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD curl -f http://localhost:8080/ || exit 1

# Run app
ENTRYPOINT ["dotnet", "BepNha.Web.dll"]