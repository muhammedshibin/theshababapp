using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Infrastructure.Identity.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration config)
        {
            services.AddDbContext<ShababContext>(options => {
                var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                string connStr;

                // Depending on if in development or production, use either Heroku-provided
                // connection string, or development connection string from env var.
                if (env == "Development")
                {
                    // Use connection string from file.
                    connStr = config.GetConnectionString("DefaultConnection");
                }
                else
                {
                    // Use connection string provided at runtime by Heroku.
                    var connUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

                    // Parse connection URL to connection string for Npgsql
                    connUrl = connUrl.Replace("postgres://", string.Empty);
                    var pgUserPass = connUrl.Split("@")[0];
                    var pgHostPortDb = connUrl.Split("@")[1];
                    var pgHostPort = pgHostPortDb.Split("/")[0];
                    var pgDb = pgHostPortDb.Split("/")[1];
                    var pgUser = pgUserPass.Split(":")[0];
                    var pgPass = pgUserPass.Split(":")[1];
                    var pgHost = pgHostPort.Split(":")[0];
                    var pgPort = pgHostPort.Split(":")[1];

                    connStr = $"Server={pgHost};Port={pgPort};User Id={pgUser};Password={pgPass};Database={pgDb};SSL Mode=Require;TrustServerCertificate=True";
                }

                // Whether the connection string came from the local development configuration file
                // or from the environment variable from Heroku, use it to set up your DbContext.
                options.UseNpgsql(connStr);

            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IInmateService, InmateService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure((Action<ApiBehaviorOptions>)(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
               {
                   var modelState = context.ModelState;
                   var modelStateErrors = modelState.Where(e => e.Value.Errors.Count > 0)
                                                            .SelectMany(e => e.Value.Errors)
                                                            .Select(e => e.ErrorMessage).ToList();
                   return new BadRequestObjectResult(new ErrorResponse
                   {
                       IsError = true,
                       UserMessage = "Invalid Request" , StatusCode = (int)HttpStatusCode.BadRequest ,
                       Errors = modelStateErrors});
               };
            }));
            return services;

        }
    }
}
