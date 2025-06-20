﻿// <auto-generated />
using System;
using BibliotekaSzkolnaBlazor.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BibliotekaSzkolnaBlazor.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250609082121_BookAddedSubjectAndClass")]
    partial class BookAddedSubjectAndClass
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Class")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookAuthorId")
                        .HasColumnType("int");

                    b.Property<int>("BookCategoryId")
                        .HasColumnType("int");

                    b.Property<int>("BookPublisherId")
                        .HasColumnType("int");

                    b.Property<int>("BookSeriesId")
                        .HasColumnType("int");

                    b.Property<int>("BookTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Class")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsVisible")
                        .HasColumnType("bit");

                    b.Property<string>("Isbn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PageCount")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BookAuthorId");

                    b.HasIndex("BookCategoryId");

                    b.HasIndex("BookPublisherId");

                    b.HasIndex("BookSeriesId");

                    b.HasIndex("BookTypeId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookAuthor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookAuthors");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookBookGenre", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BookGenreId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "BookGenreId");

                    b.HasIndex("BookGenreId");

                    b.ToTable("BooksBookGenres");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookBookSpecialTag", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BookSpecialTagId")
                        .HasColumnType("int");

                    b.HasKey("BookId", "BookSpecialTagId");

                    b.HasIndex("BookSpecialTagId");

                    b.ToTable("BooksBookSpecialTags");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookCategories");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookCopy", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("InventoryNum")
                        .HasColumnType("int");

                    b.Property<string>("Signature")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookCopies");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookGenre", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookGenres");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookLoan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookCopyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("BorrowDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("PenaltyAmount")
                        .HasPrecision(4, 2)
                        .HasColumnType("decimal(4,2)");

                    b.Property<DateTime?>("ReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookCopyId");

                    b.HasIndex("UserId");

                    b.ToTable("BookLoans");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookPublisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookPublishers");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookReservationCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("BookCopyId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("BookCopyId");

                    b.HasIndex("UserId");

                    b.ToTable("BookReservationCarts");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookSeries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookSeries");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookSpecialTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookSpecialTags");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BookTypes");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.FavoriteBook", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("AddedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "BookId");

                    b.HasIndex("BookId");

                    b.ToTable("FavoriteBooks");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.Book", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookAuthor", "BookAuthor")
                        .WithMany("Books")
                        .HasForeignKey("BookAuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookCategory", "BookCategory")
                        .WithMany("Books")
                        .HasForeignKey("BookCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookPublisher", "BookPublisher")
                        .WithMany("Books")
                        .HasForeignKey("BookPublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookSeries", "BookSeries")
                        .WithMany("Books")
                        .HasForeignKey("BookSeriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookType", "BookType")
                        .WithMany("Books")
                        .HasForeignKey("BookTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookAuthor");

                    b.Navigation("BookCategory");

                    b.Navigation("BookPublisher");

                    b.Navigation("BookSeries");

                    b.Navigation("BookType");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookBookGenre", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookGenre", "BookGenre")
                        .WithMany("BookBookGenres")
                        .HasForeignKey("BookGenreId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.Book", "Book")
                        .WithMany("BookBookGenres")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("BookGenre");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookBookSpecialTag", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.Book", "Book")
                        .WithMany("BookBookSpecialTags")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookSpecialTag", "BookSpecialTag")
                        .WithMany("BookBookSpecialTags")
                        .HasForeignKey("BookSpecialTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("BookSpecialTag");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookCopy", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.Book", "Book")
                        .WithMany("BookCopies")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookLoan", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookCopy", "BookCopy")
                        .WithMany("BookLoans")
                        .HasForeignKey("BookCopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", "User")
                        .WithMany("BookLoans")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookCopy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookReservationCart", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.BookCopy", "BookCopy")
                        .WithMany("ReservationCart")
                        .HasForeignKey("BookCopyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", "User")
                        .WithMany("ReservationCart")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BookCopy");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.FavoriteBook", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.Models.Book", "Book")
                        .WithMany("FavoriteByUsers")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", "User")
                        .WithMany("FavoriteBooks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BibliotekaSzkolnaBlazor.Data.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.ApplicationUser", b =>
                {
                    b.Navigation("BookLoans");

                    b.Navigation("FavoriteBooks");

                    b.Navigation("ReservationCart");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.Book", b =>
                {
                    b.Navigation("BookBookGenres");

                    b.Navigation("BookBookSpecialTags");

                    b.Navigation("BookCopies");

                    b.Navigation("FavoriteByUsers");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookAuthor", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookCategory", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookCopy", b =>
                {
                    b.Navigation("BookLoans");

                    b.Navigation("ReservationCart");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookGenre", b =>
                {
                    b.Navigation("BookBookGenres");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookPublisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookSeries", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookSpecialTag", b =>
                {
                    b.Navigation("BookBookSpecialTags");
                });

            modelBuilder.Entity("BibliotekaSzkolnaBlazor.Data.Models.BookType", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
