"# Micro" 
# Product Management System

Product Management System is an educational and portfolio-oriented backend project designed to simulate the structure of an enterprise-level application.

The project is developed with **.NET 10** and **ASP.NET Core Web API**, with a focus on clean architecture, separation of concerns, scalability, and maintainability.

The main goal of this project is to implement and practice advanced backend development concepts in a real-world-style structure, including:

- Clean Architecture
- CQRS
- Repository Pattern
- Unit of Work
- JWT Authentication
- API Gateway
- Microservice-based communication
- Entity Framework Core
- SQL Server
- Serilog Logging

The system is composed of two independent services:

- **Product Service (P1):** The main service responsible for business logic, data management, authentication, and communication with SQL Server.
- **API Gateway (P2):** A lightweight gateway responsible for receiving client requests, validating JWT tokens, forwarding requests to the Product Service, and passing authorization headers.

This project is developed step by step as a practical learning path for building scalable backend systems using modern .NET technologies.
