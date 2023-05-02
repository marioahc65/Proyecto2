using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Proyecto2API.Models;

namespace Proyecto2API.Data
{
    public partial class ContextDb : DbContext
    {
        public ContextDb()
        {
        }

        public ContextDb(DbContextOptions<ContextDb> options)
            : base(options) => Database.EnsureCreated();

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Libro> Libros { get; set; } = null!;
        public virtual DbSet<LibroStock> LibroStocks { get; set; } = null!;
        public virtual DbSet<LibroRetirado> LibrosRetirados { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=SQLServerConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.ClienteId).HasColumnName("ClienteID"); 

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.ClienteId).UseIdentityColumn(seed: 10000, increment: 1);

            });

            modelBuilder.Entity<Libro>(entity =>
            {
                entity.Property(e => e.LibroId).HasColumnName("LibroID");

                entity.Property(e => e.Empresa).IsUnicode(false);

                entity.Property(e => e.Nombre).IsUnicode(false);

                entity.Property(e => e.LibroId).UseIdentityColumn(seed: 10, increment: 1);

            });

            modelBuilder.Entity<LibroStock>(entity =>
            {
                entity.ToTable("LibroStock");

                entity.Property(e => e.LibroStockId).HasColumnName("LibroStockID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaIngreso).HasColumnType("date");

                entity.Property(e => e.LibroId).HasColumnName("LibroID");

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.LibroStocks)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__LibroStoc__Clien__4E88ABD4");

                entity.HasOne(d => d.Libro)
                    .WithMany(p => p.LibroStocks)
                    .HasForeignKey(d => d.LibroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LibroStoc__Libro__4D94879B");
            });

            modelBuilder.Entity<LibroRetirado>(entity =>
            {
                entity.HasKey(e => e.LibroRetiradoId)
                    .HasName("PK__LibrosRe__D0A9D53344B11174");

                entity.Property(e => e.LibroRetiradoId).HasColumnName("LibrosRetiradosID");

                entity.Property(e => e.ClienteId).HasColumnName("ClienteID");

                entity.Property(e => e.Descripcion).IsUnicode(false);

                entity.Property(e => e.FechaRetiro).HasColumnType("date");

                entity.Property(e => e.LibroId).HasColumnName("LibroID");

                entity.Property(e => e.NombreLibro).IsUnicode(false);

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.LibrosRetirados)
                    .HasForeignKey(d => d.ClienteId)
                    .HasConstraintName("FK__LibrosRet__Clien__52593CB8");

                entity.HasOne(d => d.Libro)
                    .WithMany(p => p.LibrosRetirados)
                    .HasForeignKey(d => d.LibroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LibrosRet__Libro__5165187F");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
