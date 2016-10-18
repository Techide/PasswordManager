using Microsoft.EntityFrameworkCore;
using PasswordManager.Data.EF.Entities;

namespace PasswordManager.Data.EF {
    public class PasswordManagerContext : DbContext {

        public DbSet<Profile> Profiles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=PasswordManager.db");
        }
    }
}
