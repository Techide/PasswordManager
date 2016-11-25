using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PasswordManager.Data.EF;

namespace PasswordManager.Migrations
{
    [DbContext(typeof(PasswordManagerContext))]
    [Migration("20161123171441_Current")]
    partial class Current
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("PasswordManager.Data.EF.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Account");

                    b.Property<byte[]>("IV");

                    b.Property<string>("Name");

                    b.Property<byte[]>("Password");

                    b.Property<byte[]>("Salt");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });
        }
    }
}
