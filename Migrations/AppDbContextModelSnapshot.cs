using System;
using CMCS.Web.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CMCS.Web.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.0");

            modelBuilder.Entity("CMCS.Web.Models.ClaimStatusHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ActionedBy")
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ChangedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("ClaimId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Comment")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("FromStatus")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ToStatus")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.ToTable("ClaimStatusHistories");
                });

            modelBuilder.Entity("CMCS.Web.Models.Claims", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("HourlyRate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("HoursWorked")
                        .HasColumnType("TEXT");

                    b.Property<int>("LecturerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Notes")
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("LecturerId");

                    b.ToTable("Claims");
                });

            modelBuilder.Entity("CMCS.Web.Models.Document", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClaimId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ContentType")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalFileName")
                        .IsRequired()
                        .HasMaxLength(260)
                        .HasColumnType("TEXT");

                    b.Property<long>("SizeBytes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StoredFileName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("UploadedAt")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("ClaimId");

                    b.ToTable("Documents");
                });

            modelBuilder.Entity("CMCS.Web.Models.Lecturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Lecturers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "demo.lecturer@example.com",
                            FullName = "Demo Lecturer"
                        });
                });

            modelBuilder.Entity("CMCS.Web.Models.ClaimStatusHistory", b =>
                {
                    b.HasOne("CMCS.Web.Models.Claims", "Claim")
                        .WithMany("StatusHistory")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");
                });

            modelBuilder.Entity("CMCS.Web.Models.Claims", b =>
                {
                    b.HasOne("CMCS.Web.Models.Lecturer", "Lecturer")
                        .WithMany("Claims")
                        .HasForeignKey("LecturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecturer");
                });

            modelBuilder.Entity("CMCS.Web.Models.Document", b =>
                {
                    b.HasOne("CMCS.Web.Models.Claims", "Claim")
                        .WithMany("Documents")
                        .HasForeignKey("ClaimId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Claim");
                });

            modelBuilder.Entity("CMCS.Web.Models.Claims", b =>
                {
                    b.Navigation("Documents");

                    b.Navigation("StatusHistory");
                });

            modelBuilder.Entity("CMCS.Web.Models.Lecturer", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
