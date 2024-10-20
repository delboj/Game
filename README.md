# .NET 8 Web API Project

## Description

This is a .NET 8 Web API project built using clean architecture principles, with partial Domain-Driven Design (DDD) implementation. The API is designed to manage business logic and data through various layers and follows best practices for maintainability and scalability.

The application uses **PostgreSQL** as the database, **Seq** for logging, and **Serilog** as the logging framework. We also use **MediatR** for handling CQRS (Command Query Responsibility Segregation) patterns and **Entity Framework (EF)** as the Object-Relational Mapping (ORM) tool.

API versioning is implemented with Swagger documentation for easy testing and interaction. The project is containerized using **Docker Compose** and can be run seamlessly in **Visual Studio**.

---

## Prerequisites

Before running the project, make sure you have the following tools installed:

- **.NET 8 SDK**: Download and install the latest .NET SDK [here](https://dotnet.microsoft.com/download/dotnet).
- **Docker**: Install Docker for your platform from [here](https://www.docker.com/get-started).
- **Visual Studio 2022**: Visual Studio with Docker support is required for seamless development and debugging. [Download Visual Studio](https://visualstudio.microsoft.com/).

---

## Installation

Follow these steps to set up and run the project locally using Docker Compose:

1. **Clone the repository**:

   ```bash
   git clone https://github.com/delboj/Game.git
   cd project-name
   ```

2. **Build the Docker containers**:

   The project includes a `docker-compose.yml` file that will build and run the necessary containers for the application (API, PostgreSQL, and Seq).

   - Ensure Docker is running on your machine.
   - From the project root, run the following command to start the containers:

   ```bash
   docker-compose up --build
   ```

   This will:

   - Build the Docker images for the API and database.
   - Start a PostgreSQL container.
   - Start a Seq container for logging.
   - Launch the .NET Web API.

3. **Set up the database**:

   - The application uses **Entity Framework** (EF) for migrations and database updates. Migrations are automatically applied when the application starts.

   Alternatively, you can run the migrations manually using:

   ```bash
   docker exec -it <api_container_name> dotnet ef database update
   ```

4. **Run the application in Visual Studio**:

   Open the solution in **Visual Studio**. Set **docker-compose** as startup project. Press **F5** or **Ctrl+F5** to start the application. Docker Compose will manage the container lifecycle automatically.

---

## Configuration

The application configuration, such as connection strings, logging settings, and Docker service configurations, are managed in the following files:

- **appsettings.json**: Core application settings.
- **docker-compose.yml**: Defines the services (API, PostgreSQL, Seq) and their configurations.
- **Dockerfile**: Used for building the .NET Web API container.

Make sure the following environment variables are set for production environments

## Running the Application

Once the application is up and running, you can access the API via `http://localhost:5000`.

### Swagger Documentation

The API includes **Swagger** for self-generated API documentation.
Once the application is running, open the Swagger UI by navigating to:`http://localhost:5000/swagger/index.html`
