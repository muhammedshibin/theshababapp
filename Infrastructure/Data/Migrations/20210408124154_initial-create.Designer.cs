﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ShababContext))]
    [Migration("20210408124154_initial-create")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("Core.Entities.BillDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("InmateBillId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ItemCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ItemCategoryName")
                        .HasColumnType("TEXT");

                    b.Property<string>("ItemName")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InmateBillId");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<double>("DefaultRate")
                        .HasColumnType("REAL");

                    b.Property<bool>("IsApplicableForVisitors")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NeedToConsiderDays")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Entities.Inmate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsInmateOnTopBed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsVisit")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Inmates");
                });

            modelBuilder.Entity("Core.Entities.InmateBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("BillAmount")
                        .HasColumnType("double(10,2)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("InmateId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("Month")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("InmateId");

                    b.ToTable("InmateBills");
                });

            modelBuilder.Entity("Core.Entities.Leave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("InmateId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LeaveReason")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InmateId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Core.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsExpense")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("PaidPartyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PaidPartyId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Core.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("AmountInHand")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("TEXT");

                    b.Property<double>("DueAmount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Core.Entities.BillDetail", b =>
                {
                    b.HasOne("Core.Entities.InmateBill", "inmateBill")
                        .WithMany("BillItems")
                        .HasForeignKey("InmateBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("inmateBill");
                });

            modelBuilder.Entity("Core.Entities.InmateBill", b =>
                {
                    b.HasOne("Core.Entities.Inmate", "Inmate")
                        .WithMany()
                        .HasForeignKey("InmateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inmate");
                });

            modelBuilder.Entity("Core.Entities.Leave", b =>
                {
                    b.HasOne("Core.Entities.Inmate", "Inmate")
                        .WithMany("InmateLeaves")
                        .HasForeignKey("InmateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inmate");
                });

            modelBuilder.Entity("Core.Entities.Transaction", b =>
                {
                    b.HasOne("Core.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Vendor", "PaidParty")
                        .WithMany("Transactions")
                        .HasForeignKey("PaidPartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("PaidParty");
                });

            modelBuilder.Entity("Core.Entities.Inmate", b =>
                {
                    b.Navigation("InmateLeaves");
                });

            modelBuilder.Entity("Core.Entities.InmateBill", b =>
                {
                    b.Navigation("BillItems");
                });

            modelBuilder.Entity("Core.Entities.Vendor", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
