
# Gallery System



## Tech stack

- ASP.NET Core MVC (.NET 6)
- MS SQL
- Entity Framework Core
- Onion architecture


## Steps to run project

To make migrations go in solution folder and run:

```bash
  dotnet ef migrations add "init_db"  --project GallerySystem.DataAccess --startup-project GallerySystem.Web
```

To create database run:
```bash
  dotnet ef database update  --project GallerySystem.DataAccess --startup-project GallerySystem.Web
```

To create database run:
```bash
  dotnet watch run --project GallerySystem.Web
```


