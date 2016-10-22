using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;



namespace TestExams.DBModel
{
    public class TestExamsContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<AppMail> AppMails { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=TextExamsBD.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasKey(x => x.SubjectID);
            modelBuilder.Entity<Subject>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<Theme>().HasKey(x => x.ThemeID);
            modelBuilder.Entity<Theme>().Property(x => x.Name).IsRequired();
            //modelBuilder.Entity<Theme>().HasOne(x => x.Subjet);

            modelBuilder.Entity<Question>().HasKey(x => x.QuestionID);
            modelBuilder.Entity<Question>().Property(x => x.QuestionText).IsRequired();
           // modelBuilder.Entity<Question>().Property(x => x.Theme).IsRequired();


            modelBuilder.Entity<Answer>().HasKey(x => x.AnswerId);
            modelBuilder.Entity<Answer>().Property(x => x.AnswerText).IsRequired();
           // modelBuilder.Entity<Answer>().Property(x => x.Question).IsRequired();
            modelBuilder.Entity<Answer>().Property(x => x.isCorrect).IsRequired();

            modelBuilder.Entity<Error>().HasKey(x => x.ErrorID);
            modelBuilder.Entity<Error>().HasOne(x => x.Question);
            modelBuilder.Entity<Error>().HasOne(x => x.Exam);

            modelBuilder.Entity<User>().HasKey(x => x.UserID);
            modelBuilder.Entity<User>().Property(x => x.UserName).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.email).IsRequired();

            modelBuilder.Entity<AppMail>().HasKey(x => x.AppMailID);
            modelBuilder.Entity<AppMail>().Property(x => x.Host).IsRequired();
            modelBuilder.Entity<AppMail>().Property(x => x.MailAddress).IsRequired();
            modelBuilder.Entity<AppMail>().Property(x => x.Password).IsRequired();
            modelBuilder.Entity<AppMail>().Property(x => x.port).IsRequired();

            modelBuilder.Entity<Exam>().HasKey(x => x.ExamID);
            modelBuilder.Entity<Exam>().HasOne(x => x.User);

        }
    }
}
