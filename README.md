# SmallPostAPI

A project built with ASP.NET Core Web API and Entity Framework Core, modeling a simple social-style domain: **Users** have **Posts** (1 → many).  
The goal is to incrementally evolve this into a richer domain (friendships, feeds, GraphQL, etc.) while keeping clean architecture & best practices.

---

## ✅ Current Features

- ASP.NET Core Web API
- Entity Framework Core with SQL Server Express
- `User` → `Post` one-to-many relationship
- Cascade delete (deleting a User deletes all their Posts)
- DTO-based API (no EF entities returned)
- Service layer abstraction (controllers are thin)
- Basic CRUD endpoints:
  - `Users`: Create / Get / Update / Delete
  - `Posts`: Create / Get / GetByUser / Update / Delete
- DTO projection instead of navs

---

## 🏗 Technologies Used

| Area | Technology |
|------|------------|
| API Framework | ASP.NET Core Web API |
| ORM & Data Access | Entity Framework Core |
| Database | SQL Server Express |
| Object Mapping | Manual DTO mapping |
| API Documentation & Exploration | Scalar (OpenAPI-based API exploration) |
| Dependency Injection | Built-in ASP.NET Core DI |
| Migrations | EF Core CLI (`dotnet ef`) |
| Version Control | Git + GitHub |


---

## 🧭 Planned Next Steps (Roadmap)

### Short-term
- Add **Friendship** domain (User ↔ User with statuses)
- Add **validation** (FluentValidation or similar)
- Add **paging & sorting** for lists
- Enforce **unique constraints** & indices (e.g. email)

### Mid-term
- **Privacy rules** (friends-only vs public posts)
- **Blocks / safety layer**
- **Reactions / comments**
- **News feed** (friends’ posts)

### GraphQL (future phase)
- Add GraphQL endpoint (Hot Chocolate)
- Types for Users / Posts / Friends
- Relay-style connections with pagination
- Mutations for friend requests, accepting, posting
- Subscriptions for realtime updates (post created / friend request)

---

## 📝 Running locally

1. Ensure SQL Server Express is running
2. Update `appsettings.json` connection string if needed
3. Apply migrations:

```bash
dotnet ef database update
