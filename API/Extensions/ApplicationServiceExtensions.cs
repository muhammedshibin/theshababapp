using API.Helpers;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Mvc;
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
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IInmateService, InmateService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddScoped<IBillService, BillService>();
            services.AddAutoMapper(typeof(MappingProfile));
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
               {
                   var modelState = context.ModelState;
                   var modelStateErrors = modelState.Where(e => e.Value.Errors.Count > 0)
                                                            .SelectMany(e => e.Value.Errors)
                                                            .Select(e => e.ErrorMessage).ToList();
                   return new BadRequestObjectResult(new ErrorResponse 
                   {IsError = true,UserMessage = "Invalid Request" , StatusCode = (int) HttpStatusCode.BadRequest ,ModelErrors = modelStateErrors});
               };
            });
            return services;

        }
    }
}
