using System;
using System.Collections.Generic;
using AEC_DataAccess.DBModels;
using AEC_Entities.Configuration;
using Microsoft.EntityFrameworkCore;
using Monitor = AEC_DataAccess.DBModels.Monitor;

namespace AEC_DataAccess.DBContext;

public partial class AecommerceDbContext : DbContext
{
    public AecommerceDbContext()
    {
    }

    public AecommerceDbContext(DbContextOptions<AecommerceDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cozunurluk> Cozunurluks { get; set; }

    public virtual DbSet<Cpu> Cpus { get; set; }

    public virtual DbSet<Gpu> Gpus { get; set; }

    public virtual DbSet<Hakkimizdum> Hakkimizda { get; set; }

    public virtual DbSet<IsletimSistemi> IsletimSistemis { get; set; }

    public virtual DbSet<Kategori> Kategoris { get; set; }

    public virtual DbSet<Kullanici> Kullanicis { get; set; }

    public virtual DbSet<KullaniciKart> KullaniciKarts { get; set; }

    public virtual DbSet<KullaniciTuru> KullaniciTurus { get; set; }

    public virtual DbSet<Laptop> Laptops { get; set; }

    public virtual DbSet<Monitor> Monitors { get; set; }

    public virtual DbSet<Ram> Rams { get; set; }

    public virtual DbSet<Ssd> Ssds { get; set; }

    public virtual DbSet<UrunDurum> UrunDurums { get; set; }

    public virtual DbSet<UrunResmi> UrunResmis { get; set; }

    public virtual DbSet<UrunYorum> UrunYorums { get; set; }

    public virtual DbSet<YenilemeHizi> YenilemeHizis { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = ConfigurationInfo.ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cozunurluk>(entity =>
        {
            entity.ToTable("Cozunurluk");

            entity.Property(e => e.CozunurlukAdi).HasMaxLength(50);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.CozunurlukNavigation).WithMany(p => p.Cozunurluks)
                .HasForeignKey(d => d.CozunurlukId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cozunurluk_Kategori");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Cozunurluks)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Cozunurluk_Kullanici");
        });

        modelBuilder.Entity<Cpu>(entity =>
        {
            entity.ToTable("CPU");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.IslemciAdi).HasMaxLength(200);
            entity.Property(e => e.IslemciMimarisi).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Cpus)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CPU_Kullanici");

            entity.HasOne(d => d.IslemciSerisi).WithMany(p => p.Cpus)
                .HasForeignKey(d => d.IslemciSerisiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CPU_Kategori");
        });

        modelBuilder.Entity<Gpu>(entity =>
        {
            entity.ToTable("GPU");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EkranKartiAdi).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Gpus)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GPU_Kullanici");

