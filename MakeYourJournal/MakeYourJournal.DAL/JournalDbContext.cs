using MakeYourJournal.DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace MakeYourJournal.DAL
{
    public class JournalDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        public JournalDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueDetails> IssueDetails { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Issue>().ToTable("Issue");
            modelBuilder.Entity<IssueDetails>().ToTable("Issue");
            modelBuilder.Entity<Article>().ToTable("Article");
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Note>().ToTable("Note");
            modelBuilder.Entity<Todo>().ToTable("Todo");

            //Alternate key
            modelBuilder.Entity<Issue>()
                .HasAlternateKey(d => new { d.AllTime });
            modelBuilder.Entity<Article>()
                .HasOne(a => a.Issue)
                .WithMany(i => i.Articles)
                .HasForeignKey(a => a.IssueAllTime)
                .HasPrincipalKey(i => i.AllTime);
            /*modelBuilder.Entity<IssueDetails>()
                .HasAlternateKey(i => new { i.Volume, i.Number });*/

            //Table splitting
            modelBuilder.Entity<Issue>()
                .HasKey(i => i.Id);
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.IssueDetails).WithOne(d => d.Issue).HasForeignKey<IssueDetails>(i => i.Id);
            modelBuilder.Entity<IssueDetails>()
                .HasKey(d => d.Id);
            modelBuilder.Entity<IssueDetails>()
                .HasOne(d => d.Issue).WithOne(i => i.IssueDetails).HasForeignKey<Issue>(d => d.Id);
            
            //TPH with named discriminator
            modelBuilder.Entity<Item>()
                .HasDiscriminator<string>("ItemType");
        }

    }
}
