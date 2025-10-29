# Codebridge Dogs API

## Overview
This is a mini REST API project developed as a **technical test assignment** for Codebridge.  
The project allows managing a simple database of dogs with basic CRUD operations, sorting, pagination, and validation.  

The goal of this assignment was to demonstrate proficiency in **ASP.NET Core Web API**, **Entity Framework Core**, **FluentValidation**, and software architecture best practices.

---

## Technology Stack
- **Backend:** ASP.NET Core Web API  
- **ORM:** Entity Framework Core (Code-First)  
- **Database:** Microsoft SQL Server (Dockerized)  
- **Validation:** FluentValidation  
- **Testing:** xUnit  
- **Containerization:** Docker + Docker Compose

## Installation & Run  
### 🔹Local run backend
1. **Clone the repository**
2. **Configure the database connection and JWT settings**
   Open the appsettings.json file and set your own values:

   ```bash
   {
     "Logging": {
         "LogLevel": {
             "Default": "Information",
             "Microsoft.AspNetCore": "Warning"
         }
     },
     "AllowedHosts": "*",
     "ConnectionStrings": {
         "Connection" : "Server=localhost,1433;Database=CodebridgeDogs;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true;"
     }
   }
   ```
   ⚠ Make sure that the database is running locally (you can use SQL Server or a Docker container).
  Replace YourStrong!Passw0rd with your own secure values.

## 🐳 Docker Setup  
### Create `docker-compose.yml` or you can use mine.
In the root folder of your project, create a `docker-compose.yml` file: 
```yaml
﻿services:
  
  sqlserver:
    image: mcr.microsoft.com/mssql/server:2022-latest
    container_name: codebridge_dogs_db
    environment:
      ACCEPT_EULA: "Y"
      MSSQL_SA_PASSWORD: "YourStrong!Passw0rd"
    ports:
      - "1433:1433"
    volumes:
      - dogs_codebridge:/var/opt/mssql
    restart: unless-stopped
  
  codebridgedogs:
    build:
      context: .
      dockerfile: Dockerfile
    container_name: codebridge_dogs
    ports:
      - "8080:8080"
    depends_on:
      - sqlserver
    environment:
      - ConnectionStrings__Connection=Server=codebridge_dogs_db,1433;Database=CodebridgeDogs;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true;
    restart: unless-stopped
      

volumes:
  dogs_codebridge:
