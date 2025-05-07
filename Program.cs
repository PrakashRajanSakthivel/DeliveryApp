using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DeliveryApp.src.services.OrderService.OrderService.Infra.Data;
using DeliveryApp.src.services.OrderService.OrderService.Infra.Repository;
using DeliveryApp.src.shared.Infra;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore; // Fix for CS1061: Add the required namespace for UseSqlServer
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens; // Fix for CS0117: Ensure the correct namespace for ConfigurationManager



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<OrderDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("OrderDatabase"))); // Fix for CS0117: Use builder.Configuration instead of Configuration

builder.Services.AddOrderServiceInfrastructure();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]))
        };
    });


builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/env", (IWebHostEnvironment env) => env.EnvironmentName);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.MapPost("/api/auth/token", () =>
    {
        var issuer = builder.Configuration["Jwt:Issuer"]!;
        var audience = builder.Configuration["Jwt:Audience"]!;
        var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!);

        // Hardcoded test user claims
        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, "test-user-123"),
        new Claim(JwtRegisteredClaimNames.Name, "Test User"),
        new Claim(JwtRegisteredClaimNames.Email, "test.user@example.com"),
        new Claim("role", "Admin") // Custom claim
    };

        var securityKey = new SymmetricSecurityKey(key);
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1), // Token valid for 1 day
            signingCredentials: credentials
        );

        return Results.Ok(new
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    })
.WithName("GetDevToken")
.AllowAnonymous() // Bypass auth for this endpoint
.WithTags("Dev Tools");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
