using ERP.API.Middleware;
using ERP.API.Validations;
using ERP.Application.Interfaces;
using ERP.Application.PurchaseOrders.Commands.CreatePurchaseOrder;
using ERP.Application.PurchaseOrders.Commands.UpdatePurchaseOrder;
using ERP.Infrastructure;
using ERP.Infrastructure.Persistence;
using ERP.Infrastructure.Repositories;
using ERP.Infrastructure.Security;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using System.Reflection;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine($"Environment: {builder.Environment.EnvironmentName}");
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new()
    {
        Title = "ERP API",
        Version = "v1"
    });
});

var mongoSettings = builder.Configuration
          .GetSection("MongoDb")
          .Get<MongoDbSettings>();

builder.Services.AddValidatorsFromAssemblyContaining<CreatePurchaseOrderValidator>();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(mongoSettings!.ConnectionString));
builder.Services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();

    return client.GetDatabase(
        mongoSettings.DatabaseName);
});
builder.Services.AddMediatR(
    typeof(UpdatePurchaseOrderCommand).Assembly);
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

               ValidIssuer = builder.Configuration["Jwt:Issuer"],
               ValidAudience = builder.Configuration["Jwt:Audience"],

               IssuerSigningKey =
                   new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(
                           builder.Configuration["Jwt:Key"]!)),

               RoleClaimType = ClaimTypes.Role
           };
   });
builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});
var app = builder.Build();
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

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseGlobalExceptionMiddleware();

app.UseHttpsRedirection();

app.UseCors("AngularPolicy");

app.UseAuthentication();
app.UseAuthorization();

try
{
    app.MapControllers();
}
catch (ReflectionTypeLoadException ex)
{
    foreach (var item in ex.LoaderExceptions)
    {
        Console.WriteLine(item?.Message);
    }

    throw;
}

app.Run();