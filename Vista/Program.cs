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

var initialScopes = builder.Configuration["DownstreamApi:Scopes"]?.Split(' ') ?? builder.Configuration["MicrosoftGraph:Scopes"]?.Split(' ');

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
.AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"))
        .EnableTokenAcquisitionToCallDownstreamApi(initialScopes)
.AddMicrosoftGraph(builder.Configuration.GetSection("MicrosoftGraph"))
            .AddInMemoryTokenCaches();

builder.Services.AddControllersWithViews()
.AddMicrosoftIdentityUI();

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = options.DefaultPolicy;
});

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntDesign();
builder.Services.AddHostedService<InitData>();

// Servicios Scoped
builder.Services.AddScoped<IBomberoService, BomberoService>();
builder.Services.AddScoped<IComisionDirectivaService, ComisionDirectivaService>();
builder.Services.AddScoped<ICobradorService, CobradorService>();
builder.Services.AddScoped<IBrigadaService, BrigadaService>();
builder.Services.AddScoped<IDepositoService, DepositoService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<ISalidaService, SalidaService>();
builder.Services.AddScoped<IDependenciaService, DependenciaService>();
builder.Services.AddScoped<ILicenciaService, LicenciaService>();
builder.Services.AddScoped<IVehiculoSalidaService, VehiculoSalidaService>();
builder.Services.AddScoped<IFuerzaIntervinienteService, FuerzaIntervinienteService>();
builder.Services.AddScoped<IImagenService, ImagenService>();

// Servicios HttpClient
builder.Services.AddHttpClient<INominatimService, NominatimService>();

// Configurar la localizaci�n
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

//Errores detallados
builder.Services.AddServerSideBlazor().AddCircuitOptions(options => { options.DetailedErrors = true; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Localizacion AntDesign
LocaleProvider.SetLocale("es-Es");

// Habilitar controllers (Es necesario para que el controlador de imágenes funcione)
app.MapControllers();

app.UseHttpsRedirection();
app.UseStaticFiles();
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
    // Manejar el caso en que localizationOptions sea null
    Console.WriteLine("Error: RequestLocalizationOptions no está configurado correctamente.");
}

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
