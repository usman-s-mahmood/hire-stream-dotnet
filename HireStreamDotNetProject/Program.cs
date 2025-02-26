using DevOne.Security.Cryptography.BCrypt;
using HireStreamDotNetProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// settings for BCrypt
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.Name = ".MyApp.Session";
    options.IdleTimeout = TimeSpan.FromDays(1);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// JWT Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-secret-key"))
        };
    });

// Add services to the container.
builder.Services.AddControllersWithViews();

// Context Process for frontend
builder.Services.AddHttpContextAccessor();

// Add services to the container.
// builder.Services.AddDbContext<ApplicationDbContext>(options =>
//     options.UseSqlite("Data Source=app.db"));

var secretFilePath = "db_secret.json";
var secrets = JsonSerializer.Deserialize<Dictionary<string, string>>(File.ReadAllText(secretFilePath));

string connectionString = $"Server={secrets["Server"]};Port={secrets["Port"]};Database={secrets["Database"]};User={secrets["User"]};Password={secrets["Password"]};SslMode={secrets["SslMode"]};";

// Adding SQL Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySQL(connectionString));



// Register the TokenService as a singleton
builder.Services.AddSingleton<HireStreamDotNetProject.Utils.TokenService>();

// Register EmailService as a singleton
builder.Services.AddSingleton<HireStreamDotNetProject.Utils.EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // app.UseExceptionHandler("/Home/Error");
    app.UseExceptionHandler("/Error/500"); // Custom 500 page
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
