
using licensemanager.Model.DataBaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MySQL.Data.Entity.Extensions;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// The entity framework context with a DataBaseContext DbSet
/// </summary>
public class DataBaseContext : DbContext
        {
        
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
            var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            string connectionString = configuration.GetConnectionString("DbConnection");

                optionsBuilder.UseMySQL(connectionString);
            }
            public DbSet<Users> Users { get; set; }
        }

