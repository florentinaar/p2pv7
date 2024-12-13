# P2Pv7 Project

# Overview

The P2Pv7 project is a B2B Postal management system multi-layered application built with a modular architecture to enhance scalability, maintainability, and separation of concerns. It includes three core projects:

- P2Pv7.Common: Contains shared utilities and helper classes.
- P2Pv7.Data: Manages data access and defines entities, DTOs, and database configurations.
- P2Pv7.Services: Implements business logic and services for various functionalities.

## Features
- Role and Authentication Management
- Order Processing and Management
- Email Service Integration
- Data Mappings
- Printing Services
  * Identity for autherization, providing jwt tokens. Cors enabled. Swagger UI for documentation. Entity framework, AutoMapper DTOs
# Key Components

### 1. P2Pv7.Common

Holds common functionalities, such as utility classes (e.g., Check.cs), that can be shared across different layers of the application.

### 2. P2Pv7.Data

This project manages data-related operations:

Entities: Define the database schema using Entity Framework Core.

DTOs: Serve as data transfer objects to encapsulate and structure data.

Enums: Define fixed sets of constants, such as Role and Status.

DataContext.cs: Configures the database context and manages migrations.

### 3. P2Pv7.Services

Contains services implementing the application's core business logic:

AuthService: Manages authentication and user-related operations.

OrderService: Handles order creation, updates, and retrieval.

RolesService: Implements role management and permissions.

EmailService: Provides email functionality.

PrintService: Manages document generation and printing tasks.

Mappings: Contains MapperInitializer.cs for object-to-object mappings using libraries like AutoMapper.

### 4. P2Pv7

Controllers: Handle HTTP requests and map them to appropriate service methods.

appsettings.json: Stores configuration settings such as database connections.

Program.cs: Configures and initializes the application at runtime.

### 5. Tests

Unit and integration tests for various services and controllers.
Prerequisites

.NET SDK 6.0 or higher

SQL Server

Visual Studio or compatible IDE