            entity.HasOne(d => d.EkranKartiSerisi).WithMany(p => p.Gpus)
                .HasForeignKey(d => d.EkranKartiSerisiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_GPU_Kategori");
        });

        modelBuilder.Entity<Hakkimizdum>(entity =>
        {
            entity.Property(e => e.Adres).HasMaxLength(250);
            entity.Property(e => e.CalismaGunleri).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.Hikaye).HasMaxLength(1000);
            entity.Property(e => e.Misyon).HasMaxLength(1000);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Vizyon).HasMaxLength(1000);

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.Hakkimizda)
                .HasForeignKey(d => d.UpdatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Hakkimizda_Kullanici");
        });

        modelBuilder.Entity<IsletimSistemi>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_OperatingSystem");

            entity.ToTable("IsletimSistemi");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.IsletimSistemiAdi).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.IsletimSistemis)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IsletimSistemi_Kullanici");

            entity.HasOne(d => d.IsletimSistemiNavigation).WithMany(p => p.IsletimSistemis)
                .HasForeignKey(d => d.IsletimSistemiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_IsletimSistemi_Kategori");
        });

        modelBuilder.Entity<Kategori>(entity =>
        {
            entity.ToTable("Kategori");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.KategoriAdi).HasMaxLength(100);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Kategoris)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kategori_Kullanici");

            entity.HasOne(d => d.MainKategori).WithMany(p => p.InverseMainKategori)
                .HasForeignKey(d => d.MainKategoriId)
                .HasConstraintName("FK_Kategori_Kategori");
        });

        modelBuilder.Entity<Kullanici>(entity =>
        {
            entity.ToTable("Kullanici");

            entity.Property(e => e.Ad).HasMaxLength(50);
            entity.Property(e => e.Adres).HasMaxLength(250);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.KullaniciAdi).HasMaxLength(50);
            entity.Property(e => e.Sifre).HasMaxLength(50);
            entity.Property(e => e.Soyad).HasMaxLength(50);
            entity.Property(e => e.Telefon)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.KullaniciTuru).WithMany(p => p.Kullanicis)
                .HasForeignKey(d => d.KullaniciTuruId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Kullanici_KullaniciTuru");
        });

        modelBuilder.Entity<KullaniciKart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Card");

            entity.ToTable("KullaniciKart");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.KartAdSoyad).HasMaxLength(100);
            entity.Property(e => e.KartAdi).HasMaxLength(50);
            entity.Property(e => e.KartCvvkodu).HasColumnName("KartCVVKodu");
            entity.Property(e => e.KartNumarasi)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.KartSktay).HasColumnName("KartSKTAy");
            entity.Property(e => e.KartSktyil).HasColumnName("KartSKTYil");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.KullaniciKartCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_KullaniciKart_Kullanici");

            entity.HasOne(d => d.Kullanici).WithMany(p => p.KullaniciKartKullanicis)
                .HasForeignKey(d => d.KullaniciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KullaniciKart_KullaniciId");
        });

        modelBuilder.Entity<KullaniciTuru>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_KullaniciTipi");

            entity.ToTable("KullaniciTuru");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.TurAdi).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.KullaniciTurus)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_KullaniciTuru_Kullanici");
        });

        modelBuilder.Entity<Laptop>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Notebook");

            entity.ToTable("Laptop");

            entity.Property(e => e.Agirlik).HasMaxLength(100);
            entity.Property(e => e.Batarya).HasMaxLength(100);
            entity.Property(e => e.Boyut).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Fiyat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Klavye).HasMaxLength(100);
            entity.Property(e => e.LaptopAdi).HasMaxLength(100);

            entity.HasOne(d => d.Bellek).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.BellekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_RAM");

            entity.HasOne(d => d.Cozunurluk).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.CozunurlukId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_Cozunurluk");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_Kullanici");

            entity.HasOne(d => d.Depolama).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.DepolamaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_SSD");

            entity.HasOne(d => d.EkranKarti).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.EkranKartiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_GPU");

            entity.HasOne(d => d.Islemci).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.IslemciId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_CPU");

            entity.HasOne(d => d.IsletimSistemi).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.IsletimSistemiId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_IsletimSistemi");

            entity.HasOne(d => d.YenilemeHizi).WithMany(p => p.Laptops)
                .HasForeignKey(d => d.YenilemeHiziId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Laptop_YenilemeHizi");
        });

        modelBuilder.Entity<Monitor>(entity =>
        {
            entity.ToTable("Monitor");

            entity.Property(e => e.Agirlik).HasMaxLength(100);
            entity.Property(e => e.Boyut).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.EkranYapisi).HasMaxLength(100);
            entity.Property(e => e.Fiyat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Hdr)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("HDR");
            entity.Property(e => e.MonitorAdi).HasMaxLength(100);

            entity.HasOne(d => d.Cozunurluk).WithMany(p => p.Monitors)
                .HasForeignKey(d => d.CozunurlukId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Monitor_Cozunurluk");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Monitors)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Monitor_Kullanici");

            entity.HasOne(d => d.YenilemeHizi).WithMany(p => p.Monitors)
                .HasForeignKey(d => d.YenilemeHiziId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Monitor_YenilemeHizi");
        });

        modelBuilder.Entity<Ram>(entity =>
        {
            entity.ToTable("RAM");

            entity.Property(e => e.BellekAdi).HasMaxLength(200);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Bellek).WithMany(p => p.Rams)
                .HasForeignKey(d => d.BellekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RAM_Kategori");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Rams)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RAM_Kullanici");
        });

        modelBuilder.Entity<Ssd>(entity =>
        {
            entity.ToTable("SSD");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DepolamaAdi).HasMaxLength(200);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Ssds)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSD_Kullanici");

            entity.HasOne(d => d.Depolama).WithMany(p => p.Ssds)
                .HasForeignKey(d => d.DepolamaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SSD_Kategori");
        });

        modelBuilder.Entity<UrunDurum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Sepet");

            entity.ToTable("UrunDurum");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UrunDurumCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UrunDurum_Kullanici1");

            entity.HasOne(d => d.Laptop).WithMany(p => p.UrunDurums)
                .HasForeignKey(d => d.LaptopId)
                .HasConstraintName("FK_UrunDurum_Laptop");

            entity.HasOne(d => d.Monitor).WithMany(p => p.UrunDurums)
                .HasForeignKey(d => d.MonitorId)
                .HasConstraintName("FK_UrunDurum_Monitor");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.UrunDurumUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("FK_UrunDurum_Kullanici");
        });

        modelBuilder.Entity<UrunResmi>(entity =>
        {
            entity.ToTable("UrunResmi");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.ResimBoyutu).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ResimTuru).HasMaxLength(20);
            entity.Property(e => e.ResimUrl).HasMaxLength(300);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UrunResmis)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UrunResmi_Kullanici");

            entity.HasOne(d => d.Laptop).WithMany(p => p.UrunResmis)
                .HasForeignKey(d => d.LaptopId)
                .HasConstraintName("FK_UrunResmi_Laptop");

            entity.HasOne(d => d.Monitor).WithMany(p => p.UrunResmis)
                .HasForeignKey(d => d.MonitorId)
                .HasConstraintName("FK_UrunResmi_Monitor");
        });

        modelBuilder.Entity<UrunYorum>(entity =>
        {
            entity.ToTable("UrunYorum");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Yorum).HasMaxLength(500);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.UrunYorums)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UrunYorum_Kullanici");

            entity.HasOne(d => d.Laptop).WithMany(p => p.UrunYorums)
                .HasForeignKey(d => d.LaptopId)
                .HasConstraintName("FK_UrunYorum_Laptop");

            entity.HasOne(d => d.Monitor).WithMany(p => p.UrunYorums)
                .HasForeignKey(d => d.MonitorId)
                .HasConstraintName("FK_UrunYorum_Monitor");
        });

        modelBuilder.Entity<YenilemeHizi>(entity =>
        {
            entity.ToTable("YenilemeHizi");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.YenilemeHiziAdi).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.YenilemeHizis)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YenilemeHizi_Kullanici");

            entity.HasOne(d => d.YenilemeHiziNavigation).WithMany(p => p.YenilemeHizis)
                .HasForeignKey(d => d.YenilemeHiziId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_YenilemeHizi_Kategori");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
