[![.NET](https://img.shields.io/badge/.NET-8.0-purple)](https://dotnet.microsoft.com/) [![MAUI](https://img.shields.io/badge/.NET_MAUI-Cross--Platform-brightgreen)](https://learn.microsoft.com/dotnet/maui) [![Blazor](https://img.shields.io/badge/Blazor-Hybrid-9cf)](https://learn.microsoft.com/aspnet/core/blazor/hybrid) [![Build](https://github.com/Adrianowsski/BudgetWise/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Adrianowsski/BudgetWise/actions) [![License: MIT](https://img.shields.io/badge/License-MIT-green)](LICENSE)

# 💰 BudgetWise

A **cross-platform** personal finance manager built with **.NET MAUI + Blazor Hybrid**. Track expenses & income, set budgets, manage subscriptions, and hit your savings goals — online *or* completely offline with seamless API sync.

---

## 📌 Table of Contents

* [🚀 Key Features](#key-features)
* [🛠 Tech Stack](#tech-stack)
* [🏗 Project Structure](#project-structure)
* [⚙️ Installation & Setup](#installation--setup)
  * [Backend (Web API)](#backend-web-api)
  * [Mobile App](#mobile-app)
* [▶️ Running the App](#running-the-app)
* [📸 Screenshots](#screenshots)
* [📄 License](#license)

---

## 🚀 Key Features

* **Authentication**
  ASP.NET Core Identity + IdentityServer4 (JWT) for secure sign-up / login.
* **Dashboard**
  Summary KPI cards, *Income vs Expenses* line chart, category pie chart, active subscriptions.
* **Transactions CRUD**
  Filterable, sortable *Expenses* & *Incomes* with rich add/edit modals.
* **Monthly Budgets**
  Per-category limits with live usage bars.
* **Custom Categories & Types**
  User-defined expense categories (with emoji) & income types.
* **Savings Goals**
  Target amounts + deadlines, progress visualisation.
* **Subscriptions & Reminders**
  Manage recurring charges and receive local notifications.
* **Offline-First & Sync**
  SecureStorage caching, Polly retry/circuit-breaker, RESTful sync to SQL-backed API.
* **Clean Architecture**
  MVVM, DI, Blazor components, Behaviours, MessagingCenter.

---

## 🛠 Tech Stack

| Layer              | Technology / Library                              |
| ------------------ | ------------------------------------------------- |
| **Mobile UI**      | .NET MAUI + Blazor Hybrid, C#                     |
| **State & DI**     | CommunityToolkit.Mvvm, Microsoft Extensions DI    |
| **Charts & Icons** | Microcharts, FontAwesome / Material Icons         |
| **Storage**        | SecureStorage, Preferences                        |
| **Backend API**    | ASP.NET Core 9 + EF Core 9, SQL Server            |
| **Auth**           | IdentityServer4, ASP.NET Core Identity, JWT       |
| **Networking**     | HttpClientFactory + Polly                         |
| **Testing**        | xUnit, Moq, FluentAssertions                      |
| **CI/CD**          | GitHub Actions (Android & iOS build, tests, lint) |

---

## 🏗 Project Structure

```text
BudgetWise.sln
│
├─ src/
│  ├─ Mobile/                       # .NET MAUI Blazor app
│  │   ├─ BudgetWise.Mobile/        # UI, ViewModels, services
│  │   └─ BudgetWise.Mobile.Tests/  # UI tests
│  └─ Backend/                      # ASP.NET Core 9 Web API
│      ├─ BudgetWise.Api/
│      └─ BudgetWise.Infrastructure/ # EF Core migrations & seeders
└─ build/                           # GitHub Actions & Docker files

```

---

## ⚙️ Installation & Setup

> **Tip:** The solution runs fully offline. Start the API first, then the mobile app.

### Backend (Web API)

```bash
# Clone repository
git clone https://github.com/Adrianowsski/BudgetWise.git
cd BudgetWise/src/Backend

# Restore & build
dotnet restore
dotnet build -c Release

# Apply migrations & seed demo data
dotnet ef database update

# Run API (https://localhost:5001)
dotnet run -c Release

```

Connection string sits in **appsettings.json**; default is `(localdb)\\MSSQLLocalDB`.

### Mobile App

```bash
# From repo root
cd src/Mobile/BudgetWise.Mobile

# Restore & build
dotnet restore
dotnet build -f net8.0-android    # or net8.0-ios

# Update API base URL
#  📄 appsettings.Development.json → "ApiBaseUrl": "https://10.0.2.2:5001"

# Deploy
#  Android Emulator:      dotnet maui deploy -f net8.0-android
#  iOS Simulator (macOS): dotnet maui deploy -f net8.0-ios

```

> **Visual Studio 2022** automatically handles device selection & hot reload.

---

## ▶️ Running the App

1. **Start API** (`https://localhost:5001`).
2. **Launch Mobile** on Android/iOS/Windows.
3. **Register** a new account → log in.
4. Add transactions, budgets & goals — all data syncs once the device is online again.

---

## 📸 Screenshots

> Images live in `assets/screenshots/`. Ensure filenames match.

### 1. Pre‑Authentication

| # | Screenshot                                  | Description                |
| - | ------------------------------------------- | -------------------------- |
| 1 | ![](assets/screenshots/prelogin-drawer.png) | Drawer (logged out)        |
| 2 | ![](assets/screenshots/login.png)           | Login form                 |
| 3 | ![](assets/screenshots/register.png)        | Register form              |
| 4 | ![](assets/screenshots/login-error.png)     | Invalid credentials banner |

### 2. Dashboard

| # | Screenshot                                    | Description               |
| - | --------------------------------------------- | ------------------------- |
| 5 | ![](assets/screenshots/dashboard-summary.png) | KPI summary cards         |
| 6 | ![](assets/screenshots/chart-line.png)        | Income vs Expenses chart  |
| 7 | ![](assets/screenshots/chart-pie.png)         | Expenses by category pie  |
| 8 | ![](assets/screenshots/subscriptions.png)     | Subscriptions & reminders |

### 3. Transactions

| #  | Screenshot                                        | Description       |
| -- | ------------------------------------------------- | ----------------- |
| 9  | ![](assets/screenshots/expenses-list-default.png) | Expenses list     |
| 10 | ![](assets/screenshots/expenses-list-filter.png)  | Filtered expenses |
| 11 | ![](assets/screenshots/expenses-list-sort.png)    | Sorted expenses   |
| 12 | ![](assets/screenshots/add-expense-modal.png)     | Add expense modal |
| 13 | ![](assets/screenshots/incomes-list.png)          | Incomes list      |
| 14 | ![](assets/screenshots/add-income-modal.png)      | Add income modal  |
| 15 | ![](assets/screenshots/edit-income-modal.png)     | Edit income modal |

### 4. Monthly Budgets

| #  | Screenshot                               | Description          |
| -- | ---------------------------------------- | -------------------- |
| 16 | ![](assets/screenshots/budgets-list.png) | Budgets per category |

### 5. Categories & Types

| #  | Screenshot                               | Description                |
| -- | ---------------------------------------- | -------------------------- |
| 17 | ![](assets/screenshots/categories.png)   | Expense categories manager |
| 18 | ![](assets/screenshots/income-types.png) | Income types manager       |

### 6. Goals & Reminders

| #  | Screenshot                            | Description        |
| -- | ------------------------------------- | ------------------ |
| 19 | ![](assets/screenshots/goals.png)     | Saving goals list  |
| 20 | ![](assets/screenshots/reminders.png) | Upcoming reminders |

### 7. Subscriptions & Payment Methods

| #  | Screenshot                                     | Description             |
| -- | ---------------------------------------------- | ----------------------- |
| 21 | ![](assets/screenshots/subscriptions-list.png) | Subscriptions list      |
| 22 | ![](assets/screenshots/payment-methods.png)    | Payment methods curator |

### 8. Selectors & Navigation

| #  | Screenshot                                  | Description             |
| -- | ------------------------------------------- | ----------------------- |
| 23 | ![](assets/screenshots/select-category.png) | Category selector       |
| 24 | ![](assets/screenshots/select-method.png)   | Payment method selector |
| 25 | ![](assets/screenshots/main-drawer.png)     | Main navigation drawer  |

---

## 📄 License

© 2025 – Released under the [MIT License](LICENSE).

---

*Update badges, URLs & connection strings to your environment before pushing to GitHub.*
::contentReference[oaicite:0]{index=0}

