# TodoList MVC

Professional ASP.NET Core MVC Todo List application using a clean 3-tier architecture.

## Projects

- `TodoList.MVC` - Presentation layer with controllers, Razor views, view models, filters, middleware, and Bootstrap UI.
- `TodoList.BLL` - Business layer with services, DTOs, validation rules, and AutoMapper profiles.
- `TodoList.DAL` - Data access layer with EF Core DbContext, entities, configurations, repositories, Unit of Work, migrations, and seed data.
- `TodoList.Shared` - Shared constants, helpers, exceptions, and enums.

## Run

Update `DefaultConnection` in `TodoList.MVC/appsettings.json` for your SQL Server instance, then run:

```bash
dotnet restore TodoList.slnx
dotnet ef database update --project TodoList.DAL --startup-project TodoList.MVC
dotnet run --project TodoList.MVC
```

Authentication is implemented with ASP.NET Core Identity. After applying migrations, you can register a new account from the UI. If the database is reachable, the app also seeds a demo account:

- Email: `demo@todolist.local`
- Password: `Demo123!`

If the local compiler server hangs, build with:

```bash
dotnet build TodoList.slnx --no-restore -m:1 -nr:false -p:UseSharedCompilation=false
```
