using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ASS7420_1.Data;

namespace ASS7420_1.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20170928034907_Images")]
    partial class Images
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASS7420_1.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 10);

                    b.HasKey("CategoryID");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ASS7420_1.Models.Customer", b =>
                {
                    b.Property<int>("CustomerID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Home");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Work");

                    b.HasKey("CustomerID");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("ASS7420_1.Models.Hat", b =>
                {
                    b.Property<int>("HatID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CategoryID");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("HatName")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Image");

                    b.Property<int?>("OrderitemID");

                    b.Property<double>("Price");

                    b.Property<int>("SupplierID");

                    b.HasKey("HatID");

                    b.HasIndex("CategoryID");

                    b.HasIndex("OrderitemID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Hat");
                });

            modelBuilder.Entity("ASS7420_1.Models.Order", b =>
                {
                    b.Property<int>("OrderID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CustomerID");

                    b.Property<double>("GST");

                    b.Property<double>("Grandtotal");

                    b.Property<int?>("Status");

                    b.Property<double>("Subtotal");

                    b.HasKey("OrderID");

                    b.HasIndex("CustomerID");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("ASS7420_1.Models.Orderitem", b =>
                {
                    b.Property<int>("OrderitemID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("HatID");

                    b.Property<int>("OrderID");

                    b.Property<int>("Quantity");

                    b.HasKey("OrderitemID");

                    b.HasIndex("OrderID");

                    b.ToTable("Orderitem");
                });

            modelBuilder.Entity("ASS7420_1.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Email");

                    b.Property<string>("Home");

                    b.Property<string>("Mobile");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 10);

                    b.Property<string>("Work");

                    b.HasKey("SupplierID");

                    b.ToTable("Supplier");
                });

            modelBuilder.Entity("ASS7420_1.Models.Hat", b =>
                {
                    b.HasOne("ASS7420_1.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASS7420_1.Models.Orderitem")
                        .WithMany("Hat")
                        .HasForeignKey("OrderitemID");

                    b.HasOne("ASS7420_1.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASS7420_1.Models.Order", b =>
                {
                    b.HasOne("ASS7420_1.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ASS7420_1.Models.Orderitem", b =>
                {
                    b.HasOne("ASS7420_1.Models.Order", "Order")
                        .WithMany("Orderitems")
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
