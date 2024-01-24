using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.EntityFrameworkCore;

namespace Trello_back.Models;

public partial class TrelloContext : DbContext
{
    private readonly IConfiguration _configuration;
    public TrelloContext()
    {
        // Create the database if it doesn't exist
        Database.EnsureCreated();
        // Create the tables if they don't exist
        Database.Migrate();
    }

    public TrelloContext(DbContextOptions<TrelloContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        // {
        //     string connectionString = _configuration.GetConnectionString("MyConnectionString");

        //     // Configurer Entity Framework pour utiliser SQL Server avec la chaîne de connexion
        //     optionsBuilder.UseSqlServer(connectionString);
        // }
        {
            string connectionString = _configuration.GetConnectionString("MySqliteString");

            // Configurer Entity Framework pour utiliser SQL Server avec la chaîne de connexion
            optionsBuilder.UseSqlite(connectionString);
        }
    }
    // =>
    // optionsBuilder.UseSqlServer(
    //     ConnectionStrings
    //     );

    // => optionsBuilder.UseSqlite("Data Source=Trello.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME").HasColumnName("createdAt");
            entity
                .Property(e => e.Description)
                .HasColumnType("varchar (50)")
                .HasColumnName("description");
            entity.Property(e => e.IdList).HasColumnType("INT").HasColumnName("idList");
            entity.Property(e => e.Title).HasColumnType("varchar (50)").HasColumnName("title");

            entity
                .HasOne(d => d.IdListNavigation)
                .WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Content).HasColumnType("varchar (50)").HasColumnName("content");
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME").HasColumnName("createdAt");
            entity.Property(e => e.IdCard).HasColumnType("INT").HasColumnName("idCard");
            entity.Property(e => e.User).HasColumnType("varchar (50)").HasColumnName("user");

            entity
                .HasOne(d => d.IdCardNavigation)
                .WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdCard)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.ToTable("List");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdProject).HasColumnType("INT").HasColumnName("idProject");
            entity.Property(e => e.Name).HasColumnType("varchar (50)").HasColumnName("name");

            entity
                .HasOne(d => d.IdProjectNavigation)
                .WithMany(p => p.Lists)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnType("DATETIME").HasColumnName("createdAt");
            entity
                .Property(e => e.Description)
                .HasColumnType("varchar (50)")
                .HasColumnName("description");
            entity.Property(e => e.Name).HasColumnType("varchar (50)").HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
