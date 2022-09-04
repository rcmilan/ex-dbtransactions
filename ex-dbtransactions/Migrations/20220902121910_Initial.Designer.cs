﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ex_dbtransactions.Database;

#nullable disable

namespace ex_dbtransactions.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220902121910_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ex_dbtransactions.Entities.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("ex_dbtransactions.Entities.Person", b =>
                {
                    b.OwnsOne("ex_dbtransactions.Entities.ValueObjects.PersonAddress", "Address", b1 =>
                        {
                            b1.Property<Guid>("PersonId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("District")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Number")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.Property<string>("Street")
                                .IsRequired()
                                .HasColumnType("longtext");

                            b1.HasKey("PersonId");

                            b1.ToTable("People");

                            b1.WithOwner()
                                .HasForeignKey("PersonId");
                        });

                    b.Navigation("Address")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
