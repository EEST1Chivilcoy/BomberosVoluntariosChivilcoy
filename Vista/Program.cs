using Vista.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Vista.Services;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using AntDesign;
using Microsoft.Extensions.Options;

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

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAntDesign();
builder.Services.AddHostedService<InitData>();
builder.Services.AddScoped<IBomberoService, BomberoService>();
builder.Services.AddScoped<IBrigadaService, BrigadaService>();
builder.Services.AddScoped<IDepositoService, DepositoService>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<ISalidaService, SalidaService>();
builder.Services.AddScoped<IParteService, ParteService>();
builder.Services.AddHttpClient<GeorefService>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// A�adir el middleware de localizaci�n
var localizationOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>()?.Value;

if (localizationOptions != null)
{
    app.UseRequestLocalization(localizationOptions);
}
else
{
    // Manejar el caso en que localizationOptions sea null
    Console.WriteLine("Error: RequestLocalizationOptions no est� configurado correctamente.");
}

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();
