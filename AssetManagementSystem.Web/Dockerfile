# Stage 1: Build the application (and install tools)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# vvvvvvvvvv ย้ายคำสั่งติดตั้งมาไว้ที่นี่ vvvvvvvvvv
# Install dotnet-ef tool
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"
# ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

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

# vvvvvvvvvv คัดลอก tool ที่ติดตั้งแล้วมาจาก Stage 1 vvvvvvvvvv
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools
# ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

ENTRYPOINT ["dotnet", "AssetManagementSystem.Web.dll"]