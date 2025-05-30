using System.Text;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OfficeProject.Web;
using OfficeProject.Data;
using OfficeProject.Servicess;
using System.Text.Json.Serialization;
using System.Text.Json;
using OfficeProject.Models.Mapings;

var builder = WebApplication.CreateBuilder(args);

// ------------------ Services Configuration ------------------

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;

        // Add enum converter to accept string names case-insensitively
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
    });

// Razor Components
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddAutoMapper(typeof(MapingProfile));

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));


// Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.SignIn.RequireConfirmedEmail = false;
});

// JWT Authentication
var secret = builder.Configuration["Jwt:SecretKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})


.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true; // Optional: Save token in context
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret!)),
        ClockSkew = TimeSpan.Zero,

    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["accessToken"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/_blazor"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});

// Local Storage for Blazor
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.WriteIndented = true;
});

// Custom Services

builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IProjectsService, ProjectsService>();
builder.Services.AddScoped<IDesignPhaseServices, DesignPhaseServices>();
builder.Services.AddScoped<IDevelopmentPhaseServices, DevelopmentPhaseServices>();
builder.Services.AddScoped<IMaintenancePhaseServices, MaintenancePhaseServices>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IUserRolesService, UserRolesService>();
builder.Services.AddScoped<IAssignedUsersService, AssignedUsersService>();
builder.Services.AddScoped<IProductVsServices, ProductVsServices>();
builder.Services.AddScoped<IOthersServices, OthersServices>();
builder.Services.AddScoped<ISeoServicess, SeoServicess>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<IMarketingPhaseService, MarketingPhaseService>();
builder.Services.AddScoped<IClientSources, ClientSourcesService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
    provider.GetRequiredService<CustomAuthStateProvider>()); 
builder.Services.AddScoped<ApiService>();

// HttpClient
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7153/");
});


// ✅ Authorization By Role
builder.Services.AddAuthorizationBuilder()
.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"))
.AddPolicy("AdminOrManager", policy => policy.RequireRole("ADMIN", "MANAGER"))
.AddPolicy("User", policy => policy.RequireRole("USER"));

// Controllers
builder.Services.AddControllers();


// ✅ Add CORS BEFORE building the app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
// ------------------ Build App ------------------

var app = builder.Build();

// Apply EF Core migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// ------------------ Middleware Pipeline ------------------

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication(); // 🛡️ Must come before UseAuthorization
app.UseAuthorization();

app.MapControllers();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();
