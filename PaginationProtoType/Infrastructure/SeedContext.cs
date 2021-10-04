using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaginationProtoType.Infrastructure
{
    public class SeedContext
    {
        public static async Task SeedDataAsync(ApplicationDbContext context)
        {
            if(!context.Jobs.Any())
            {
                for (int i = 0; i < 100000; i++)
                {
                    await context.Jobs.AddAsync(new Models.Job()
                    {
                        Title = "Job Title" + i + 1
                    });
                }
            }

            await context.SaveChangesAsync();
        }
    }
}
