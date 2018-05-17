﻿using MakeYourJournal.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL
{
    public class JournalDbContext : DbContext
    {
        public JournalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().ToTable("Issue");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Todo>().ToTable("Todo");

            modelBuilder.Entity<Issue>()
                .HasAlternateKey(i => new { i.Volume, i.Number });

            modelBuilder.Entity<Issue>()
                .HasAlternateKey(i => i.AllTime);

            modelBuilder.Entity<Item>()
                .HasDiscriminator<string>("ItemType");
        }
    }
}