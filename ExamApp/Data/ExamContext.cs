using ExamApp.Entities;
using Microsoft.EntityFrameworkCore;
using System;


namespace ExamApp.Data
{
    public class ExamContext: DbContext
    {
        public DbSet<Exam> Exam { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Content> Content { get; set; }

        public string DbPath { get; private set; }
        private static bool _created = false;

        public ExamContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = $"{path}{System.IO.Path.DirectorySeparatorChar}exam.db";
            if (!_created)
            {
                _created = true;
                Database.EnsureDeleted();
                Database.EnsureCreated();
            }

        }

      
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>().HasKey(p => new { p.id, p.ExamId });
        }
            

    }
}
