using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PasswordManager.Data.EF {
    public class PasswordManagerContext : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=PasswordManager.db");
        }
    }
}
