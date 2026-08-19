# 🔐 Employee Management API

> A production-inspired ASP.NET Core Web API built to learn and implement modern authentication and authorization concepts from the ground up.

![.NET](https://img.shields.io/badge/.NET-10-purple)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-16-blue)
![JWT](https://img.shields.io/badge/JWT-Authentication-success)
![License](https://img.shields.io/badge/License-MIT-green)

---

## 📖 Overview

This project started as a simple JWT authentication API and gradually evolved into a production-style authentication system.

Instead of relying on ASP.NET Core Identity, the goal was to **understand and implement the underlying concepts manually**, including:

- JWT Authentication
- Refresh Token Rotation
- Password Hashing
- Repository Pattern
- EF Core
- PostgreSQL
- Exception Handling
- FluentValidation
- Claims-based Authentication

The project focuses on understanding **how authentication systems work internally** rather than simply using framework abstractions.

---

# 🚀 Tech Stack

| Category | Technology |
|----------|------------|
| Backend | ASP.NET Core (.NET 10) |
| Language | C# |
| ORM | Entity Framework Core |
| Database | PostgreSQL |
| Authentication | JWT Bearer |
| Password Hashing | BCrypt |
| Validation | FluentValidation |
| API Documentation | Swagger / OpenAPI |
| Dependency Injection | Built-in .NET DI |

---

# 📂 Project Structure

```text
EmployeeManagement.Api
│
├── Configuration
├── Contexts
├── Controllers
├── Exceptions
├── Middlewares
├── Models
│   ├── Entities
│   ├── Requests
│   └── Responses
├── Repositories
├── Services
├── Validators
└── Program.cs
```

---

# 🏗️ Architecture

```text
                HTTP Request
                     │
                     ▼
               Authentication
                     │
                     ▼
              Authorization
                     │
                     ▼
               Auth Controller
                     │
                     ▼
               FluentValidation
                     │
                     ▼
                Auth Service
          ┌──────────┼──────────┐
          ▼          ▼          ▼
 UserRepository TokenService PasswordHasher
          │          │
          ▼          ▼
     PostgreSQL   JWT Generator
```

---

# 🔑 Features Implemented

## Authentication

- User Registration
- User Login
- JWT Access Token Generation
- Refresh Token Generation
- Refresh Token Rotation
- Logout
- Current User Service

---

## Security

- BCrypt Password Hashing
- Refresh Token Persistence
- Token Revocation
- Claims-based Authentication
- Role Claims
- JWT Signature Validation

---

## Data Layer

- Entity Framework Core
- PostgreSQL
- Repository Pattern
- Refresh Token Entity
- User Entity

---

## API

- Swagger Documentation
- JWT Swagger Authentication
- RESTful Endpoints

---

## Validation

- FluentValidation
- Request Validation
- Business Validation
- Centralized Exception Handling

---

# 🔄 Authentication Flow

## Register

```text
Client
   │
   ▼
Register API
   │
   ▼
Validate Request
   │
   ▼
Hash Password (BCrypt)
   │
   ▼
Save User
```

---

## Login

```text
Client
   │
   ▼
Login API
   │
   ▼
Validate Password
   │
   ▼
Generate JWT
   │
   ▼
Generate Refresh Token
   │
   ▼
Persist Refresh Token
   │
   ▼
Return Tokens
```

---

## Refresh Token

```text
Client
   │
   ▼
Refresh API
   │
   ▼
Validate Refresh Token
   │
   ▼
Revoke Previous Token
   │
   ▼
Generate New JWT
   │
   ▼
Generate New Refresh Token
```

---

## Logout

```text
Client
   │
   ▼
JWT Authentication
   │
   ▼
Current User
   │
   ▼
Validate Refresh Token Ownership
   │
   ▼
Revoke Refresh Token
```

---

# 📚 Key Concepts Learned

This project focuses on understanding:

- Stateless Authentication
- Access Token vs Refresh Token
- Refresh Token Rotation
- Token Revocation
- Claims
- JWT Signature Validation
- BCrypt Password Hashing
- Authentication vs Authorization
- Dependency Injection
- Repository Pattern
- Middleware Pipeline
- Entity Framework Core
- PostgreSQL Integration

---

# 📌 API Endpoints

| Method | Endpoint | Description |
|---------|----------|-------------|
| POST | `/api/auth/register` | Register new user |
| POST | `/api/auth/login` | Login |
| POST | `/api/auth/refresh` | Refresh access token |
| POST | `/api/auth/logout` | Logout |

---

# 🛡️ Security Decisions

### Password Storage

Passwords are never stored in plain text.

- BCrypt
- Random Salt
- Secure Verification

---

### JWT

Access Tokens are intentionally short-lived.

Refresh Tokens are persisted in PostgreSQL and revoked on logout.

---

### Refresh Token Rotation

Every refresh request generates a new Refresh Token while revoking the previous one.

This reduces replay attack risks.

---

# 🚧 Planned Features

- [x] JWT Authentication
- [x] Refresh Tokens
- [x] BCrypt Password Hashing
- [x] Logout
- [x] FluentValidation
- [ ] Logout from All Devices
- [ ] Role Based Authorization
- [ ] Policy Based Authorization
- [ ] Resource Based Authorization
- [ ] Forgot Password
- [ ] Reset Password
- [ ] Email Verification
- [ ] Sliding Refresh Tokens
- [ ] Refresh Token Reuse Detection
- [ ] Secure HttpOnly Refresh Cookies
- [ ] OAuth2
- [ ] OpenID Connect
- [ ] ASP.NET Core Identity
- [ ] Google Login
- [ ] GitHub Login
- [ ] Azure AD Authentication

---

# 🎯 Learning Goals

The purpose of this project is to deeply understand how authentication systems are designed rather than relying on built-in frameworks.

Every feature is implemented incrementally to understand:

- Why it exists
- How it works internally
- Security implications
- Production best practices

---

# 📖 Future Enhancements

Beyond authentication, the project will evolve into a production-ready backend with:

- Clean Architecture
- CQRS + MediatR
- Redis Caching
- Docker
- Unit Testing
- Integration Testing
- GitHub Actions CI/CD
- Serilog
- API Versioning
- Health Checks
- Performance Optimization

---

## ⭐ Repository Goal

This repository serves as a learning project to explore enterprise-grade ASP.NET Core backend development, with a strong emphasis on authentication, security, and software architecture.
