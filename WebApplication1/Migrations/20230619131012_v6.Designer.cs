﻿// <auto-generated />
using System;
using HRSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HRSystem.Migrations
{
    [DbContext(typeof(HRContext))]
    [Migration("20230619131012_v6")]
    partial class v6
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HRSystem.Models.Attendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("DeptID")
                        .HasColumnType("int");

                    b.Property<int>("EmpID")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("TimeAttendance")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("TimeLeave")
                        .HasColumnType("time");

                    b.HasKey("ID");

                    b.HasIndex("DeptID");

                    b.HasIndex("EmpID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("HRSystem.Models.Department", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("HRSystem.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("DeptID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phone")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DeptID");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("HRSystem.Models.Attendance", b =>
                {
                    b.HasOne("HRSystem.Models.Department", "department")
                        .WithMany("attendances")
                        .HasForeignKey("DeptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HRSystem.Models.Employee", "employee")
                        .WithMany("attendances")
                        .HasForeignKey("EmpID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("department");

                    b.Navigation("employee");
                });

            modelBuilder.Entity("HRSystem.Models.Employee", b =>
                {
                    b.HasOne("HRSystem.Models.Department", "department")
                        .WithMany("employees")
                        .HasForeignKey("DeptID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("department");
                });

            modelBuilder.Entity("HRSystem.Models.Department", b =>
                {
                    b.Navigation("attendances");

                    b.Navigation("employees");
                });

            modelBuilder.Entity("HRSystem.Models.Employee", b =>
                {
                    b.Navigation("attendances");
                });
#pragma warning restore 612, 618
        }
    }
}
