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

    public virtual DbSet<Carte> Cartes { get; set; }

    public virtual DbSet<Commentaire> Commentaires { get; set; }

    public virtual DbSet<Liste> Listes { get; set; }

    public virtual DbSet<Projet> Projets { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=Trello.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Carte>(entity =>
        {
            entity.ToTable("Carte");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT");
            entity.Property(e => e.DateCreation).HasColumnType("DATETIME");
            entity.Property(e => e.Description).HasColumnType("varchar (50)");
            entity.Property(e => e.IdListe).HasColumnType("INT");
            entity.Property(e => e.Titre).HasColumnType("varchar (50)");

            entity.HasOne(d => d.IdListeNavigation).WithMany(p => p.Cartes).HasForeignKey(d => d.IdListe);
        });

        modelBuilder.Entity<Commentaire>(entity =>
        {
            entity.ToTable("Commentaire");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT");
            entity.Property(e => e.Contenu).HasColumnType("varchar (50)");
            entity.Property(e => e.DateCreation).HasColumnType("DATETIME");
            entity.Property(e => e.IdCarte).HasColumnType("INT");
            entity.Property(e => e.Utilisateur).HasColumnType("varchar (50)");

            entity.HasOne(d => d.IdCarteNavigation).WithMany(p => p.Commentaires).HasForeignKey(d => d.IdCarte);
        });

        modelBuilder.Entity<Liste>(entity =>
        {
            entity.ToTable("Liste");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IdProjet).HasColumnType("INT");
            entity.Property(e => e.Nom).HasColumnType("varchar (50)");

            entity.HasOne(d => d.IdProjetNavigation).WithMany(p => p.Listes).HasForeignKey(d => d.IdProjet);
        });

        modelBuilder.Entity<Projet>(entity =>
        {
            entity.ToTable("Projet");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnType("INT")
                .HasColumnName("id");
            entity.Property(e => e.DateCreation).HasColumnType("DATETIME");
            entity.Property(e => e.Description).HasColumnType("varchar (50)");
            entity.Property(e => e.Nom).HasColumnType("varchar (50)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
