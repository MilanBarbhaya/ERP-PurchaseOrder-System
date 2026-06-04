using Asp.Versioning;
using ERP.API.Middleware;
using ERP.API.Validations;
using ERP.Application.Interfaces;
using ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.UpdatePurchaseOrder;
using ERP.Infrastructure;
using ERP.Infrastructure.Persistence;
using ERP.Infrastructure.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

#region MongoDB

var mongoSettings = builder.Configuration
    .GetSection("MongoDb")
    .Get<MongoDbSettings>();

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoSettings!.ConnectionString));

builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();

    return client.GetDatabase(
        mongoSettings.DatabaseName);
});

#endregion

#region Infrastructure

builder.Services.AddInfrastructure(
    builder.Configuration);

#endregion

#region MediatR

builder.Services.AddMediatR(
    typeof(UpdatePurchaseOrderCommand).Assembly);

#endregion

#region FluentValidation

builder.Services.AddValidatorsFromAssemblyContaining<
    CreatePurchaseOrderValidator>();

builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));

#endregion

#region JWT Authentication

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["Jwt:Issuer"],

                ValidAudience =
                    builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:Key"]!)),

                RoleClaimType = ClaimTypes.Role
            };
    });

builder.Services.AddAuthorization();

#endregion

#region API Versioning

builder.Services
    .AddApiVersioning(options =>
    {
        options.DefaultApiVersion =
            new ApiVersion(1, 0);

        options.AssumeDefaultVersionWhenUnspecified = true;

        options.ReportApiVersions = true;

        options.ApiVersionReader =
            new UrlSegmentApiVersionReader();
    })
    .AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";

        options.SubstituteApiVersionInUrl = true;
    });

#endregion

#region Swagger

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "ERP API",
            Version = "v1"
        });

    options.SwaggerDoc(
        "v2",
        new OpenApiInfo
        {
            Title = "ERP API",
            Version = "v2"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter: Bearer {your JWT token}"
        });

    options.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference =
                        new OpenApiReference
                        {
                            Type =
                                ReferenceType.SecurityScheme,

                            Id = "Bearer"
                        }
                },
                Array.Empty<string>()
            }
        });
});

#endregion

#region CORS

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "AngularPolicy",
        policy =>
        {
            policy
                .WithOrigins(
                    "http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

#endregion

var app = builder.Build();

#region Seed Database

using (var scope = app.Services.CreateScope())
{
    var userRepository =
        scope.ServiceProvider
            .GetRequiredService<IUserRepository>();

    var passwordHasher =
        scope.ServiceProvider
            .GetRequiredService<IPasswordHasher>();

    await DatabaseSeeder.SeedAsync(
        userRepository,
        passwordHasher);
}

#endregion

#region Middleware

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "ERP API V1");

    options.SwaggerEndpoint(
        "/swagger/v2/swagger.json",
        "ERP API V2");
});

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

#endregion

app.Run();