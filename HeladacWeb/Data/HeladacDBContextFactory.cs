using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HeladacWeb.Data
{
    public class HeladacDBContextFactory : IDesignTimeDbContextFactory<HeladacDbContext>
    {
        public HeladacDbContext CreateDbContext(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json").Build();

            var builder = new DbContextOptionsBuilder<HeladacDbContext>();
            var connectionString = configuration.GetConnectionString(configuration["ConnectionName"]);
            builder.UseSqlServer(connectionString);

            return new HeladacDbContext(builder.Options, new OperationalStoreOptionsMigrations());
        }
    }
}
