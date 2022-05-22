using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL_KTPM.Data.EF
{
    public class BTL_KTPMDbContextFactory : IDesignTimeDbContextFactory<BTL_KTPMDbContext>
    {
        public BTL_KTPMDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("datasetting.json")
                .Build();
            var optionBuilder = new DbContextOptionsBuilder<BTL_KTPMDbContext>();
            var ConnectionString = configuration.GetConnectionString("BTL_KTPM_Database");
            optionBuilder.UseSqlServer(ConnectionString);
            return new BTL_KTPMDbContext(optionBuilder.Options);
        }
    }
}
