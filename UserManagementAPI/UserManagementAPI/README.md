# UserManagementAPI (ASP.NET Core Web API)

## Overview
A simple User Management API for TechHive Solutions that supports CRUD operations for users.

## Requirements covered (Coursera Peer-Graded Rubric)
- GitHub repository created (public)
- CRUD endpoints (GET, POST, PUT, DELETE)
- Validation for user input (only valid data is accepted)
- Middleware implemented (error handling + token auth + request logging)
- Copilot used to generate and debug code (notes below)

## How to run
1. Open the project in Visual Studio
2. Press **F5**
3. Swagger UI opens at: `https://localhost:{port}/swagger`

## Authentication (Swagger)
This API uses a simple Bearer token middleware.
1. Click **Authorize** in Swagger
2. Enter: `Bearer dev-token`  
   (or just `dev-token` depending on your Swagger setup)

## Endpoints
Base route: `/api/users`

- `GET /api/users` → returns all users (200)
- `GET /api/users/{id}` → returns user by id (200) or not found (404)
- `POST /api/users` → creates a user (201 Created)
- `PUT /api/users/{id}` → updates user (204 No Content) or not found (404)
- `DELETE /api/users/{id}` → deletes user (204 No Content) or not found (404)

## Validation
User model uses DataAnnotations validation:
- `Name` is required and must be at least 2 characters
- `Email` is required and must be a valid email format

Invalid input returns **400 Bad Request** automatically because the controller uses `[ApiController]`.

## Middleware
Implemented middleware components:
- **ErrorHandlingMiddleware**: catches unhandled exceptions and returns JSON `{ "error": "Internal server error." }`
- **SimpleTokenAuthMiddleware**: requires `Authorization: Bearer <token>` and returns 401 for missing/invalid token
- **RequestLoggingMiddleware**: logs HTTP method, path, and response status code

Middleware order in the pipeline:
1) Error handling  
2) Authentication  
3) Logging  

## Copilot usage notes (examples)
I used Microsoft Copilot to:
- Generate CRUD controller actions and repository scaffolding
- Identify and fix issues like:
  - Missing validation for name/email (added DataAnnotations)
  - Returning proper 404 when user ID does not exist
  - Preventing crashes by adding global error-handling middleware
- Create middleware templates with prompts like:
  - "Generate middleware to log HTTP requests and responses in ASP.NET Core"
  - "Create middleware that catches exceptions and returns consistent JSON errors"
  - "Create token-based authentication middleware using Authorization Bearer header"