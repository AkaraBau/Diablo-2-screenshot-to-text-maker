using System;
using System.Collections;
using System.Collections.Generic;
using DiabloItemMuleSystem;
using Microsoft.EntityFrameworkCore; 

namespace DiabloItemMuleSystem
{
    public class ItemDbContext : DbContext
    {
        public DbSet<Item> ItemTable { get; set; } // Represents the item table.
        public DbSet<Stats> StatsTable { get; set; } // Represents the stats table 


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) //override for configuration 
        {
            optionsBuilder.UseMySql(
                "Server=localhost;Database=itemdb;User=root;Password=To7opxv9!;", // sql server string
                new MySqlServerVersion(new Version(8, 0, 40)) // sql version 
            );
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) // override method
        {

            modelBuilder.Entity<Item>(item =>
            {
                item.ToTable("Items"); // Explicitly maps to the "Items" table
                item.Property(i => i.Id).HasColumnName("Id");
                item.Property(i => i.Name).HasColumnName("Name").IsRequired();
                item.Property(i => i.Level).HasColumnName("Level").IsRequired();
                item.Property(i => i.LevelRequirement).HasColumnName("LevelRequirement").IsRequired();
            });

            modelBuilder.Entity<Stats>(stats =>
            {
                stats.ToTable("Stats"); // Explicitly maps to the "stats" table
                // stats.HasKey(s => s.StatsId); // telling the code where the PK is comment to remember how to reverse also a reminder how to do it. 
                stats.Property(s => s.StatsId).HasColumnName("StatsId");
                stats.Property(s => s.ItemId).HasColumnName("ItemId");
                stats.Property(s => s.Amount).HasColumnName("Amount");
                stats.Property(s => s.Name).HasColumnName("Name");

            });

        }
    }
}