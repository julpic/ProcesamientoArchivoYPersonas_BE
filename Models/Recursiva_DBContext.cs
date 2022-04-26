using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Configuration;

namespace WebApplication4.Models
{
    public partial class Recursiva_DBContext : DbContext
    {
        public Recursiva_DBContext()
        {
        }

        public Recursiva_DBContext(DbContextOptions<Recursiva_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Persona> Personas { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148.//For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLExpress;Database=Recursiva_DB;User Id=usuario2;Password=usuario2");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(entity =>
            {
                entity.ToTable("Persona");

                entity.Property(e => e.ClubDeportivo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clubDeportivo");

                entity.Property(e => e.EstadoCivil)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("estadoCivil");

                entity.Property(e => e.NivelEstudios)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nivelEstudios");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
