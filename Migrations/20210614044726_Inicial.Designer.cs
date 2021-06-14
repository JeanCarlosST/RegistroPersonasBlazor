﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RegistroPersonasBlazor.DAL;

namespace RegistroPersonasBlazor.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20210614044726_Inicial")]
    partial class Inicial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("RegistroPersonasBlazor.Models.Moras", b =>
                {
                    b.Property<int>("MoraID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<float>("Total")
                        .HasColumnType("REAL");

                    b.HasKey("MoraID");

                    b.ToTable("Moras");
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.MorasDetalle", b =>
                {
                    b.Property<int>("MoraDetalleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("MoraID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PrestamoID")
                        .HasColumnType("INTEGER");

                    b.Property<float>("Valor")
                        .HasColumnType("REAL");

                    b.HasKey("MoraDetalleID");

                    b.HasIndex("MoraID");

                    b.ToTable("MorasDetalle");
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.Personas", b =>
                {
                    b.Property<int>("PersonaID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Cedula")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("PersonaID");

                    b.ToTable("Personas");
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.Prestamos", b =>
                {
                    b.Property<int>("PrestamoID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<float>("Balance")
                        .HasColumnType("REAL");

                    b.Property<string>("Concepto")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("TEXT");

                    b.Property<float>("Monto")
                        .HasColumnType("REAL");

                    b.Property<float>("Mora")
                        .HasColumnType("REAL");

                    b.Property<int>("PersonaID")
                        .HasColumnType("INTEGER");

                    b.HasKey("PrestamoID");

                    b.HasIndex("PersonaID");

                    b.ToTable("Prestamos");
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.MorasDetalle", b =>
                {
                    b.HasOne("RegistroPersonasBlazor.Models.Moras", null)
                        .WithMany("Detalle")
                        .HasForeignKey("MoraID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.Prestamos", b =>
                {
                    b.HasOne("RegistroPersonasBlazor.Models.Personas", null)
                        .WithMany("Prestamos")
                        .HasForeignKey("PersonaID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.Moras", b =>
                {
                    b.Navigation("Detalle");
                });

            modelBuilder.Entity("RegistroPersonasBlazor.Models.Personas", b =>
                {
                    b.Navigation("Prestamos");
                });
#pragma warning restore 612, 618
        }
    }
}