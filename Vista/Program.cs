using Vista.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Vista.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using AntDesign;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Identity.Web;
using Microsoft.Identity.Web.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Components.Server.Circuits;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["ConnectionString"];
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContextFactory<BomberosDbContext>(
    dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors()
);

var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ')
    ?? builder.Configuration["MicrosoftGraph:Scopes"]?.Split(' ');

// Agregar cache distribuido ANTES de la autenticación
builder.Services.AddDistributedMemoryCache();

// Configuración de autenticación CON configuraciones para Azure
builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(options =>
    {
        builder.Configuration.Bind("AzureAd", options);

        // Esto es obligatorio para que funcionen los Roles de Aplicación.
        // Le dice a .NET que busque el array "roles" en el token JSON.
        options.TokenValidationParameters.RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";

        // Configuración de eventos
        options.Events = new OpenIdConnectEvents
        {
            OnRemoteFailure = context =>
            {
                context.HandleResponse();
                context.Response.Redirect("/Error?message=" + context.Failure?.Message);
                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine($"Authentication failed: {context.Exception?.Message}");
                return Task.CompletedTask;
            },
            OnTokenValidated = context =>
            {
                Console.WriteLine("=== TOKEN VALIDADO ===");
                foreach (var claim in context.Principal.Claims)
                {
                    Console.WriteLine($"{claim.Type}: {claim.Value}");
                }
                Console.WriteLine("======================");
                return Task.CompletedTask;
            }
        };
    })
    .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
    .AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
    .AddInMemoryTokenCaches();


// Configuración de cookies para Azure Web Apps
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => false; // Deshabilitar en Azure
    options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
    options.HandleSameSiteCookieCompatibility();
});

// Configuración adicional de cookies de autenticación
builder.Services.ConfigureApplicationCookie(options =>
{
    options.Cookie.SameSite = SameSiteMode.Lax;
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews()
    .AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

// Add services to the container.
builder.Services.AddRazorPages();

// CRÍTICO: Agregar HttpContextAccessor ANTES de Blazor
builder.Services.AddHttpContextAccessor();

builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler()
    .AddCircuitOptions(options =>
    {
        options.DetailedErrors = true;
        // Aumentar timeout para Azure
        options.DisconnectedCircuitMaxRetained = 100;
        options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
    });

// Habilitar HttpContext en circuitos de Blazor
builder.Services.AddScoped<CircuitHandler, CircuitHandlerService>();
builder.Services.AddAntDesign();
builder.Services.AddHostedService<InitData>();

// Servicios Scoped
builder.Services.AddScoped<IBomberoService, BomberoService>();
builder.Services.AddScoped<IComisionDirectivaService, ComisionDirectivaService>();
builder.Services.AddScoped<ICobradorService, CobradorService>();
builder.Services.AddScoped<IBrigadaService, BrigadaService>();
builder.Services.AddScoped<IDepositoService, DepositoService>();
builder.Services.AddScoped<IEquipoAutonomoService, EquipoAutonomoService>();
builder.Services.AddScoped<IMovimientoEquipoAutonomoService, MovimientoEquipoAutonomoService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<ISalidaService, SalidaService>();
builder.Services.AddScoped<IDependenciaService, DependenciaService>();
builder.Services.AddScoped<ILicenciaService, LicenciaService>();
builder.Services.AddScoped<IVehiculoSalidaService, VehiculoSalidaService>();
builder.Services.AddScoped<IFuerzaIntervinienteService, FuerzaIntervinienteService>();
builder.Services.AddScoped<IHistorialSocioService, HistorialSocioService>();
builder.Services.AddScoped<ISocioService, SocioService>();
builder.Services.AddScoped<IEntraIDService, EntraIDService>();
builder.Services.AddScoped<IParteVehiculoService, ParteVehiculoService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IImagenService, ImagenService>();

// Servicios HttpClient
builder.Services.AddHttpClient<INominatimService, NominatimService>();

// Configurar la localización
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new List<CultureInfo>
    {
        new CultureInfo("es-AR")
    };

    options.DefaultRequestCulture = new RequestCulture("es-AR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

// Localizacion AntDesign
LocaleProvider.SetLocale("es-Es");

app.UseHttpsRedirection();
app.UseStaticFiles();

// IMPORTANTE: El orden de estos middleware es crítico
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// Añadir el middleware de localización
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value;

if (localizationOptions != null)
{
    app.UseRequestLocalization(localizationOptions);
}
else
{
    Console.WriteLine("Error: RequestLocalizationOptions no está configurado correctamente.");
}

app.MapControllers();
app.MapBlazorHub();
app.MapRazorPages();
app.MapFallbackToPage("/_Host");

app.Run();