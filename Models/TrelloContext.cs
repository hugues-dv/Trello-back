using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Trello_back.Models;

public partial class TrelloContext : DbContext
{
    public TrelloContext()
    {
    }

    public TrelloContext(DbContextOptions<TrelloContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Trello.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.ToTable("Card");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("varchar (50)")
                .HasColumnName("description");
            entity.Property(e => e.IdList)
                .HasColumnType("INT")
                .HasColumnName("idList");
            entity.Property(e => e.Title)
                .HasColumnType("varchar (50)")
                .HasColumnName("title");

            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.Cards)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comment");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.Content)
                .HasColumnType("varchar (50)")
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME")
                .HasColumnName("createdAt");
            entity.Property(e => e.IdCard)
                .HasColumnType("INT")
                .HasColumnName("idCard");
            entity.Property(e => e.User)
                .HasColumnType("varchar (50)")
                .HasColumnName("user");

            entity.HasOne(d => d.IdCardNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdCard)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<List>(entity =>
        {
            entity.ToTable("List");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.IdProject)
                .HasColumnType("INT")
                .HasColumnName("idProject");
            entity.Property(e => e.Name)
                .HasColumnType("varchar (50)")
                .HasColumnName("name");

            entity.HasOne(d => d.IdProjectNavigation).WithMany(p => p.Lists)
                .HasForeignKey(d => d.IdProject)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.ToTable("Project");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("DATETIME")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("varchar (50)")
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasColumnType("varchar (50)")
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
