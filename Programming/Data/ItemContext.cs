using System;
using System.Collections;
using System.Collections.Generic;
using DiabloItemMuleSystem;
using DiabloItemMuleSystem.Models;
using Microsoft.EntityFrameworkCore; 

namespace DiabloItemMuleSystem.Data
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Item> ItemTable { get; set; } 
        public DbSet<Stats> StatsTable { get; set; } 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //override for configuration 
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=itemdb;User=root;Password=To7opxv9!;", // sql server string
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
                // stats.HasKey(s => s.StatsId); 
                stats.Property(s => s.StatsId).HasColumnName("StatsId");
                stats.Property(s => s.ItemId).HasColumnName("ItemId");
                stats.Property(s => s.Amount).HasColumnName("Amount");
                stats.Property(s => s.Name).HasColumnName("Name");

            });

        }
    }
}