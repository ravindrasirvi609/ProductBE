# ProductBE

ProductBE is a backend application for managing products with CRUD operations and image upload functionality. This README provides instructions on how to set up and run the application.

## Prerequisites

Before running the application, ensure you have the following prerequisites installed on your machine:

- [.NET Core SDK](https://dotnet.microsoft.com/download): Ensure you have .NET Core SDK installed to build and run the application.
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads): SQL Server is required for database storage. Alternatively, you can use SQLite for development purposes.

## Setup

1. **Open the Application ZIP file**

2. **Database Configuration**:

- Ensure SQL Server is running, or configure the connection string in `ProductDbContext.cs` located in the `Models` directory to match your database setup.

3. **Add Dependencies**:

- Add the following dependencies to your project:
  - Microsoft.EntityFrameworkCore
  - Microsoft.EntityFrameworkCore.SqlServer
- You can add dependencies using the .NET CLI:
  ```
  dotnet add package Microsoft.EntityFrameworkCore
  dotnet add package Microsoft.EntityFrameworkCore.SqlServer
  ```

4. **Apply Migrations**:

- Run the commands to apply migrations and update the database schema:

## Running the Application

- To run the application, execute the following command:

## Testing APIs with Swagger

- After running the application, you can access the API documentation using Swagger.
- Navigate to the following URL in your browser:
- Swagger provides a user-friendly interface to test and interact with the available APIs.

## Troubleshooting

- If you encounter any issues during setup or while running the application, refer to error messages for troubleshooting.
- Ensure that your database connection string is correct and that SQL Server is running if you're using it for database storage.
