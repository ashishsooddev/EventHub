# EventHub

## Project Overview

EventHub is a database-driven web application developed using ASP.NET Core MVC and Entity Framework Core. The purpose of the application is to provide a simple platform where users can view events and manage event registrations.

The application uses Entity Framework Core Code-First to create and manage the database. ASP.NET Core Identity is used to provide authentication and role-based authorization for administrators and standard users.

The application is designed to make event management and registration simple while demonstrating the use of MVC, Entity Framework Core, relationships, CRUD operations, and authentication.

---

## Project Purpose

The purpose of EventHub is to provide a centralized application for managing events and event registrations.

The application allows:

- Visitors to view public event information.
- Standard users to register for events and manage their own information.
- Administrators to manage events, categories, registrations, and users.
- Administrators to access functionality that is restricted from standard users.

---

## Technologies Used

- ASP.NET Core MVC
- C#
- Entity Framework Core
- SQL Server
- ASP.NET Core Identity
- HTML
- CSS
- Bootstrap
- Razor Views
- LINQ

---
## Login Credentials

### Admin Account

- **Email:** admin@eventhub.com
- **Password:** Admin@123
- **Role:** Admin

### Standard User Account

- **Email:** user@eventhub.com
- **Password:** User@123
- **Role:** Standard User

## Solution Structure

The solution is divided into four projects:

### EventHub

The main ASP.NET Core MVC project.

Contains:

- Controllers
- Views
- Identity configuration
- Application startup/configuration
- CSS and other UI files

### EventHub.Models

Contains the application's domain models.

Main domain entities:

- Event
- Category
- Registration

### EventHub.DAL

Contains the data access layer.

Includes:

- DbContext
- Entity Framework Core configuration
- Fluent API relationship configuration
- Database-related functionality

### EventHub.BLL

Contains business logic used by the application.

---

## Domain Entities

EventHub uses exactly three primary domain entities.

### Event

Represents an event available on the platform.

Examples of information stored include:

- Event ID
- Event title
- Event date
- Event description
- Category

### Category

Represents a category used to organize events.

Examples:

- Technology
- Sports
- Music
- Education

### Registration

Represents a user's registration for an event.

A registration connects a user with an event.

---

## Entity Relationships

EventHub contains both one-to-many and many-to-many relationships.

### One-to-Many Relationship

One Category can contain many Events.

