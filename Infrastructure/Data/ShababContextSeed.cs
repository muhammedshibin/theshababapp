using Core.Entities;
using Core.Enumerations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShababContextSeed
    {

        public static async Task Seed(ShababContext context, IHostEnvironment env)
        {
            var isCategoriesAdded = await context.Categories.AnyAsync();

            if (!isCategoriesAdded)
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "RENT",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 10000,
                        IsApplicableForVisitors = true,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = false,
                        ConsiderDefaultRate = true,
                        CoreCategory =true,
                        Id =1
                    },
                    new Category
                    {
                        Name = "MESS",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 0,
                        IsApplicableForVisitors = true,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = true,
                        CoreCategory = true,
                        Id = 2,
                    },
                    new Category
                    {
                        Name = "USTHADSALARY",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 2500,
                        IsApplicableForVisitors = true,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = true,
                        ConsiderDefaultRate = true,
                        Id = 3,
                    },
                     new Category
                    {
                        Name = "BILLPAYMENT",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 0,
                        IsApplicableForVisitors = true,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = true,
                        ConsiderDefaultRate = true,
                        Id = 4,
                    },
                    new Category
                    {
                        Name = "DEPOSIT",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 100,
                        IsApplicableForVisitors = false,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = false,
                        Id = 5
                    },
                    new Category
                    {
                        Name = "SETTLEMENT",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 0,
                        IsApplicableForVisitors = true,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = false,
                        Id = 6
                    }
                };

                context.AddRange(categories);
                await context.SaveChangesAsync();
            }

            var vendorsPresent = await context.Vendors.AnyAsync();

            if (!vendorsPresent)
            {
                var mainAccount = new Vendor
                {
                    Name = VendorNames.Main,
                    CreatedBy = "Admin",
                    AmountInHand = 0,
                    DueAmount = 0
                };

                context.Vendors.Add(mainAccount);

                var othersAccount = new Vendor
                {
                    Name = VendorNames.Others,
                    CreatedBy = "Admin",
                    AmountInHand = 0,
                    DueAmount = 0
                };

                context.Vendors.Add(othersAccount);

                var billPaymentAccount = new Vendor
                {
                    Name = VendorNames.BillPaymentAccount,
                    CreatedBy = "Admin",
                    AmountInHand = 0,
                    DueAmount = 0
                };

                context.Vendors.Add(billPaymentAccount);

                await context.SaveChangesAsync();
            }


            if (env.IsDevelopment())
            {
                var inamtesPresent = await context.Inmates.AnyAsync();
                
                if (!inamtesPresent) {
                    var inmates = new List<Inmate>
                {
                    new Inmate
                    {
                        FullName = "Muhammed Shibin",
                        EmailAddress = "shibin@gmail.com",
                        PhoneNumber = "4435435353",
                        DateOfBirth = DateTime.Now.AddYears(-29),
                        Address = "Cholayil House",
                        IsInmateOnTopBed = true,
                        IsVisit = false,
                        Status = InmateStatus.Active
                    },
                     new Inmate
                    {
                        FullName = "Asmabi",
                        EmailAddress = "shibin@gmail.com",
                        PhoneNumber = "4435435353",
                        DateOfBirth = DateTime.Now.AddYears(-48),
                        Address = "Cholayil House",
                        IsInmateOnTopBed = true,
                        IsVisit = false,
                        Status = InmateStatus.Active
                    },
                      new Inmate
                    {
                        FullName = "Yoosuf Ali",
                        EmailAddress = "yoosuf@gmail.com",
                        PhoneNumber = "4435435353",
                        DateOfBirth = DateTime.Now.AddYears(-55),
                        Address = "Cholayil House",
                        IsInmateOnTopBed = true,
                        IsVisit = false,
                        Status = InmateStatus.Active
                    },
                       new Inmate
                    {
                        FullName = "Fathima Shibin",
                        EmailAddress = "fathima@gmail.com",
                        PhoneNumber = "4435435353",
                        DateOfBirth = DateTime.Now.AddYears(-23),
                        Address = "Cholayil House",
                        IsInmateOnTopBed = true,
                        IsVisit = false,
                        Status = InmateStatus.Active
                    },
                        new Inmate
                    {
                        FullName = "Saima Yoosuf",
                        EmailAddress = "saima@gmail.com",
                        PhoneNumber = "4435435353",
                        DateOfBirth = DateTime.Now.AddYears(-20),
                        Address = "Cholayil House",
                        IsInmateOnTopBed = true,
                        IsVisit = false,
                        Status = InmateStatus.Active
                    }
                };
                    context.Inmates.AddRange(inmates);
                    await context.SaveChangesAsync();
                }                
            }
        }
    }
}
