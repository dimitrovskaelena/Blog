﻿// <auto-generated />
using System;
using Blog.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blog.Domain.Migrations
{
    [DbContext(typeof(BlogDataContext))]
    [Migration("20231114145755_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blog.Domain.Models.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DeletedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BlogPosts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedOn = new DateTime(2023, 11, 14, 15, 57, 55, 401, DateTimeKind.Local).AddTicks(9661),
                            Text = "Text 1",
                            Title = "Title 1"
                        },
                        new
                        {
                            Id = 2,
                            CreatedOn = new DateTime(2023, 11, 14, 15, 57, 55, 401, DateTimeKind.Local).AddTicks(9700),
                            Text = "Text 2",
                            Title = "Title 2"
                        },
                        new
                        {
                            Id = 3,
                            CreatedOn = new DateTime(2023, 11, 14, 15, 57, 55, 401, DateTimeKind.Local).AddTicks(9704),
                            Text = "Text 3",
                            Title = "Title 3"
                        });
                });

            modelBuilder.Entity("Blog.Domain.Models.BlogPost", b =>
                {
                    b.HasOne("Blog.Domain.Models.BlogPost", null)
                        .WithMany("RelatedBlogs")
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blog.Domain.Models.BlogPost", b =>
                {
                    b.Navigation("RelatedBlogs");
                });
#pragma warning restore 612, 618
        }
    }
}