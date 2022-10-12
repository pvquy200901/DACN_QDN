﻿// <auto-generated />
using System;
using System.Collections.Generic;
using BackEnd_Football.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BackEnd_Football.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221012062843_database-v0.5.2")]
    partial class databasev052
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BackEnd_Football.Models.Comment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<long?>("Newsid")
                        .HasColumnType("bigint");

                    b.Property<string>("comments")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("useCommentsID")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("Newsid");

                    b.HasIndex("useCommentsID");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BackEnd_Football.Models.News", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("contact")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("content")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("images")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<long?>("managerID")
                        .HasColumnType("bigint");

                    b.Property<long?>("stateID")
                        .HasColumnType("bigint");

                    b.Property<long?>("userID")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("managerID");

                    b.HasIndex("stateID");

                    b.HasIndex("userID");

                    b.ToTable("News");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlFile", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("key")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("link")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("time")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("ID");

                    b.ToTable("File");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlOrderStadium", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("isDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("isFinish")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("orderTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("price")
                        .HasColumnType("integer");

                    b.Property<long?>("stadiumOrderid")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("stateOrderID")
                        .HasColumnType("bigint");

                    b.Property<long?>("userManagerOrderID")
                        .HasColumnType("bigint");

                    b.Property<long?>("userOrderID")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("stadiumOrderid");

                    b.HasIndex("stateOrderID");

                    b.HasIndex("userManagerOrderID");

                    b.HasIndex("userOrderID");

                    b.ToTable("OrderStadium");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlRole", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("des")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("note")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlStadium", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("contact")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<List<string>>("images")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("isDelete")
                        .HasColumnType("boolean");

                    b.Property<bool>("isFinish")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("price")
                        .HasColumnType("integer");

                    b.Property<long?>("stateID")
                        .HasColumnType("bigint");

                    b.Property<long?>("userSystemID")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("stateID");

                    b.HasIndex("userSystemID");

                    b.ToTable("Stadium");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlState", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<int>("code")
                        .HasColumnType("integer");

                    b.Property<string>("des")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.ToTable("Statements");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlTeam", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("id"));

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("createdTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("des")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<List<string>>("imagesTeam")
                        .IsRequired()
                        .HasColumnType("text[]");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("logo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("quantity")
                        .HasColumnType("integer");

                    b.Property<string>("shortName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlUser", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<bool>("ChucVu")
                        .HasColumnType("boolean");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhotoURL")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("SqlTeamid")
                        .HasColumnType("bigint");

                    b.Property<string>("UID")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("SqlTeamid");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlUserSystem", b =>
                {
                    b.Property<long>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("ID"));

                    b.Property<string>("avatar")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("des")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("isdeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("phoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long?>("roleID")
                        .HasColumnType("bigint");

                    b.Property<string>("token")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("user")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ID");

                    b.HasIndex("roleID");

                    b.ToTable("UserSystem");
                });

            modelBuilder.Entity("BackEnd_Football.Models.Comment", b =>
                {
                    b.HasOne("BackEnd_Football.Models.News", "News")
                        .WithMany("comments")
                        .HasForeignKey("Newsid");

                    b.HasOne("BackEnd_Football.Models.SqlUser", "useComments")
                        .WithMany()
                        .HasForeignKey("useCommentsID");

                    b.Navigation("News");

                    b.Navigation("useComments");
                });

            modelBuilder.Entity("BackEnd_Football.Models.News", b =>
                {
                    b.HasOne("BackEnd_Football.Models.SqlUserSystem", "manager")
                        .WithMany()
                        .HasForeignKey("managerID");

                    b.HasOne("BackEnd_Football.Models.SqlState", "state")
                        .WithMany()
                        .HasForeignKey("stateID");

                    b.HasOne("BackEnd_Football.Models.SqlUser", "user")
                        .WithMany()
                        .HasForeignKey("userID");

                    b.Navigation("manager");

                    b.Navigation("state");

                    b.Navigation("user");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlOrderStadium", b =>
                {
                    b.HasOne("BackEnd_Football.Models.SqlStadium", "stadiumOrder")
                        .WithMany()
                        .HasForeignKey("stadiumOrderid");

                    b.HasOne("BackEnd_Football.Models.SqlState", "stateOrder")
                        .WithMany()
                        .HasForeignKey("stateOrderID");

                    b.HasOne("BackEnd_Football.Models.SqlUserSystem", "userManagerOrder")
                        .WithMany()
                        .HasForeignKey("userManagerOrderID");

                    b.HasOne("BackEnd_Football.Models.SqlUser", "userOrder")
                        .WithMany()
                        .HasForeignKey("userOrderID");

                    b.Navigation("stadiumOrder");

                    b.Navigation("stateOrder");

                    b.Navigation("userManagerOrder");

                    b.Navigation("userOrder");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlStadium", b =>
                {
                    b.HasOne("BackEnd_Football.Models.SqlState", "state")
                        .WithMany()
                        .HasForeignKey("stateID");

                    b.HasOne("BackEnd_Football.Models.SqlUserSystem", "userSystem")
                        .WithMany()
                        .HasForeignKey("userSystemID");

                    b.Navigation("state");

                    b.Navigation("userSystem");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlUser", b =>
                {
                    b.HasOne("BackEnd_Football.Models.SqlTeam", null)
                        .WithMany("user")
                        .HasForeignKey("SqlTeamid");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlUserSystem", b =>
                {
                    b.HasOne("BackEnd_Football.Models.SqlRole", "role")
                        .WithMany()
                        .HasForeignKey("roleID");

                    b.Navigation("role");
                });

            modelBuilder.Entity("BackEnd_Football.Models.News", b =>
                {
                    b.Navigation("comments");
                });

            modelBuilder.Entity("BackEnd_Football.Models.SqlTeam", b =>
                {
                    b.Navigation("user");
                });
#pragma warning restore 612, 618
        }
    }
}
