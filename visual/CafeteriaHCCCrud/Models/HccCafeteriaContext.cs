using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CafeteriaHCCCrud.Models
{
    public partial class HccCafeteriaContext : DbContext
    {
        public HccCafeteriaContext()
        {
        }

        public HccCafeteriaContext(DbContextOptions<HccCafeteriaContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TbHccAlmacen> TbHccAlmacens { get; set; } = null!;
        public virtual DbSet<TbHccDetallesOrden> TbHccDetallesOrdens { get; set; } = null!;
        public virtual DbSet<TbHccMesa> TbHccMesas { get; set; } = null!;
        public virtual DbSet<TbHccOrdene> TbHccOrdenes { get; set; } = null!;
        public virtual DbSet<TbHccProducto> TbHccProductos { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                //optionsBuilder.UseSqlServer("server=localhost; database=HccCafeteria; integrated security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TbHccAlmacen>(entity =>
            {
                entity.ToTable("Tb_HccAlmacen");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.TbHccAlmacens)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tb_HccAlm__Produ__2E1BDC42");
            });

            modelBuilder.Entity<TbHccDetallesOrden>(entity =>
            {
                entity.ToTable("Tb_HccDetallesOrden");

                entity.HasOne(d => d.Orden)
                    .WithMany(p => p.TbHccDetallesOrdens)
                    .HasForeignKey(d => d.OrdenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tb_HccDet__Orden__36B12243");

                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.TbHccDetallesOrdens)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tb_HccDet__Produ__37A5467C");
            });

            modelBuilder.Entity<TbHccMesa>(entity =>
            {
                entity.ToTable("Tb_HccMesas");

                entity.Property(e => e.Disponible)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TbHccOrdene>(entity =>
            {
                entity.ToTable("Tb_HccOrdenes");

                entity.Property(e => e.FechaCreacion)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Mesa)
                    .WithMany(p => p.TbHccOrdenes)
                    .HasForeignKey(d => d.MesaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tb_HccOrd__MesaI__31EC6D26");
            });

            modelBuilder.Entity<TbHccProducto>(entity =>
            {
                entity.ToTable("Tb_HccProductos");

                entity.Property(e => e.Activo)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
