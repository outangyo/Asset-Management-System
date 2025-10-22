# Use the official SQL Server 2019 image as the base
FROM mcr.microsoft.com/mssql/server:2019-latest

# Switch to the root user to install packages
USER root

# Install the required command-line tools for the healthcheck
RUN apt-get update && \
    apt-get install -y curl gnupg && \
    curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list > /etc/apt/sources.list.d/mssql-release.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y mssql-tools unixodbc-dev

# Switch back to the default mssql user for running the database
USER mssql