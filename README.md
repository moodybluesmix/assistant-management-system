# Assistant-Management-System
The Assistant On-Call Duty Management System (AsistanNobetYonetimi) is an ASP.NET Core MVC-based web application developed to facilitate the duty (on-call) scheduling of pediatric residents (assistants) in hospitals.

The system offers the following supports:
- User Management
- Duty Creation (Shift Scheduling)
- Calendar-Based Duty Viewing
- Resident-to-Resident Duty Swapping/Exchange
- Admin Panel Support


# Key Features
🗓 Calendar-Based Duty Scheduling

Creation and modification of monthly on-call duty schedules for residents.

👥 Role-Based User Management

Authority-based access with roles such as Admin, Resident, etc.

🔄 Duty Exchange Request System

Residents can create duty swap requests, which can be approved by the Admin.

📊 Reporting and Analytics

Tracking and analysis of residents' total duty count, duty types, etc.

🔐 Secure Authentication

Secure login and session management using Authentication + Cookies.

🛠 Easily Extensible Architecture

Layered structure following the Controller–Service–Repository pattern.



# Backend
- ASP.NET Core 6 MVC
- Entity Framework Core 6
- Repository Pattern
- Model-View-Controller (MVC) Architecture

# Database
- Microsoft SQL Server
- EF Core Code First + Migrations

# Frontend
- Bootstrap 5
- HTML5 / CSS3
- Razor View Engine
- FullCalendar (Optional, for duty calendar visualization)

# Tools & Development
- Visual Studio 2022
- Git / GitHub
- LINQ
- Dependency Injection
