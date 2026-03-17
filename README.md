# 🏢 Asset Management System (A.M.S)

An enterprise-grade solution for tracking and managing company assets efficiently. This project demonstrates a robust implementation of **ASP.NET Core MVC** with a Focus on advanced security and administrative control.

## 🚀 Key Features

- **Advanced Authentication:** Fully integrated with **ASP.NET Core Identity**, featuring Email Confirmation and **Social Logins** (Google & Facebook).
- **Comprehensive Administration System:** - **Roles Management & Users Management:** Full CRUD operations on roles. Supports creating, renaming, and deleting roles, as well as granular permission assignments.
  - **User Management:** Centralized dashboard to manage system users, assign/change roles, and control access levels across the application.
    
  - **Roles Administration & Access Control:**
A structural-level security and Role-Based Access Control (RBAC) system.
Dynamic Role Management: Full CRUD operations for application roles. Seamlessly customize role names, descriptions, and toggle active/inactive statuses.
User-Role Mapping: An insightful preview interface that instantly displays the total count, names, and emails of users assigned to each specific role.
Page-Level Authorization: Practical implementation of roles to secure application routing and views. Effectively restricts unauthorized access, such as preventing Guest users from viewing Admin-exclusive sensitive data.

  - **Users Administration & Role Assignment:**
A comprehensive account management system focusing on user-level authorization.
Comprehensive User Management: A centralized dashboard for complete user CRUD operations, featuring built-in search, status filtering, and email confirmation tracking.
Dynamic Role Assignment: An intuitive interface for assigning specific permissions to individual users. Supports multiple role assignments via checkboxes, complete with quick 'Select All' and 'Clear All' functionalities.
Detailed User Profile: In-depth profile views displaying contact details, account information, and currently assigned roles to ensure system transparency and simplify security audits.

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
