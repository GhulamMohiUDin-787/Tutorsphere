# TutorSphere — Final Project Presentation Report

**Project Name:** TutorSphere (Online Tutoring Platform)  
**Technology Stack:** ASP.NET Core MVC (.NET 10), Entity Framework Core, SQLite, Razor Views, Bootstrap  
**Date:** June 21, 2026  
**Status:** ✅ Built, Connected & Running

---

## 1. Executive Summary

TutorSphere is a full-stack web application that connects students with expert tutors. Users can browse tutors, learn about the platform, and book tutoring sessions online. The project uses the **ASP.NET Core MVC** architecture where the **frontend (Razor Views + CSS/JavaScript)** and **backend (Controllers + Database)** are integrated in a single application, communicating through HTTP requests and form submissions.

---

## 2. Project Architecture

```
┌─────────────────────────────────────────────────────────────┐
│                     FRONTEND (Presentation)                  │
│  Razor Views (.cshtml)  │  CSS  │  JavaScript  │  Bootstrap │
├─────────────────────────────────────────────────────────────┤
│                     BACKEND (Application)                    │
│  Controllers  │  Models  │  Validation  │  Routing           │
├─────────────────────────────────────────────────────────────┤
│                     DATA LAYER                               │
│  Entity Framework Core  │  SQLite Database (app.db)          │
└─────────────────────────────────────────────────────────────┘
```

### Frontend–Backend Connection

| Frontend Page | Controller | Backend Action | Connection Type |
|---------------|------------|----------------|-----------------|
| Home (`/`) | `HomeController` | `Index()` | View rendering |
| Tutors (`/Tutors`) | `TutorsController` | `Index()` | View rendering |
| Booking (`/Booking`) | `BookingController` | `Index()` GET/POST | Form → Database |
| About (`/Info/About`) | `InfoController` | `About()` | View rendering |
| Contact (`/Info/Contact`) | `InfoController` | `Contact()` | View rendering |
| How It Works (`/Info/HowItWorks`) | `InfoController` | `HowItWorks()` | View rendering |

The **Booking** page is the primary frontend–backend integration point: the user fills a form on the frontend, which POSTs data to `BookingController`, validates it, and saves it to the SQLite database via Entity Framework Core.

---

## 3. Build Status

| Component | Status | Details |
|-----------|--------|---------|
| Backend Build | ✅ **Already Built** | `bin/Debug/net10.0/p1.dll` and `p1.exe` present |
| Frontend Assets | ✅ Ready | Views, CSS, Bootstrap, jQuery in `wwwroot/` |
| Database | ✅ Connected | SQLite `app.db` auto-created on first run |
| Server | ✅ Running | `http://localhost:5172` |

### Build Output Location
```
p1/bin/Debug/net10.0/
├── p1.exe          ← Application executable
├── p1.dll          ← Compiled backend assembly
├── app.db          ← SQLite database (created at runtime)
└── appsettings.json
```

---

## 4. Key Features Implemented

### 4.1 Home Page
- Modern hero section with animated gradients
- Statistics strip (50K+ students, 98% satisfaction)
- Feature cards explaining platform benefits
- Step-by-step "How It Works" section
- Call-to-action for booking

### 4.2 Tutors Page
- Search and filter UI for tutors by subject
- Tutor cards with ratings, subjects, and pricing
- Responsive grid layout

### 4.3 Booking System (Core Backend Feature)
- **Form fields:** Full Name, Email, Subject, Date, Time, Duration
- **Client-side:** Duration pills, live price calculator, date validation
- **Server-side:** Model validation, anti-forgery token, database persistence
- **Success feedback:** Confirmation message after booking

### 4.4 Information Pages
- About Us, Contact, How It Works pages
- Consistent navigation and branding

---

## 5. Database Schema

**Table: Bookings**

