using GallerySystem.Core.Config;
using GallerySystem.Core.Entities;
using GallerySystem.DataAccess.Contexts;
using GallerySystem.DataAccess.Repositories.Abstractions;
using GallerySystem.DataAccess.Repositories.Abstractions.Base;
using GallerySystem.DataAccess.Repositories.Implementations;
using GallerySystem.DataAccess.Repositories.Implementations.Base;
using GallerySystem.DataAccess.UnitOfWork.Abstractions;
using GallerySystem.DataAccess.UnitOfWork.Implementations;
using GallerySystem.Service.Business.Data.Abstractions;
using GallerySystem.Service.Business.Data.Implementations;
using GallerySystem.Service.Business.Utility.Abstractions;
using GallerySystem.Service.Business.Utility.Implementations;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conString = builder.Configuration.GetConnectionString("ConString");

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<GalleryContext>(options => options.UseSqlServer(conString));
builder.Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireLowercase = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireNonAlphanumeric = false;

        options.SignIn.RequireConfirmedEmail = false;
    }).AddEntityFrameworkStores<GalleryContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.LoginPath = new PathString($"/account/login");
    options.LogoutPath = new PathString($"/account/logout");
    options.AccessDeniedPath = new PathString($"/");
    options.ExpireTimeSpan = TimeSpan.FromHours(72);
    options.Cookie.Name = "IdentityCookie";
});

// DI
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAlbumRepository, AlbumRepository>();
builder.Services.AddScoped<IPhotoRepository, PhotoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAlbumService, AlbumService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IFileService, FileService>();


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));
builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseStatusCodePagesWithRedirects("/Home/Error/{0}");
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Album}/{action=Index}/{id?}");

app.Run();