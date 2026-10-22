<div align="center">

# ✈️ Booking System API

**A RESTful booking & ticket management API with JWT authentication and role-based access control.**

![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-239120?logo=csharp&logoColor=white)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-4169E1?logo=postgresql&logoColor=white)
![EF Core](https://img.shields.io/badge/EF%20Core-Entity%20Framework-5C2D91)
![JWT](https://img.shields.io/badge/Auth-JWT-000000?logo=jsonwebtokens&logoColor=white)
![Swagger](https://img.shields.io/badge/Docs-Swagger-85EA2D?logo=swagger&logoColor=black)

</div>

---

## 📖 About

Booking System API lets authenticated users book trips and view their tickets, while an **Admin** manages the tickets. Every user can only see and change their own data, and admin-only actions are protected with role-based authorization.

## ✨ Features

- 🔐 **Authentication**: registration and login with ASP.NET Core Identity, secured with JWT
- 👥 **Role-based authorization**: `Admin` and `User` roles
- 🧳 **Bookings**: create, list (with destination filter), reschedule and cancel your own bookings
- 🎫 **Tickets**: created and managed by the Admin, viewable only by the ticket's owner
- 🛡️ **Ownership checks**: users can never access another user's bookings or tickets
- 📚 **Swagger UI**: interactive API documentation with JWT support
- 🌱 **Admin seeding**: the `Admin` role is created and assigned automatically on startup

## 🧱 Tech Stack

| Layer          | Technology                          |
| -------------- | ----------------------------------- |
| Framework      | ASP.NET Core Web API (C#)           |
| ORM            | Entity Framework Core               |
| Database       | PostgreSQL (Npgsql)                 |
| Authentication | ASP.NET Core Identity + JWT Bearer  |
| Documentation  | Swagger / Swashbuckle               |

## 🗂️ Data Model

```
ApplicationUser ──< Booking ──< Ticket
```

- A **user** can have many **bookings**.
- A **booking** can have many **tickets** (one ticket per passenger).
- Each **ticket** belongs to exactly one booking.

## 🔌 API Endpoints

### Auth

| Method | Endpoint             | Access | Description         |
| ------ | -------------------- | ------ | ------------------- |
| POST   | `/api/Auth/register` | Public | Register a new user |
| POST   | `/api/Auth/Login`    | Public | Log in and get a JWT |

### Booking

| Method | Endpoint            | Access | Description                                        |
| ------ | ------------------- | ------ | -------------------------------------------------- |
| POST   | `/api/Booking`      | User   | Create a booking                                   |
| GET    | `/api/Booking`      |  Admin | List my bookings (optional `?destination=` filter) |
| PUT    | `/api/Booking/{id}` |  Admin | Change the travel date of my booking               |
| DELETE | `/api/Booking/{id}` |  Admin | Cancel my booking                                  |

### Tickets

| Method | Endpoint            | Access | Description                     |
| ------ | ------------------- | ------ | ------------------------------- |
| GET    | `/api/Tickets`      | Admin  | List all tickets                |
| GET    | `/api/Tickets/{id}` | User   | Get one of my tickets           |
| POST   | `/api/Tickets`      | Admin  | Create a ticket for a booking   |
| PUT    | `/api/Tickets/{id}` | Admin  | Update a ticket's status        |
| DELETE | `/api/Tickets/{id}` | Admin  | Delete a ticket                 |

### Flights

| Method | Endpoint            | Access | Description                                      |
| ------ | ------------------- | ------ | ------------------------------------------------ |
| GET    | `/api/Flight`       | Admin  | List all flights                                 |
| POST   | `/api/Flight`       | Admin  | Create a flight for a user's booking             |
| PUT    | `/api/Flight/{id}`  | Admin  | Update a flight                                  |
| DELETE | `/api/Flight/{id}`  | Admin  | Delete a flight (must have at least one booking) |
## 🚀 Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/download/)
- EF Core CLI tools: `dotnet tool install --global dotnet-ef`

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/aqdarahmad/booking-system-api.git
cd booking-system-api
```

**2. Configure the app**

Copy `appsettings.example.json` to `appsettings.json` and fill in your own values:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=BookingDb;Username=postgres;Password=YOUR_PASSWORD"
  },
  "Jwt": {
    "Key": "YOUR_SECRET_KEY_AT_LEAST_32_CHARACTERS",
    "Issuer": "BookingSystem",
    "Audience": "BookingSystemUsers"
  }
}
```

> ⚠️ Never commit real passwords or JWT keys.

**3. Create the database**

```bash
dotnet ef database update
```

**4. Run the project**

```bash
dotnet run
```

**5. Open Swagger**

Go to `https://localhost:<port>/swagger` (Swagger is enabled in the Development environment).

## 🔑 Authentication & Roles

### Using the API in Swagger

1. Register with `POST /api/Auth/register`
2. Log in with `POST /api/Auth/Login` and copy the `token`
3. Click **Authorize** and paste the token

### Creating the Admin

The app assigns the `Admin` role to the user named **`admin`** on startup.

1. Register a user with the username `admin`
2. **Restart the application**
3. Log in as `admin` to get a token that includes the Admin role

> Tokens are issued at login, so log in again whenever roles change.

## 🧪 Example Flow

1. A **user** logs in and creates a booking:

   ```json
   {
     "numberofTickets": 2,
     "travelDate": "2027-01-20T08:30:00Z",
     "destination": "Dubai"
   }
   ```

2. The **admin** creates a ticket for each passenger:

   ```json
   {
     "passengerName": "JohnDoe",
     "ticketPrice": 200,
     "seatNumber": 3,
     "bookingid": "<booking-id>"
   }
   ```

3. The **user** views their ticket with `GET /api/Tickets/{id}`.

## 🗺️ Roadmap

- [ ] Payments
- [ ] Seat availability checks
- [ ] Ticket status as an enum
- [ ] Unit and integration tests
- [ ] React frontend

## 📸 Screenshots

_Add a Swagger screenshot here:_

```md
![Swagger UI](docs/swagger.png)
```

## 👩‍💻 Author

**Aqdar Ahmad**
GitHub: [@aqdarahmad](https://github.com/aqdarahmad)
