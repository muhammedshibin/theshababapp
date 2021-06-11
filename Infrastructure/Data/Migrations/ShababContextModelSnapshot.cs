﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Infrastructure.Data.Migrations
{
    [DbContext(typeof(ShababContext))]
    partial class ShababContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("Core.Entities.BillDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InmateBillId")
                        .HasColumnType("integer");

                    b.Property<int>("ItemCategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("ItemCategoryName")
                        .HasColumnType("text");

                    b.Property<string>("ItemName")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("InmateBillId");

                    b.ToTable("BillDetails");
                });

            modelBuilder.Entity("Core.Entities.BillPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int?>("BillId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InmateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("PaidOn")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("BillId")
                        .IsUnique();

                    b.HasIndex("InmateId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("ConsiderDefaultRate")
                        .HasColumnType("boolean");

                    b.Property<bool>("CoreCategory")
                        .HasColumnType("boolean");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<decimal>("DefaultRate")
                        .HasColumnType("numeric");

                    b.Property<bool>("IsApplicableForVisitors")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<bool>("NeedToConsiderDays")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Core.Entities.Inmate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("AmountDue")
                        .HasColumnType("numeric");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("text");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsInmateOnTopBed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsVisit")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PictureUrl")
                        .HasColumnType("text");

                    b.Property<decimal>("Savings")
                        .HasColumnType("numeric");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Inmates");
                });

            modelBuilder.Entity("Core.Entities.InmateBill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("BillAmount")
                        .HasColumnType("numeric(18,2)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InmateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Month")
                        .HasColumnType("integer");

                    b.Property<int?>("PaymentId")
                        .HasColumnType("integer");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("integer");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InmateId");

                    b.ToTable("InmateBills");
                });

            modelBuilder.Entity("Core.Entities.Leave", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("FromDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("InmateId")
                        .HasColumnType("integer");

                    b.Property<string>("LeaveReason")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime>("ToDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("InmateId");

                    b.ToTable("Leaves");
                });

            modelBuilder.Entity("Core.Entities.TransactionDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsAutoGenerated")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsExpense")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("PaidPartyId")
                        .HasColumnType("integer");

                    b.Property<int>("PaidToId")
                        .HasColumnType("integer");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("PaidPartyId");

                    b.HasIndex("PaidToId");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("Core.Entities.Vendor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<decimal>("AmountInHand")
                        .HasColumnType("numeric");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<double>("DueAmount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ModfiedOn")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Vendors");
                });

            modelBuilder.Entity("Core.Entities.BillDetail", b =>
                {
                    b.HasOne("Core.Entities.InmateBill", "InmateBill")
                        .WithMany("BillItems")
                        .HasForeignKey("InmateBillId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InmateBill");
                });

            modelBuilder.Entity("Core.Entities.BillPayment", b =>
                {
                    b.HasOne("Core.Entities.InmateBill", "Bill")
                        .WithOne("BillPayment")
                        .HasForeignKey("Core.Entities.BillPayment", "BillId");

                    b.HasOne("Core.Entities.Inmate", "Inmate")
                        .WithMany()
                        .HasForeignKey("InmateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bill");

                    b.Navigation("Inmate");
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

            modelBuilder.Entity("Core.Entities.TransactionDetail", b =>
                {
                    b.HasOne("Core.Entities.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Vendor", "PaidParty")
                        .WithMany("OutgoingTransactions")
                        .HasForeignKey("PaidPartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Entities.Vendor", "PaidTo")
                        .WithMany("IncomingTransactions")
                        .HasForeignKey("PaidToId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("PaidParty");

                    b.Navigation("PaidTo");
                });

            modelBuilder.Entity("Core.Entities.Inmate", b =>
                {
                    b.Navigation("InmateLeaves");
                });

            modelBuilder.Entity("Core.Entities.InmateBill", b =>
                {
                    b.Navigation("BillItems");

                    b.Navigation("BillPayment");
                });

            modelBuilder.Entity("Core.Entities.Vendor", b =>
                {
                    b.Navigation("IncomingTransactions");

                    b.Navigation("OutgoingTransactions");
                });
#pragma warning restore 612, 618
        }
    }
}
