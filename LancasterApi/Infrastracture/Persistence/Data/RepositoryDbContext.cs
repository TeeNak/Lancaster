using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Persistence.Data
{
    public class RepositoryDbContext : DbContext
    {
        private string ConnectionString { get; set; }

        public RepositoryDbContext() : base()
        {
            // when it comes from EF Migration
            var settingsPath = AppDomain.CurrentDomain.BaseDirectory;
            settingsPath += @"\datalayersettings.json";
            var datalayersettings = File.ReadAllText(settingsPath);
            dynamic jSetting = JObject.Parse(datalayersettings);
            this.ConnectionString =
                    (string)jSetting.ConnectionStrings.DefaultConnection;
        }

        public RepositoryDbContext(DbContextOptions options, IConfiguration config) : base(options)
        {
            this.ConnectionString = config["ConnectionStrings:DefaultConnection"];
        }

        public DbSet<FileItem> FileItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder
                                      optionsBuilder) =>
            optionsBuilder.UseSqlServer(this.ConnectionString);

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RepositoryDbContext).Assembly);

    }
}
