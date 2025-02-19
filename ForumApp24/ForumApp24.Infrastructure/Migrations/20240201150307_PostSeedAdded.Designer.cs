﻿// <auto-generated />
using ForumApp24.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ForumApp24.Infrastructure.Migrations
{
    [DbContext(typeof(ForumDbContext))]
    [Migration("20240201150307_PostSeedAdded")]
    partial class PostSeedAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ForumApp24.Infrastructure.Data.Models.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasComment("Identifire");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1500)
                        .HasColumnType("nvarchar(1500)")
                        .HasComment("Post content");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasComment("Post title");

                    b.HasKey("Id");

                    b.ToTable("Posts");

                    b.HasComment("Db post model");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "First post content",
                            Title = "My first post"
                        },
                        new
                        {
                            Id = 2,
                            Content = "Second post content",
                            Title = "My second post"
                        },
                        new
                        {
                            Id = 3,
                            Content = "Third post content",
                            Title = "My third post"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
