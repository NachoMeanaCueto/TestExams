using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TestExams.DBModel
{
    public class TestExamsContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<AppMail> AppMails { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ExamQuestions> ExamQuestions { get; set; }
        public DbSet<ExamType> ExamTypes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //var connection = new SqliteConnection("data source =TextExamsBD.db, EnforceFKConstraints=Yes|True|1");
            optionsBuilder.UseSqlite("Filename=TextExamsBD.db");
            //optionsBuilder.UseSqlite(connection);


        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Subject>().HasKey(x => x.SubjectID);
            modelBuilder.Entity<Subject>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Subject>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Theme>().HasKey(x => x.ThemeID);
            modelBuilder.Entity<Theme>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<Theme>().HasOne(x => x.Subjet).WithMany().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>().HasKey(x => x.QuestionID);
            modelBuilder.Entity<Question>().Property(x => x.QuestionText).IsRequired();
            modelBuilder.Entity<Question>().HasOne(x => x.Theme).WithMany().OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Answer>().HasKey(x => x.AnswerId);
            modelBuilder.Entity<Answer>().Property(x => x.AnswerText).IsRequired();
            modelBuilder.Entity<Answer>().HasOne(x => x.Question).WithMany().OnDelete(DeleteBehavior.Cascade); ;
            modelBuilder.Entity<Answer>().Property(x => x.isCorrect).IsRequired();

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
            modelBuilder.Entity<Exam>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Exam>().HasOne(x => x.ExamType).WithMany().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamQuestions>().HasKey(x => x.ExamQuestionID);
            modelBuilder.Entity<ExamQuestions>().HasOne(x => x.Exam).WithMany().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamType>().HasKey(x => x.ExamTypeID);
            modelBuilder.Entity<ExamType>().Property(x => x.Code).IsRequired();
            modelBuilder.Entity<ExamType>().Property(x => x.Name).IsRequired();
            modelBuilder.Entity<ExamType>().HasOne(x => x.User).WithMany().OnDelete(DeleteBehavior.Cascade);
            
        }

        
    }
}
