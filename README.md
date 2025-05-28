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


## 🗂️ Screenshots Overview

Below is a full breakdown of all views and components in **BudgetWise**, organized by feature area. Store images under `assets/screenshots/` and reference them by the given filenames.

---

### 1. Pre-Authentication

| # | Screenshot                                             | Filename                    | Description                                                        |
| - | ------------------------------------------------------ | --------------------------- | ------------------------------------------------------------------ |
| 1 | ![Drawer - Logged Out](assets/screenshots/prelogin-drawer.png) | `prelogin-drawer.png`       | Side menu on welcome screen, with **Login** / **Register** options |
| 2 | ![Login Form](assets/screenshots/login.png)            | `login.png`                 | Email & password form, plus link to registration                   |
| 3 | ![Register Form](assets/screenshots/register.png)      | `register.png`              | New account form: Email, Password, Confirm Password                |
| 4 | ![Login Error](assets/screenshots/login-error.png)     | `login-error.png`           | Error banner “Invalid credentials” on failed login                 |

---

### 2. Dashboard

| # | Screenshot                                          | Filename                     | Description                                                                                       |
| - | --------------------------------------------------- | ---------------------------- | ------------------------------------------------------------------------------------------------- |
| 5 | ![Summary Cards](assets/screenshots/dashboard-summary.png) | `dashboard-summary.png`      | Cards showing Total Expenses, Total Income, Subs/mo., Balance                                      |
| 6 | ![Income vs Expenses](assets/screenshots/chart-line.png)   | `chart-line.png`             | Line chart (green = income, red = expenses) for current month                                      |
| 7 | ![Expenses by Category](assets/screenshots/chart-pie.png)  | `chart-pie.png`              | Pie chart breakdown of expense categories                                                          |
| 8 | ![Subscriptions & Reminders](assets/screenshots/subscriptions.png) | `subscriptions.png`          | Lists recurring subscriptions and upcoming local reminders                                         |

---

### 3. Transactions (Expenses & Incomes)

| #  | Screenshot                                    | Filename                       | Description                                                                                 |
| -- | --------------------------------------------- | ------------------------------ | ------------------------------------------------------------------------------------------- |
| 9  | ![Expenses List Default](assets/screenshots/expenses-list-default.png) | `expenses-list-default.png`     | Expense entries with Description, Amount, Date, Category, Payment Method, edit/delete icons |
| 10 | ![Filtered Expenses](assets/screenshots/expenses-list-filter.png)     | `expenses-list-filter.png`      | Filter bar applied (e.g. “Taxi”)                                                             |
| 11 | ![Sorted Expenses](assets/screenshots/expenses-list-sort.png)         | `expenses-list-sort.png`        | Sorting by Amount                                                                            |
| 12 | ![Add Expense Modal](assets/screenshots/add-expense-modal.png)        | `add-expense-modal.png`         | Modal for creating a new expense (all fields)                                               |
| 13 | ![Incomes List](assets/screenshots/incomes-list.png)                  | `incomes-list.png`              | Income entries with Source, Amount, Date, Type                                               |
| 14 | ![Add Income Modal](assets/screenshots/add-income-modal.png)          | `add-income-modal.png`          | Modal for adding an income source                                                            |
| 15 | ![Edit Income Modal](assets/screenshots/edit-income-modal.png)        | `edit-income-modal.png`         | Modal for editing existing income entries                                                    |

---

### 4. Monthly Budgets

| #  | Screenshot                            | Filename                   | Description                                           |
| -- | ------------------------------------- | -------------------------- | ----------------------------------------------------- |
| 16 | ![Budgets List](assets/screenshots/budgets-list.png) | `budgets-list.png`         | List of per-category budgets (Total, Month, Category) |

---

### 5. Categories & Types

| #  | Screenshot                                | Filename               | Description                                                 |
| -- | ----------------------------------------- | ---------------------- | ----------------------------------------------------------- |
| 17 | ![Expense Categories](assets/screenshots/categories.png) | `categories.png`        | Manage expense categories (Name + Emoji icon)               |
| 18 | ![Income Types](assets/screenshots/income-types.png)    | `income-types.png`      | Define income types with descriptions                       |

---

### 6. Goals & Reminders

| #  | Screenshot                                | Filename                 | Description                                         |
| -- | ----------------------------------------- | ------------------------ | --------------------------------------------------- |
| 19 | ![Saving Goals](assets/screenshots/goals.png)     | `goals.png`              | List of savings targets with Title, Target, Deadline |
| 20 | ![Reminders List](assets/screenshots/reminders.png) | `reminders.png`          | Upcoming local notifications for user-defined tasks  |

---

### 7. Subscriptions & Payment Methods

| #  | Screenshot                                        | Filename                      | Description                                              |
| -- | ------------------------------------------------- | ----------------------------- | -------------------------------------------------------- |
| 21 | ![Subscriptions](assets/screenshots/subscriptions-list.png) | `subscriptions-list.png`      | Recurring subscriptions (Title, Amount, Frequency)       |
| 22 | ![Payment Methods](assets/screenshots/payment-methods.png)  | `payment-methods.png`         | Configurable payment methods (Name, Type, Provider)      |

---

### 8. Selectors & Navigation

| #  | Screenshot                                             | Filename                       | Description                                                    |
| -- | ------------------------------------------------------ | ------------------------------ | -------------------------------------------------------------- |
| 23 | ![Category Selector](assets/screenshots/select-category.png) | `select-category.png`          | Native modal to choose an expense category                     |
| 24 | ![Method Selector](assets/screenshots/select-method.png)     | `select-method.png`            | Native modal to choose a payment method                        |
| 25 | ![Main Navigation Drawer](assets/screenshots/main-drawer.png) | `main-drawer.png`              | Full app menu after login, highlighting **Expenses** section    |

---




