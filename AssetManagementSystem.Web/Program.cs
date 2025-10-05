using AssetManagementSystem.Db.Data;
using AssetManagementSystem.Db.Entities;
using AssetManagementSystem.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AssetManagementSystem.Web.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString,
        b => b.MigrationsAssembly("AssetManagementSystem.Db")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// เปลี่ยนเป็น AddIdentity เพื่อใช้ ApplicationUser และ ApplicationRole
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;               // Must include digits
    options.Password.RequiredLength = 8;                // Minimum length 8
    options.Password.RequireNonAlphanumeric = true;     // Must include special characters
    options.Password.RequireUppercase = true;           // Must include uppercase letters
    options.Password.RequireLowercase = true;           // Must include lowercase letters
    options.Password.RequiredUniqueChars = 4;           // At least 4 unique characters
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// ปรับแต่งพฤติกรรมของคุกกี้ (วางโค้ดของคุณตรงนี้)
builder.Services.ConfigureApplicationCookie(options =>
{
    // กำหนดให้หน้า Login เริ่มต้นคือ Controller 'Account' และ Action 'Login'
    options.LoginPath = "/Account/Login";
});

// Set token valid for 30 minutes
builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(30);
});

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, MyUserClaimsPrincipalFactory>();

// External Authentication - Google
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        // Load credentials from appsettings.json
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    })
    .AddFacebook(facebookOptions =>
    {
        facebookOptions.ClientId = builder.Configuration["Authentication:Facebook:ClientId"]!;
        facebookOptions.ClientSecret = builder.Configuration["Authentication:Facebook:ClientSecret"]!;
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=account}/{action=login}/{id?}");

app.Run();