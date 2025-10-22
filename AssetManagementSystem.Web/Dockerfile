# Stage 1: Build the application and install tools
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Install SQL tools
RUN apt-get update && \
    apt-get install -y curl apt-transport-https gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/22.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql18 mssql-tools unixodbc-dev && \
    echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc

# Install dotnet-ef tool first
RUN dotnet tool install --global dotnet-ef

# Copy all .csproj files and restore dependencies
COPY ["AssetManagementSystem.Web/AssetManagementSystem.Web.csproj", "AssetManagementSystem.Web/"]
COPY ["AssetManagementSystem.Db/AssetManagementSystem.Db.csproj", "AssetManagementSystem.Db/"]
RUN dotnet restore "AssetManagementSystem.Web/AssetManagementSystem.Web.csproj"

# Copy the rest of the source code
COPY . .
WORKDIR "/src/AssetManagementSystem.Web"
RUN dotnet build "AssetManagementSystem.Web.csproj" -c Release -o /app/build

# Stage 2: Publish the application
FROM build AS publish
RUN dotnet publish "AssetManagementSystem.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Create the final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Copy the installed ef tool from the build stage
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools

# Add the tools to the PATH
ENV PATH="${PATH}:/root/.dotnet/tools"

ENTRYPOINT ["dotnet", "AssetManagementSystem.Web.dll"]