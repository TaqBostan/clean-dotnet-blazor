# Clean .NET Blazor Boilerplate

A **production-ready boilerplate** template designed to kickstart your projects using a modern, clean, and scalable stack.

## Key Features

- **Test-Driven Development (TDD)**
- **Behavior-Driven Development (BDD)**
- **Domain-Driven Design (DDD)**
- **Clean Architecture**
- **Blazor Web UI**
- **Entity Framework (EF) Migrations**
- **Docker & Docker Compose**
- Multi-layered architecture for maintainability and scalability

## Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

## Getting Started

### Running EF Migrations

For the initial migration, run:

```bash
dotnet ef migrations add Init --project ./Infrastructure --startup-project ./Server --output-dir ./Data/Migrations
```

For subsequent migrations, update the database with:

```bash
dotnet ef database update --context ApplicationDbContext --project ./Infrastructure --startup-project ./Server
```

### Running the Application

Start the full solution with Docker Compose:

```bash
docker-compose up
```

### Accessing the Database with pgAdmin

This boilerplate uses **pgAdmin** for managing the PostgreSQL database.

1. Open your browser and navigate to:  
   [http://localhost:8888/browser/](http://localhost:8888/browser/)

2. Login with:  
   - **Username:** user-name@domain-name.com  
   - **Password:** postgres123

3. Register a new server in pgAdmin:  
   - Go to **Servers** → **Register** → **Server...**

4. Enter the following connection details:

   - **General Tab**  
     Name: `db_host`

   - **Connection Tab**  
     - Host name/address: `db_host`  
     - Port: `5432`  
     - Maintenance database: `postgres`  
     - Username: `postgres`  
     - Password: `postgres123`
