using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace bichos.Models.Entities;

public partial class PixarContext : DbContext
{
    public PixarContext()
    {
    }

    public PixarContext(DbContextOptions<PixarContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Apariciones> Apariciones { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Cortometraje> Cortometraje { get; set; }

    public virtual DbSet<Pelicula> Pelicula { get; set; }

    public virtual DbSet<Personaje> Personaje { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;user=root;database=pixar;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Apariciones>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("apariciones")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdPelicula, "fkPelicula_idx");

            entity.HasIndex(e => e.IdPersonaje, "fkPersonaje_idx");

            entity.HasOne(d => d.IdPeliculaNavigation).WithMany(p => p.Apariciones)
                .HasForeignKey(d => d.IdPelicula)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPelicula");

            entity.HasOne(d => d.IdPersonajeNavigation).WithMany(p => p.Apariciones)
                .HasForeignKey(d => d.IdPersonaje)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPersonaje");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("categoria")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<Cortometraje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("cortometraje")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.HasIndex(e => e.IdCategoria, "fkCategoria_idx");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(45);

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Cortometraje)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fkCategoria");
        });

        modelBuilder.Entity<Pelicula>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("pelicula")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Descripción).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.NombreOriginal).HasMaxLength(100);
        });

        modelBuilder.Entity<Personaje>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("personaje")
                .HasCharSet("utf8mb3")
                .UseCollation("utf8mb3_general_ci");

            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
