﻿// <auto-generated />
using System;
using FinFolio.PortFolioRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FinFolio.PortFolioRepository.Migrations
{
    [DbContext(typeof(PortFolioDBContext))]
    [Migration("20220717183226_SchemeMasterTableWithRelations")]
    partial class SchemeMasterTableWithRelations
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.PortFolio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(225)
                        .IsUnicode(true)
                        .HasColumnType("varchar(225)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PortFolios");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.PortFolioItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<decimal>("CostValue")
                        .HasPrecision(5, 2)
                        .HasColumnType("decimal(5,2)");

                    b.Property<bool>("IsSIP")
                        .HasColumnType("bit");

                    b.Property<int>("NoOfUnits")
                        .HasColumnType("int");

                    b.Property<int?>("PortFolioId")
                        .HasColumnType("int");

                    b.Property<int>("PortFolioItemType")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime");

                    b.Property<int>("SchemeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PortFolioId");

                    b.HasIndex("SchemeId");

                    b.ToTable("PortFolioItems");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.Scheme", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("Code")
                        .HasColumnType("int");

                    b.Property<bool?>("IsActive")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("LaunchDate")
                        .HasColumnType("datetime");

                    b.Property<string>("NAVName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.ToTable("Schemes");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.Wishlist", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("SchemeId")
                        .HasColumnType("int");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SchemeId");

                    b.ToTable("Wishlist");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.PortFolioItem", b =>
                {
                    b.HasOne("FinFolio.PortFolioRepository.Entities.PortFolio", null)
                        .WithMany("Items")
                        .HasForeignKey("PortFolioId");

                    b.HasOne("FinFolio.PortFolioRepository.Entities.Scheme", "Scheme")
                        .WithMany("PortFolioItems")
                        .HasForeignKey("SchemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .HasConstraintName("FK_Scheme_PortFolioItem");

                    b.Navigation("Scheme");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.Wishlist", b =>
                {
                    b.HasOne("FinFolio.PortFolioRepository.Entities.Scheme", "Scheme")
                        .WithMany("Wishlist")
                        .HasForeignKey("SchemeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Scheme_Wishlist");

                    b.Navigation("Scheme");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.PortFolio", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("FinFolio.PortFolioRepository.Entities.Scheme", b =>
                {
                    b.Navigation("PortFolioItems");

                    b.Navigation("Wishlist");
                });
#pragma warning restore 612, 618
        }
    }
}
