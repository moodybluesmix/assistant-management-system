using System;
using Microsoft.EntityFrameworkCore;
using AsistanNobetYonetimi.Models;

namespace AsistanNobetYonetimi.Contexts
{
    public class NobetDbContext : DbContext
    {
        public NobetDbContext(DbContextOptions<NobetDbContext> options) : base(options)
        {
        }

        public DbSet<Asistan> asistanlar { get; set; }
        public DbSet<AcilDurum> acildurumlar { get; set; }
        public DbSet<Admin> admin { get; set; }
        public DbSet<Bolum> bolumler { get; set; }
        public DbSet<Nobet> nobetler { get; set; }
        public DbSet<OgretimUyesi> ogretimuyeleri { get; set; }
        public DbSet<Randevu> randevular { get; set; }
        public DbSet<Musaitlik> musaitlikler { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureNobetModel(modelBuilder);
            ConfigureRandevuModel(modelBuilder);
            ConfigureMusaitlikModel(modelBuilder);
            ConfigureBolumModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureNobetModel(ModelBuilder modelBuilder)
        {
            // Nobet - Asistan İlişkisi
            modelBuilder.Entity<Nobet>()
                .HasOne(n => n.asistan)
                .WithMany(a => a.nobet)
                .HasForeignKey(n => n.AsistanID)
                .OnDelete(DeleteBehavior.Cascade);

            // Nobet - Bolum İlişkisi
            modelBuilder.Entity<Nobet>()
                .HasOne(n => n.bolum)
                .WithMany(b => b.nobet)
                .HasForeignKey(n => n.BolumID)
                .OnDelete(DeleteBehavior.Restrict);
        }

        private void ConfigureRandevuModel(ModelBuilder modelBuilder)
        {
            // Randevu - Asistan İlişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.asistan)
                .WithMany(a => a.randevu)
                .HasForeignKey(r => r.AsistanID)
                .OnDelete(DeleteBehavior.Cascade);

            // Randevu - Musaitlik İlişkisi
            modelBuilder.Entity<Randevu>()
                .HasOne(r => r.musaitlik)
                .WithMany(m => m.Randevular)
                .HasForeignKey(r => r.MusaitlikID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        private void ConfigureMusaitlikModel(ModelBuilder modelBuilder)
        {
            // Musaitlik - OgretimUyesi İlişkisi
            modelBuilder.Entity<Musaitlik>()
                .HasOne(m => m.OgretimUyesi)
                .WithMany(o => o.musaitlikler)
                .HasForeignKey(m => m.OgretimUyesiID)
                .OnDelete(DeleteBehavior.Cascade);
        }

        private void ConfigureBolumModel(ModelBuilder modelBuilder)
        {
            // Bolum - Asistan İlişkisi
            modelBuilder.Entity<Bolum>()
                .HasMany(b => b.asistan)
                .WithOne(a => a.bolum)
                .HasForeignKey(a => a.BolumID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
