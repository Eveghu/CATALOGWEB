using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CATALOGWEB.Models;

public partial class CatwebContext : DbContext
{
    public CatwebContext()
    {
    }

    public CatwebContext(DbContextOptions<CatwebContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

   
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Idr).HasName("PK__ROL__C4971C3CEE7624C9");

            entity.ToTable("ROL");

            entity.Property(e => e.Idr).HasColumnName("IDR");
            entity.Property(e => e.Rol1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("ROL");

        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Idu).HasName("PK__USUARIO__C4971C39F31E79CE");

            entity.ToTable("USUARIO");

            entity.Property(e => e.Idu).HasColumnName("IDU");
            entity.Property(e => e.Correo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("CORREO");
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Edad).HasColumnName("EDAD");
            entity.Property(e => e.Genero)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("GENERO");
            entity.Property(e => e.Idr).HasColumnName("IDR");
            entity.Property(e => e.Nombre)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("NOMBRE");

            entity.HasOne(d => d.oRol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Idr)
                .HasConstraintName("FK_ROL");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}