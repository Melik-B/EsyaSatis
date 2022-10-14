using AppCore.DataAccess.Configs;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Contexts
{
    public class EsyaSatisContext : DbContext
    {
        public DbSet<Esya> Esyalar { get; set; }
        public DbSet<Kategori> Kategoriler { get; set; }
        public DbSet<Magaza> Magazalar { get; set; }
        public DbSet<EsyaMagaza> EsyaMagazalar { get; set; }
        public DbSet<Kullanici> Kullanicilar { get; set; }
        public DbSet<KullaniciDetayi> KullaniciDetaylari { get; set; }
        public DbSet<Rol> Roller { get; set; }
        public DbSet<Ulke> Ulkeler { get; set; }
        public DbSet<Sehir> Sehirler { get; set; }
        public DbSet<Siparis> Siparisler { get; set; }
        public DbSet<EsyaSiparis> EsyaSiparisler { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"server=DESKTOP-OMQDSIF\SQLEXPRESS;database=EsyaSatis;trusted_connection=true;multipleactiveresultsets=true;";


            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Esya>()
                .ToTable("EsyaSatisEsyalar")
                .HasOne(esya => esya.Kategori)
                .WithMany(kategori => kategori.Esyalar)
                .HasForeignKey(esya => esya.KategoriId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Esya>()
                .HasMany(esya => esya.EsyaMagazalar)
                .WithOne(esyaMagaza => esyaMagaza.Esya)
                .HasForeignKey(EsyaMagaza => EsyaMagaza.EsyaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Magaza>()
                .ToTable("EsyaSatisMagazalar")
                .HasMany(magaza => magaza.EsyaMagazalar)
                .WithOne(esyaMagaza => esyaMagaza.Magaza)
                .HasForeignKey(esyaMagaza => esyaMagaza.MagazaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Kategori>()
                .ToTable("EsyaSatisKategoriler");

            modelBuilder.Entity<EsyaMagaza>()
                .ToTable("EsyaSatisEsyaMagazalar")
                .HasKey(esyaMagaza => new { esyaMagaza.EsyaId, esyaMagaza.MagazaId });

            modelBuilder.Entity<Kullanici>()
                .ToTable("EsyaSatisKullanicilar")
                .HasOne(kullanici => kullanici.Rol)
                .WithMany(rol => rol.Kullanicilar)
                .HasForeignKey(kullanici => kullanici.RolId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .ToTable("EsyaSatisKullaniciDetaylari")
                .HasOne(kullaniciDetayi => kullaniciDetayi.Kullanici)
                .WithOne(kullanici => kullanici.KullaniciDetayi)
                .HasForeignKey<KullaniciDetayi>(kullaniciDetayi => kullaniciDetayi.KullaniciId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kullaniciDetayi => kullaniciDetayi.Ulke)
                .WithMany(ulke => ulke.KullaniciDetaylari)
                .HasForeignKey(kullaniciDetayi => kullaniciDetayi.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<KullaniciDetayi>()
                .HasOne(kullaniciDetayi => kullaniciDetayi.Sehir)
                .WithMany(sehir => sehir.KullaniciDetaylari)
                .HasForeignKey(kullaniciDetayi => kullaniciDetayi.SehirId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Sehir>()
                .ToTable("EsyaSatisSehirler")
                .HasOne(sehir => sehir.Ulke)
                .WithMany(ulke => ulke.Sehirler)
                .HasForeignKey(sehir => sehir.UlkeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EsyaSiparis>()
                .ToTable("EsyaSatisEsyaSiparisler")
                .HasOne(esyaSiparis => esyaSiparis.Esya)
                .WithMany(esya => esya.EsyaSiparisler)
                .HasForeignKey(esyaSiparis => esyaSiparis.EsyaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EsyaSiparis>()
                .HasOne(esyaSiparis => esyaSiparis.Siparis)
                .WithMany(siparis => siparis.EsyaSiparisler)
                .HasForeignKey(esyaSiparis => esyaSiparis.SiparisId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<EsyaSiparis>()
                .HasKey(esyaSiparis => new { esyaSiparis.EsyaId, esyaSiparis.SiparisId });

            modelBuilder.Entity<Ulke>().ToTable("EsyaSatisUlkeler");

            modelBuilder.Entity<Rol>().ToTable("EsyaSatisRoller");

            modelBuilder.Entity<Siparis>().ToTable("EsyaSatisSiparisler");

            modelBuilder.Entity<Esya>().HasIndex(esya => esya.Adi);

            modelBuilder.Entity<Kullanici>().HasIndex(kullanici => kullanici.KullaniciAdi).IsUnique();

            modelBuilder.Entity<KullaniciDetayi>().HasIndex(kullaniciDetay => kullaniciDetay.Eposta).IsUnique();
        }
    }
}