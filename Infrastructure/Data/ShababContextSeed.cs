using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ShababContextSeed
    {
        public static async Task Seed(ShababContext context)
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
                        Id = 1,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = false,
                        ConsiderDefaultRate = true,
                        CoreCategory =true
                    },
                    new Category
                    {
                        Name = "MESS",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 0,
                        IsApplicableForVisitors = true,
                        Id = 2,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = true,
                        CoreCategory = true
                    },
                    new Category
                    {
                        Name = "USTHADSALARY",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 0,
                        IsApplicableForVisitors = true,
                        Id = 3,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = true,
                        ConsiderDefaultRate = true
                    },
                    new Category
                    {
                        Name = "DEPOSIT",
                        CreatedOn = DateTime.Now,
                        DefaultRate = 100,
                        IsApplicableForVisitors = false,
                        Id = 4,
                        ModfiedOn = DateTime.MinValue,
                        NeedToConsiderDays = false,
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
                    Name = "Main",
                    CreatedBy = "Admin",
                    Id = 1,
                    AmountInHand = 0,
                    DueAmount = 0
                };

                context.Vendors.Add(mainAccount);
                await context.SaveChangesAsync();
            }
        }
    }
}
