using Microsoft.EntityFrameworkCore;
using PasswordManager.Data.EF.Entities;
using PasswordManager.Models.Data.EF.Entities;

namespace PasswordManager.Data.EF {

    public class PasswordManagerContext : DbContext {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=PasswordManager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Setting>().HasAlternateKey(x => x.Key).HasName("AlternateKey_Key");
        }
    }
}