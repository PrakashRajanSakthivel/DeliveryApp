//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;
//using DeliveryApp.src.services.OrderService.OrderService.Application.Common;
//using DeliveryApp.src.services.OrderService.OrderService.Infra.Data;
//using DeliveryApp.src.services.OrderService.OrderService.Infra.Repository;
//using DeliveryApp.src.shared.Authentication;
//using DeliveryApp.src.shared.CorrelationId;
//using DeliveryApp.src.shared.DevTools;
//using DeliveryApp.src.shared.Http;
//using DeliveryApp.src.shared.Infra;
//using DeliveryApp.src.shared.Logging;
//using DeliveryApp.src.shared.Swagger;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore; // Fix for CS1061: Add the required namespace for UseSqlServer
//using Microsoft.Extensions.Configuration;
//using Microsoft.IdentityModel.Tokens;
//using Polly;
//using Polly.Extensions.Http;
//using Serilog;
//using Serilog.Sinks.Elasticsearch; // Fix for CS0117: Ensure the correct namespace for ConfigurationManager



//var builder = WebApplication.CreateBuilder(args);

//builder.Services
//    .AddControllers();

//builder.Services
//    .AddEndpointsApiExplorer()
//    .AddOrderServiceInfrastructure(builder.Configuration)
//    .AddHttpClients(builder.Configuration)
//    .AddJwtAuth(builder.Configuration)
//    .AddSwaggerSupport();

//var app = builder.Build();

//app.UseMiddleware<CorrelationIdMiddleware>();
//app.UseDefaultLogging(builder.Configuration);
//app.UseJwtAuth();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//    app.MapDevTokenGenerator(builder.Configuration); // Optional
//}

//app.UseHttpsRedirection();
//app.MapControllers();
//app.Run();
