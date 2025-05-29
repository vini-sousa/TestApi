# Midgar API Project

## Description

This project is a RESTful API developed using .NET and C# as part of a backend developer assessment. 
It provides CRUD operations for users and integrates with an external API (PokéAPI) to fetch and display Pokémon data. 
The API also features JWT authentication for protected routes.

## Features

* User management (Create, Read, Update, Delete)
* Integration with PokéAPI to fetch Pokémon data
* JWT-based authentication for secure endpoints
* Layered architecture (API, Application, Domain, Persistence, ExternalServices)
* Entity Framework Core for data persistence
* Swagger UI for API documentation and testing

## Prerequisites

Before you begin, ensure you have the following installed:

* [.NET SDK](https://dotnet.microsoft.com/download) (NET 6.0)
* [Git](https://git-scm.com/downloads)
* A SQL database: [SQLite](https://www.sqlite.org/download.html)
* An IDE like [Visual Studio](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

## Setup and Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/vini-sousa/TestApi.git
    cd TestApi
    ```

2.  **Navigate to the API project folder:**
    The main API project is typically named `Midgar.API`. If you are in the root of the cloned repository, you might navigate like this:
    ```bash
    cd src/Midgar.API
    ```

## Configuration

The main configuration files are `appsettings.json` and `appsettings.Development.json` located in the `Midgar.API` project folder.

1.  **Database Connection String:**
    * Open `Midgar.API/appsettings.Development.json`.
    * The project is likely configured to use SQLite by default. The connection string might look like:
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Data Source=Midgar.db"
        }
        ```

2.  **JWT Settings:**
    * In `Midgar.API/appsettings.Development.json`, ensure the `JwtSettings` are configured, especially the `SecretKey`.
        ```json
        "JwtSettings": {
          "SecretKey": "YOUR_SECRET_KEY_HERE",
          "Issuer": "MidgarAPI",
          "Audience": "MidgarClients"
        }
        ```
    * For local development, the existing key is fine. For a real deployment, use a strong, unique key stored securely.

3.  **Apply Database Migrations:**
    * Open a terminal or command prompt in the root directory of the solution (where your `Midgar.sln` file is) or in the `Midgar.API` project directory.
    * Run the following command to create or update the database schema based on the Entity Framework Core migrations:
        ```bash
        dotnet ef database update --project Midgar.Persistence --startup-project Midgar.API
        ```
        (Adjust `--project` and `--startup-project` paths if your folder structure differs significantly or if you run it from a specific subfolder). If you are already in the `Midgar.API` folder, you might only need:
        ```bash
        dotnet ef database update --project ../Midgar.Persistence
        ```

## Running the API

1.  **Using the .NET CLI:**
    * Navigate to the `Midgar.API` project folder in your terminal.
    * Run the command:
        ```bash
        dotnet watch run
        ```

2.  **Using an IDE:**
    * Open the solution file (`Midgar.sln`) in Visual Studio or open the project folder in VS Code.
    * Run the project (usually by pressing F5 or a "Start Debugging" button) or
    * Run the command on terminal:
        ```bash
        dotnet watch run
        ```

By default, the API will typically be accessible at `https://localhost:7167` (HTTPS) and `http://localhost:5037` (HTTP). Check your terminal output for the exact URLs.

## Running Unit Tests

This project includes unit tests to ensure code quality.

    * Open a terminal in the root directory of the solution (the `src` folder).
    * Run the following command to execute all tests in the solution:
        ```bash
        dotnet test
        ```
    * To run tests for a specific test project (e.g., `Midgar.API.Tests`), you can navigate to its folder or specify the project:
        ```bash
        dotnet test tests/Midgar.API.Tests/Midgar.API.Tests.csproj
        ```

## Accessing API Endpoints & Documentation

Once the API is running, you can access the Swagger UI for interactive documentation and to test the endpoints:

* Navigate to `https://localhost:71670/swagger` (or the appropriate HTTPS URL and port from your terminal output).

## Authentication

Some routes are protected and require a JWT token for access.

1.  **Login to get a token:**
    * Make a `POST` request to the `/api/auth/login` endpoint.
    * **Request Body:**
        ```json
        {
            "username": "testuser",
            "password": "password123"
        }
        ```
    * The response will contain a JWT token if the credentials are valid.

2.  **Accessing Protected Routes:**
    * Copy the token received from the login response.
    * For any protected route (e.g., `GET /api/users/me`), include the token in the `Authorization` header of your request:
        ```
        Authorization: Bearer <YOUR_JWT_TOKEN_HERE>
        ```
    * Swagger UI can be configured to send this header after you authorize it using the "Authorize" button.

---