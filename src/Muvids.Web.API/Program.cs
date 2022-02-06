using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Muvids.Application;
using Muvids.Application.Contracts;
 
using Muvids.Identity.Models;
using Muvids.Infrastructure;
using Muvids.Persistence;
using Muvids.Web.API.Helpers;
using Muvids.Web.API.Middleware;
using Muvids.Web.API.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilog();

IConfiguration configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(configuration);
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddIdentityServices(configuration);

builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
 

// START Swagger
// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
// https://dotnetthoughts.net/openapi-support-for-aspnetcore-minimal-webapi/
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                          },
                          Scheme = "oauth2",
                          Name = "Bearer",
                          In = ParameterLocation.Header,

                        },
                        new List<string>()
                      }
                    });

    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Muvids Management API",
        Description = "An ASP.NET Core Web API for managin Movies",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Heliberto Arias",
            Url = new Uri("https://localhost/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://localhost/license")
        }
    });

    // c.OperationFilter<FileResultContentTypeOperationFilter>();
});
// END Swagger

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Muvids API");
    });

    app.UseDeveloperExceptionPage();
}
 
    // https://andrewlock.net/creating-a-custom-error-handler-middleware-function/
    app.UseCustomExceptionHandler();
 

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

try
{
    var userManager = app.Services.GetRequiredService<UserManager<ApplicationUser>>();

    await Muvids.Identity.Seed.CreateFirstUser.SeedAsync(userManager);
    Log.Information("Application Starting");
}
catch (Exception ex)
{
    Log.Warning(ex, "An error occured while starting the application");
}

app.Run();


// https://stackoverflow.com/questions/69058176/how-to-use-webapplicationfactory-in-net6-without-speakable-entry-point
//<ItemGroup> < InternalsVisibleTo Include = "Muvids.Web.API.IntegrationTest" /> </ ItemGroup >
public partial class Program { } // so you can reference it from tests