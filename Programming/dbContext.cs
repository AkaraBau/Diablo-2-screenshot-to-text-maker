using System;
using System.Collections.Generic;
using DiabloItemMuleSystem;
using Microsoft.EntityFrameworkCore; 


public class ItemDbContext : DbContext
{
    public DbSet<Item> ItemTable { get; set; } // Represents the item table.

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql(
            "Server=localhost;Database=itemdb;User=root;Password=To7opxv9!;",
            new MySqlServerVersion(new Version(8, 0, 40)) // Replace with your MySQL version.
        );
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<string> statParameters = new List<string> { "FCR", "FHR", "STR", "DEX", "LL", "VITA", "ENERGY", "ML", "LIFE", "REP", "MANA", "MREG", "PR", "LR", "FR", "PLR", "ED", "GOLD" };

        modelBuilder.Entity<Item>(item =>
        {
            item.ToTable("items"); // Explicitly maps to the "Items" table
            item.Property(i => i.Id).HasColumnName("id");
            item.Property(i => i.Name).HasColumnName("item_type").IsRequired();
            item.Property(i => i.Level).HasColumnName("level_requirement").IsRequired();
        });

        modelBuilder.Entity<Item>(item =>
        {
            item.ToTable("stats"); // Explicitly maps to the "Items" table
            item.Property(i => i.Id).HasColumnName("id");
            foreach (var s in statParameters)
            {
                item.Property(i => i.GetStat(s).Amount).HasColumnName(s);
            }
        });

    }
}
