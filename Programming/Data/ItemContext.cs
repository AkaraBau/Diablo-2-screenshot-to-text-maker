using System;
using System.Collections.Generic;
using DiabloItemMuleSystem;
using Microsoft.EntityFrameworkCore;


public class ItemDbContext : DbContext
{
    public DbSet<Item> ItemTable { get; set; } // Represents the item table.
    public DbSet<Stats> StatsTable { get; set; } //represents the stats table 

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
            item.ToTable("items"); // Explicitly maps to the "Items" table
            item.Property(i => i.Id).HasColumnName("id");
            item.Property(i => i.Name).HasColumnName("item_type").IsRequired();
            item.Property(i => i.Level).HasColumnName("level_requirement").IsRequired();
            item.Property(i => i.LevelRequirement).HasColumnName("level_string").IsRequired();
        });

        modelBuilder.Entity<Stats>(stats =>
        {
            stats.ToTable("stats"); // Explicitly maps to the "stats" table
            stats.Property(s => s.Id).HasColumnName("id");
            stats.Property(s => s.Amount).HasColumnName("Amount");
            stats.Property(s => s.Name).HasColumnName("Name");

        });

    }
}
