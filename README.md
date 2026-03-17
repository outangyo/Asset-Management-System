# 🏢 Asset Management System

An enterprise-grade solution for tracking and managing company assets efficiently. This project demonstrates a robust implementation of **ASP.NET Core MVC** with a Focus on advanced security and administrative control.

👉 **[View Project Presentation & Screenshots (PDF)](./AssetManagement_Project_Preview.pdf)**

## 🚀 Key Features

- **Advanced Authentication:** Fully integrated with **ASP.NET Core Identity**, featuring Email Confirmation and **Social Logins** (Google & Facebook).
- **Comprehensive Administration System:** - **Roles Management & Users Management:** Full CRUD operations on roles. Supports creating, renaming, and deleting roles, as well as granular permission assignments.
  - **User Management:** Centralized dashboard to manage system users, assign/change roles, and control access levels across the application.
    
  - **Roles Administration & Access Control (RBAC):** Manage application roles (CRUD) with status toggles, track assigned users in real-time, and secure application routing with page-level authorization.
      - Dynamic Roles: Full CRUD operations with Active/Inactive toggles.
      - User Mapping: Instantly view user counts and details assigned to each role.
      - Page-Level Security: Secure application routes and views based on roles.

  - **Users Administration & Role Assignment:** A centralized dashboard for user management, featuring intuitive multi-role assignments and detailed user profiles for quick security audits.
      - User Management: Centralized dashboard with search, filter, and CRUD capabilities.
      - Role Assignment: Intuitive checkbox interface for assigning multiple roles quickly.
      - User Profiles: Detailed views for transparent tracking and security audits.
   
- **Asset Lifecycle Management:** Complete CRUD operations for assets, suppliers, and departments with an intuitive UI.
- **Dynamic Dashboard:** Real-time summary cards for quick insights into total assets, suppliers, and system users.
- **Data Integrity:** Implements **Entity Framework Core** with relational database constraints and automated Data Seeding for a consistent environment.

---

## 🛠️ Tech Stack

- **Backend:** C# | .NET Core MVC | Entity Framework Core
- **Database:** Microsoft SQL Server (MSSQL)
- **Security:** ASP.NET Core Identity | OAuth 2.0 (Google & Facebook)
- **Frontend:** HTML | CSS | JavaScript | jQuery | Bootstrap 5

---

## 🏗️ Project Structure

The solution follows a clean, multi-layered architecture:
- **AssetManagementSystem.Web:** The Presentation Layer (MVC) containing Controllers, Views, and UI logic.
- **AssetManagementSystem.Core:** The Domain Layer containing Business Logic and Entities.
- **AssetManagementSystem.Db:** The Data Infrastructure Layer for DB Context and Migrations.

---

## 📝 Important Note for Local Development

- **Database & Data Seeding:** Currently, this project is configured for my local development environment. If cloned and run on a new machine, the database will be created but will initially be empty (as the user mock data resides on my local SQL Server). 
- **Docker / Containerization:** The project does not yet utilize Docker containers. 
