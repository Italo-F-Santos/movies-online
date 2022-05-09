using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MoviesOnline.Models;
using System;
using System.IO;

namespace MoviesOnline.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Watching> Watching { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            // get the configuration from the app settings
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environment}.json", true)
                .Build();

            var connection = config.GetConnectionString("DefaultConnectionString");

            // define the database to use
            if (!string.IsNullOrEmpty(connection))
            {
                optionsBuilder.UseSqlServer(connection);
            }
        }

    }
}