| Column | Type | Description |
|--------|------|-------------|
| Id | int (PK) | Auto-generated primary key |
| FullName | string | Student's full name |
| Email | string | Contact email |
| Subject | string | Tutoring subject |
| Date | string | Session date |
| Time | string | Session time |
| Duration | int | Session length in minutes |
| CreatedAt | DateTime | Booking timestamp |

---

## 6. Technology Details

| Layer | Technology | Version |
|-------|-----------|---------|
| Framework | .NET | 10.0 |
| Web Framework | ASP.NET Core MVC | 10.0 |
| ORM | Entity Framework Core | 10.0.9 |
| Database | SQLite | Embedded |
| UI Framework | Bootstrap | 5.x |
| Fonts | Google Fonts (Playfair Display, DM Sans) | — |

---

## 7. How to Run the Project

### Option A — Run Pre-built Executable
```powershell
cd "c:\Users\HP\Desktop\vp project\p1\bin\Debug\net10.0"
$env:ASPNETCORE_URLS="http://localhost:5172"
.\p1.exe
```

### Option B — Run with .NET CLI (requires .NET SDK in PATH)
```powershell
cd "c:\Users\HP\Desktop\vp project\p1"
dotnet build
dotnet run --urls "http://localhost:5172"
```

### Open in Browser
Navigate to: **http://localhost:5172**

---

## 8. Testing Results (Verified June 21, 2026)

| Test | URL | Result |
|------|-----|--------|
| Home page loads | `http://localhost:5172/` | ✅ HTTP 200 |
| Tutors page loads | `http://localhost:5172/Tutors` | ✅ HTTP 200 |
| Booking page loads | `http://localhost:5172/Booking` | ✅ HTTP 200 |
| About page loads | `http://localhost:5172/Info/About` | ✅ HTTP 200 |
| Contact page loads | `http://localhost:5172/Info/Contact` | ✅ HTTP 200 |
| How It Works loads | `http://localhost:5172/Info/HowItWorks` | ✅ HTTP 200 |
| Booking form POST | POST `/Booking` | ✅ Redirect + DB save |

---

## 9. Project File Structure

```
p1/
├── Controllers/
│   ├── HomeController.cs       # Home & Privacy pages
│   ├── TutorsController.cs     # Tutor listing
│   ├── BookingController.cs    # Booking form (GET/POST)
│   └── InfoController.cs       # About, Contact, HowItWorks
├── Models/
│   ├── Booking.cs              # Booking entity with validation
│   ├── AppDbContext.cs         # EF Core database context
│   └── ErrorViewModel.cs
├── Views/
│   ├── Home/Index.cshtml       # Landing page
│   ├── Tutors/Index.cshtml     # Tutors listing
│   ├── Booking/Index.cshtml    # Booking form
│   ├── Info/                   # About, Contact, HowItWorks
│   └── Shared/_Layout.cshtml   # Master layout
├── wwwroot/
│   ├── css/site.css
│   ├── js/site.js
│   └── lib/                    # Bootstrap, jQuery
├── Program.cs                  # App startup & DI configuration
├── appsettings.json            # Connection strings & config
└── p1.csproj                   # Project file
```

---

## 10. Demo Flow for Presentation

1. **Open Home Page** — Show the hero section and platform overview
2. **Navigate to Tutors** — Demonstrate tutor browsing UI
3. **Click "Book Now"** — Open the booking form
4. **Fill the form** — Enter name, email, subject, date, time, duration
5. **Submit** — Show success confirmation message
6. **Explain backend** — Data saved to SQLite via Entity Framework Core
7. **Show Info pages** — About, Contact, How It Works

---

## 11. Conclusion

TutorSphere successfully demonstrates a complete full-stack web application with:
- A polished, responsive frontend UI
- A functional backend with database persistence
- Proper MVC separation of concerns
- Form validation and security (anti-forgery tokens)
- End-to-end booking workflow from UI to database

The application is **built, connected, tested, and ready for demonstration**.

---

*Report generated for Final Project Presentation — TutorSphere*
