using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OnBusyness.Model
{
    public partial class bitbyteContext : DbContext
    {
        public bitbyteContext()
        {
        }

        public bitbyteContext(DbContextOptions<bitbyteContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblContact> TblContacts { get; set; }
        public virtual DbSet<TblContactProductInterest> TblContactProductInterests { get; set; }
        public virtual DbSet<TblFestival> TblFestivals { get; set; }
        public virtual DbSet<TblLoyalContact> TblLoyalContacts { get; set; }
        public virtual DbSet<TblLoyalContactProductIntrest> TblLoyalContactProductIntrests { get; set; }
        public virtual DbSet<TblOurProduct> TblOurProducts { get; set; }
        public virtual DbSet<TblProductBullet> TblProductBullets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;user=root;password=Vishal@123;database=bitbyte", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.32-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8mb4")
                .UseCollation("utf8mb4_0900_ai_ci");

            modelBuilder.Entity<TblContact>(entity =>
            {
                entity.HasKey(e => e.ContactId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<TblContactProductInterest>(entity =>
            {
                entity.HasKey(e => e.ContactProductId)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.TblContactsContact)
                    .WithMany(p => p.TblContactProductInterests)
                    .HasForeignKey(d => d.TblContactsContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblContactProductInterest_tblContacts1");

                entity.HasOne(d => d.TblOurProductProduct)
                    .WithMany(p => p.TblContactProductInterests)
                    .HasForeignKey(d => d.TblOurProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblContactProductInterest_tblOurProduct1");
            });

            modelBuilder.Entity<TblFestival>(entity =>
            {
                entity.HasKey(e => e.FestivalId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<TblLoyalContact>(entity =>
            {
                entity.HasKey(e => e.LoyalContactId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<TblLoyalContactProductIntrest>(entity =>
            {
                entity.HasKey(e => e.LcpId)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.TblLoyalContactLoyalContact)
                    .WithMany(p => p.TblLoyalContactProductIntrests)
                    .HasForeignKey(d => d.TblLoyalContactLoyalContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblLoyalContactProductIntrest_tblLoyalContact1");

                entity.HasOne(d => d.TblOurProductProduct)
                    .WithMany(p => p.TblLoyalContactProductIntrests)
                    .HasForeignKey(d => d.TblOurProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblLoyalContactProductIntrest_tblOurProduct1");
            });

            modelBuilder.Entity<TblOurProduct>(entity =>
            {
                entity.HasKey(e => e.ProductId)
                    .HasName("PRIMARY");
            });

            modelBuilder.Entity<TblProductBullet>(entity =>
            {
                entity.HasKey(e => e.ProductBulletId)
                    .HasName("PRIMARY");

                entity.HasOne(d => d.TblOurProductProduct)
                    .WithMany(p => p.TblProductBullets)
                    .HasForeignKey(d => d.TblOurProductProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tblProductBullets_tblOurProduct");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
