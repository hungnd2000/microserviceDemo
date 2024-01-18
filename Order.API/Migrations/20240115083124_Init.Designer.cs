﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using Order.API.Database;

#nullable disable

namespace Order.API.Migrations
{
    [DbContext(typeof(OrderDbContext))]
    [Migration("20240115083124_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Order.API.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("CustomerId");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("IdentityId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("IdentityId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CustomerName");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CustomerPhoneNumber");

                    b.HasKey("Id")
                        .HasName("Tbl_CustomerId_PK");

                    b.ToTable("Customer", (string)null);
                });

            modelBuilder.Entity("Order.API.Entities.OrderItem", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("OrderItemId");

                    b.Property<int>("OrderId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("OrderModelId")
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<int>("ProductId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ProductId");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("ProductName");

                    b.Property<int>("Quantity")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("Quantity");

                    b.HasKey("Id");

                    b.HasIndex("OrderModelId");

                    b.ToTable("OrderItem", (string)null);
                });

            modelBuilder.Entity("Order.API.Entities.OrderModel", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("OrderId");

                    b.Property<string>("AdditionalAddress")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("AdditionalAddress");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("City");

                    b.Property<int>("CustomerId")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("CustomerId");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("District");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("OrderDate");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("Street");

                    b.HasKey("Id")
                        .HasName("Tbl_OrderId_PK");

                    b.HasIndex("CustomerId");

                    b.ToTable("Order", (string)null);
                });

            modelBuilder.Entity("Order.API.Entities.OrderItem", b =>
                {
                    b.HasOne("Order.API.Entities.OrderModel", null)
                        .WithMany("Items")
                        .HasForeignKey("OrderModelId");
                });

            modelBuilder.Entity("Order.API.Entities.OrderModel", b =>
                {
                    b.HasOne("Order.API.Entities.Customer", null)
                        .WithMany()
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("Order.API.Entities.OrderModel", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}