# 🏢 Asset Management System (A.M.S)

An enterprise-grade solution for tracking and managing company assets efficiently. This project demonstrates a robust implementation of **ASP.NET Core MVC** with a Focus on advanced security and administrative control.

## 🚀 Key Features

- **Advanced Authentication:** Fully integrated with **ASP.NET Core Identity**, featuring Email Confirmation and **Social Logins** (Google & Facebook).
- **Comprehensive Administration System:** - **Roles Management:** Full CRUD operations on roles. Supports creating, renaming, and deleting roles, as well as granular permission assignments.
  - **User Management:** Centralized dashboard to manage system users, assign/change roles, and control access levels across the application.
- **Asset Lifecycle Management:** Complete CRUD operations for assets, suppliers, and departments with an intuitive UI.
- **Dynamic Dashboard:** Real-time summary cards for quick insights into total assets, suppliers, and system users.
- **Data Integrity:** Implements **Entity Framework Core** with relational database constraints and automated Data Seeding for a consistent environment.

---

## 🛠️ Tech Stack

- **Backend:** C# | .NET Core MVC | Entity Framework Core
- **Database:** Microsoft SQL Server (MSSQL)
- **Security:** ASP.NET Core Identity | OAuth 2.0 (Google & Facebook)
- **Frontend:** Bootstrap 5 | JavaScript | CSS3 (SCSS) | jQuery

---

## 🏗️ Project Structure

The solution follows a clean, multi-layered architecture:
- **AssetManagementSystem.Web:** The Presentation Layer (MVC) containing Controllers, Views, and UI logic.
- **AssetManagementSystem.Core:** The Domain Layer containing Business Logic and Entities.
- **AssetManagementSystem.Db:** The Data Infrastructure Layer for DB Context and Migrations.

---
