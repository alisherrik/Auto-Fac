﻿// <auto-generated />
using System;
using Auto_Fac;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Auto_Fac.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220918165645_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.15");

            modelBuilder.Entity("Auto_Fac.Models.Faculty.Departament", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("idFaculty")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Departaments");
                });

            modelBuilder.Entity("Auto_Fac.Models.Faculty.Faculty", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Auto_Fac.Models.Faculty.Professions.LessonSchedule", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int>("idWeekDays")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("LessonSchedule");
                });

            modelBuilder.Entity("Auto_Fac.Models.Faculty.Professions.Profession", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("IdDepartament")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("Auto_Fac.Models.Faculty.Professions.Simesters", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Simesters");
                });

            modelBuilder.Entity("Auto_Fac.Models.Faculty.Professions.WeekDays", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("az")
                        .HasColumnType("longtext");

                    b.Property<int>("idProfession")
                        .HasColumnType("int");

                    b.Property<int>("idSimesters")
                        .HasColumnType("int");

                    b.Property<string>("to")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("WeekDays");
                });

            modelBuilder.Entity("Auto_Fac.Models.Post", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("longtext");

                    b.Property<string>("photo")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.Property<string>("title")
                        .HasMaxLength(256)
                        .HasColumnType("varchar(256)");

                    b.HasKey("id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Auto_Fac.Models.admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .HasColumnType("longtext");

                    b.Property<int>("IdFaculty")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("longtext");

                    b.Property<string>("Password")
                        .HasColumnType("longtext");

                    b.Property<string>("login")
                        .HasColumnType("longtext");

                    b.Property<int>("status")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("Admins");
                });
#pragma warning restore 612, 618
        }
    }
}
