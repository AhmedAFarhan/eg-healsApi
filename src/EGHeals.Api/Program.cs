using BuildingBlocks.Exceptions.Handler;
using EGHeals.Application;
using EGHeals.Infrastructure;
using EGHeals.Infrastructure.Helpers;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Allow any origin, header, and method
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "EG-Heals API",
        Version = "v1",
        Description = "This is my EG-Heals Web API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Ahmed Abdou",
            Email = "ahmedabduo625@gmail.com"
        }
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: 'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        },
        new string[] {}
    }
});
});

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

//Automatically apply migrations on startup
using (var scope = app.Services.CreateScope())
{
    var setup = scope.ServiceProvider.GetRequiredService<DataBaseSetup>();
    await setup.SetupAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); // You can customize it below
}

// Configure the HTTP request pipeline.

//Apply the policy globally
app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.UseExceptionHandler(options => { });

app.Run();
