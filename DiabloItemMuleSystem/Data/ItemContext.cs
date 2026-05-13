using System;
using DiabloItemMuleSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Data.Common;
using System.IO;
using System.Diagnostics.Metrics;
using DiabloItemMuleSystem.Utilities;

namespace DiabloItemMuleSystem.Data
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Item> Items { get; set; } 
        public DbSet<Stats> Stats { get; set; }

        readonly string ConnectionString = AppConfig.GetConfigurationFromJson().ConnectionString;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySql(
            ConnectionString, 
                new MySqlServerVersion(new Version(8, 0, 40))
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {

            modelBuilder.Entity<Item>(item =>
            {
                item.ToTable("Items"); 
                item.Property(i => i.Id).HasColumnName("Id");
                item.Property(i => i.Name).HasConversion( i => i.ToString(), x => (ItemType)Enum.Parse(typeof(ItemType),x));
                item.Property(i => i.Level).HasColumnName("Level").IsRequired();
            });

            modelBuilder.Entity<Stats>(stats =>
            {
                stats.ToTable("Stats");  
                stats.Property(s => s.StatsId).HasColumnName("StatsId");
                stats.Property(s => s.ItemId).HasColumnName("ItemId");
                stats.Property(s => s.Amount).HasColumnName("Amount");
                stats.Property(s => s.Name).HasConversion(s => s.ToString(), x => (StatType)Enum.Parse(typeof(StatType), x));

            });

        }
    }
}