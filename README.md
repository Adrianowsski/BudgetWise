# BudgetWise
Personal Finance Manager (​.NET MAUI + Blazor)

📋 Project Overview

**BudgetWise** is a cross-platform mobile application built with **.NET MAUI** and **Blazor** that helps users:

- Track incomes and expenses
  
- Categorize transactions

- Set and monitor monthly budgets
  
- Plan and measure savings goals
  
- Manage recurring subscriptions
  
- Receive local reminders

It works offline-first with seamless synchronization to a custom **ASP.NET Core Web API** backed by **SQL Server**.

## 🚀 Key Features

- **Authentication**
  
  Sign up, log in, and log out using ASP.NET Core Identity with JWT (IdentityServer4)

- **Dashboard**
    
  - Summary cards: Total Expenses, Total Income, Current Balance, Monthly Subscriptions
     
  - Line chart: “Income vs. Expenses” for the current month
      
  - Pie chart: “Expenses by Category”

- **Transactions CRUD**
  
  - **Expenses** and **Incomes** lists with filtering and sorting
     
  - Add/Edit modals capturing:
     
    - Description, Amount, Currency, Date
        
    - Category (Food, Transport, Education, etc.)
       
    - Payment Method (Credit Card, Bank Transfer, Mobile Payment)
      
    - Income Type (Salary, Bonus, Other)

- **Monthly Budgets**
  
  Define per-category spending limits and track usage

- **Custom Categories & Types**
    
  - Create and edit expense categories (with emoji icons)
      
  - Define income types with descriptions

- **Savings Goals**
    
  Set target amounts with deadlines and view progress

- **Subscriptions & Reminders**
  
  - Manage recurring subscriptions (Netflix, Gym, Spotify…)
     
  - Schedule local notifications for upcoming payments or tasks

- **Offline-First & Sync**
   
  - Local caching with SecureStorage and Preferences
    
  - Retry and circuit-breaker policies (Polly)
      
  - Sync to RESTful Web API (Entity Framework Core, SQL Server)

- **Clean Architecture & Patterns**
  
  - MVVM (CommunityToolkit.Mvvm)
      
  - Dependency Injection (Microsoft.Extensions.DependencyInjection)
      
  - MessagingCenter for event-driven updates
    
  - Behaviors & Triggers for UI enhancements

---

## 🛠 Tech Stack

| Layer            | Technology / Library                          |
| ---------------- | --------------------------------------------- |
| **Mobile UI**    | .NET MAUI + Blazor (Razor), C#               |
| **State Management & DI** | CommunityToolkit.Mvvm, DI Container    |
| **Charts & Icons** | Microcharts, FontAwesome / MaterialIcons    |
| **Local Storage** | SecureStorage, Preferences                    |
| **Web API**      | ASP.NET Core 9, Entity Framework Core, SQL Server |
| **Authentication** | ASP.NET Core Identity, IdentityServer4, JWT  |
| **Networking**   | HttpClient, Polly (retry & circuit breaker)   |
| **Testing**      | xUnit, Moq                                    |

---

## ⚙️ Installation & Setup

REST API (ASP.NET Core Web API + SQL Server)

1. **Clone repository**
2. 
   ```bash
   
   git clone https://github.com/YourUsername/BudgetWise.git
   
   cd BudgetWise

Backend (Web API)


-cd src/Backend

-dotnet ef database update

-dotnet run

Mobile App


-Open BudgetWise.sln in Rider or Visual Studio 2022+

-In appsettings.json, set ApiBaseUrl to your API URL

-Deploy to Android/iOS emulator or physical device
