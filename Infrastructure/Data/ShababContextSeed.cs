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
        }
    }
}
