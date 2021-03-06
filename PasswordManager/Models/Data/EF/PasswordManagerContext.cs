﻿using Microsoft.EntityFrameworkCore;

namespace PasswordManager.Models.Data.EF {

    public class PasswordManagerContext : DbContext {
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Setting> Settings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=PasswordManager.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Setting>().HasAlternateKey(x => x.Name);
            modelBuilder.Entity<Setting>().HasIndex(x => x.Name);
        }
    }
}