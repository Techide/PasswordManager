using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using PasswordManager.Models.Data.EF;

namespace PasswordManager.Migrations {
    [DbContext(typeof(PasswordManagerContext))]
    partial class PasswordManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("PasswordManager.Data.EF.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account")
                        .IsRequired();

                    b.Property<byte[]>("IV")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("Password")
                        .IsRequired();

                    b.Property<byte[]>("Salt")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("PasswordManager.Models.Data.EF.Entities.Setting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<byte[]>("Value");

                    b.HasKey("Id");

                    b.HasAlternateKey("Name");

                    b.HasIndex("Name");

                    b.ToTable("Settings");
                });
        }
    }
}
