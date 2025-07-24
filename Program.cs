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


// Controllers with JSON options
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, allowIntegerValues: true));
        // options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve; // if circular references needed
    });

// Razor Components

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// AutoMapper

builder.Services.AddAutoMapper(typeof(MapingProfile));

// Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("DefaultConnection is not set in configuration.");
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
var jwtConfig = builder.Configuration.GetSection("JwtSettings");
var secret = jwtConfig["Secret"];
if (string.IsNullOrEmpty(secret))
{
    throw new InvalidOperationException("JWT:Secret is not set in configuration.");
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
        ValidIssuer = jwtConfig["Issuer"],
        ValidAudience = jwtConfig["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret)),
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

// Blazored LocalStorage
builder.Services.AddBlazoredLocalStorage(config =>
{
    config.JsonSerializerOptions.WriteIndented = true;
});

// Services registration
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddHttpClient<IUserService, UserService>(client =>
//{
//    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7153/");
//});

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
builder.Services.AddScoped<DateService>();
builder.Services.AddScoped<IUserTaskMasterService, UserTaskMasterService>();


// HttpClient registration using IHttpClientFactory (recommended)
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? "https://localhost:7153/");
});

// Authorization policies
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => policy.RequireRole("ADMIN"))
    .AddPolicy("AdminOrManager", policy => policy.RequireRole("ADMIN", "MANAGER"))
    .AddPolicy("AdminOrManagerOrUser", policy => policy.RequireRole("ADMIN", "MANAGER", "USER"))
    .AddPolicy("User", policy => policy.RequireRole("USER"));

// Controllers
builder.Services.AddControllers();

// CORS
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

// Apply EF migrations
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
app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery(); // ✅ Must be placed here (AFTER Authentication & Authorization, BETWEEN UseRouting and MapEndpoints)

// Map endpoints
app.MapControllers();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

app.Run();

