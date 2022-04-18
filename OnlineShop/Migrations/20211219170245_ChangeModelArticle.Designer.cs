﻿// <auto-generated />
using Lista10.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Lista10.Migrations
{
    [DbContext(typeof(ShopContext))]
    [Migration("20211219170245_ChangeModelArticle")]
    partial class ChangeModelArticle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Lista10.Models.Article", b =>
                {
                    b.Property<int>("ArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ArticleID");

                    b.HasIndex("CategoryID");

                    b.ToTable("Articles");

                    b.HasData(
                        new
                        {
                            ArticleID = 1,
                            CategoryID = 2,
                            Name = "Black Jeans",
                            Price = 99.989999999999995
                        },
                        new
                        {
                            ArticleID = 2,
                            CategoryID = 1,
                            Name = "Red Lipstick",
                            Price = 20.0
                        },
                        new
                        {
                            ArticleID = 3,
                            CategoryID = 3,
                            Name = "Rose",
                            Price = 12.5
                        },
                        new
                        {
                            ArticleID = 4,
                            CategoryID = 2,
                            Name = "Baseball Cap",
                            Price = 23.190000000000001
                        });
                });

            modelBuilder.Entity("Lista10.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            Name = "Make-up"
                        },
                        new
                        {
                            CategoryID = 2,
                            Name = "Clothes"
                        },
                        new
                        {
                            CategoryID = 3,
                            Name = "Flowers"
                        });
                });

            modelBuilder.Entity("Lista10.Models.Article", b =>
                {
                    b.HasOne("Lista10.Models.Category", "Category")
                        .WithMany("Articles")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Lista10.Models.Category", b =>
                {
                    b.Navigation("Articles");
                });
#pragma warning restore 612, 618
        }
    }
}
