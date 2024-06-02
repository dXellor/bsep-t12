﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using bsep_dll.Data;

#nullable disable

namespace bsep_dll.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240528144941_Add_2Fa_Fields_To_User_Identity")]
    partial class Add_2Fa_Fields_To_User_Identity
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.18")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("bsep_dll.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("user_id");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("address");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("city");

                    b.Property<string>("CompanyName")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("company_name");

                    b.Property<string>("CompanyPib")
                        .HasMaxLength(9)
                        .HasColumnType("character(9)")
                        .HasColumnName("company_pib")
                        .IsFixedLength();

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("country");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)")
                        .HasColumnName("last_name");

                    b.Property<string>("Package")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("package");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)")
                        .HasColumnName("phone");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("type");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("bsep_dll.Models.UserIdentity", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<bool>("IsAwaitingTotp")
                        .HasColumnType("boolean")
                        .HasColumnName("awaiting_totp");

                    b.Property<int>("Iterations")
                        .HasColumnType("integer")
                        .HasColumnName("iterations");

                    b.Property<int>("OutputLength")
                        .HasColumnType("integer")
                        .HasColumnName("output_length");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text")
                        .HasColumnName("refresh_token");

                    b.Property<DateTime?>("RefreshTokenExpirationDateTime")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("refresh_token_expires");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea")
                        .HasColumnName("salt");

                    b.Property<bool>("TwoFaEnabled")
                        .HasColumnType("boolean")
                        .HasColumnName("two_fa_enabled");

                    b.HasKey("Email");

                    b.ToTable("user_identities", (string)null);
                });

            modelBuilder.Entity("bsep_dll.Models.UserIdentity", b =>
                {
                    b.HasOne("bsep_dll.Models.User", "User")
                        .WithOne("UserIdentity")
                        .HasForeignKey("bsep_dll.Models.UserIdentity", "Email")
                        .HasPrincipalKey("bsep_dll.Models.User", "Email")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("bsep_dll.Models.User", b =>
                {
                    b.Navigation("UserIdentity");
                });
#pragma warning restore 612, 618
        }
    }
}
