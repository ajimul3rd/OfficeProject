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
DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

// ------------------ Services Configuration ------------------

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        // Add enum converter to accept string names case-insensitively
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        //options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

// Razor Components
builder.Services.AddRazorComponents().AddInteractiveServerComponents();

builder.Services.AddAutoMapper(typeof(MapingProfile));

// Database
//var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("DB_CONNECTION is not set.");
}


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
//var secret = builder.Configuration["Jwt:SecretKey"];
var secret = Environment.GetEnvironmentVariable("JWT_SECRET");

if (string.IsNullOrEmpty(secret))
{
    throw new InvalidOperationException("JWT_SECRET is not set in .env or environment variables.");
}

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})

    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = true;
        options.SaveToken = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER"),
            ValidAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")!)
            ),
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
builder.Services.AddScoped<IWorkTaskDetailsService, WorkTaskDetailsService>();
builder.Services.AddScoped<IBillingCycleHelper, BillingCycleHelper>();
builder.Services.AddScoped<IUserWorkingActivityServices, UserWorkingActivityServices>();
builder.Services.AddScoped<IUserDesignationService, UserDesignationService>();
builder.Services.AddScoped<IProductsService, ProductsService>();
builder.Services.AddScoped<IUserActivityMasterService, UserActivityMasterService>();
builder.Services.AddScoped<IUserRolesService, UserRolesService>();
builder.Services.AddScoped<IAssignedUsersService, AssignedUsersService>();
builder.Services.AddScoped<IProductVsServices, ProductVsServices>();
builder.Services.AddScoped<IOthersServices, OthersServices>();
builder.Services.AddScoped<IWebDevelopmentService, WebDevelopmentService>();
builder.Services.AddScoped<ISeoServicess, SeoServicess>();
builder.Services.AddScoped<IOthersTaskservices, OthersTaskservices>();
builder.Services.AddScoped<IWebDeveTaskService, WebDeveTaskService>();
builder.Services.AddScoped<ISeoTaskServicess, SeoTaskServicess>();
builder.Services.AddScoped<CustomAuthStateProvider>();
builder.Services.AddScoped<IClientSources, ClientSourcesService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
provider.GetRequiredService<CustomAuthStateProvider>()); 
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<IDataSerializer, DataSerializer>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddSingleton<AppState>();


// HttpClient
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7153/");
});


// ✅ Authorization By Role
builder.Services.AddAuthorizationBuilder()
.AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"))
.AddPolicy("AdminOrManager", policy => policy.RequireRole("ADMIN", "MANAGER"))
.AddPolicy("AdminOrManagerOrUser", policy =>policy.RequireRole("ADMIN", "MANAGER", "USER"))
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
