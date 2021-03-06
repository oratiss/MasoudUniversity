﻿// <auto-generated />
using MasoudUniversity.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;

namespace MasoudUniversity.Migrations
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:Sequence:shared.Student", "'Student', 'shared', '1', '1', '', '', 'Int32', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MasoudUniversity.Models.Class", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Location")
                        .IsRequired();

                    b.Property<string>("TeacherName")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Class");
                });

            modelBuilder.Entity("MasoudUniversity.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClassID");

                    b.Property<int>("StudentID");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("ClassID");

                    b.HasIndex("StudentID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("MasoudUniversity.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Age");

                    b.Property<double>("GPA");

                    b.Property<string>("StudentFullName");

                    b.HasKey("Id");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("MasoudUniversity.Models.Enrollment", b =>
                {
                    b.HasOne("MasoudUniversity.Models.Class", "Class")
                        .WithMany("Enrollments")
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MasoudUniversity.Models.Student", "Student")
                        .WithMany("Enrollments")
                        .HasForeignKey("StudentID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
