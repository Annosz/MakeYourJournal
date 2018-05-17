﻿// <auto-generated />
using MakeYourJournal.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace MakeYourJournal.DAL.Migrations
{
    [DbContext(typeof(JournalDbContext))]
    partial class JournalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IssueId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Topic")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("IssueId");

                    b.ToTable("Article");
                });

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AllTime");

                    b.Property<DateTime>("Deadline");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .HasMaxLength(50);

                    b.Property<int>("Number");

                    b.Property<int>("Volume");

                    b.HasKey("Id");

                    b.HasAlternateKey("AllTime");


                    b.HasAlternateKey("Volume", "Number");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleId");

                    b.Property<string>("ItemType")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.ToTable("Item");

                    b.HasDiscriminator<string>("ItemType").HasValue("Item");
                });

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Note", b =>
                {
                    b.HasBaseType("MakeYourJournal.DAL.Entities.Item");

                    b.Property<string>("Description")
                        .HasMaxLength(1000);

                    b.ToTable("Note");

                    b.HasDiscriminator().HasValue("Note");
                });

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Todo", b =>
                {
                    b.HasBaseType("MakeYourJournal.DAL.Entities.Item");

                    b.Property<bool>("Done");

                    b.ToTable("Todo");

                    b.HasDiscriminator().HasValue("Todo");
                });

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Article", b =>
                {
                    b.HasOne("MakeYourJournal.DAL.Entities.Issue", "Issue")
                        .WithMany("Articles")
                        .HasForeignKey("IssueId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MakeYourJournal.DAL.Entities.Item", b =>
                {
                    b.HasOne("MakeYourJournal.DAL.Entities.Article", "Article")
                        .WithMany("Items")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
