﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using TestExams.DBModel;

namespace TestExams.Migrations
{
    [DbContext(typeof(TestExamsContext))]
    partial class TestExamsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("TestExams.DBModel.Answer", b =>
                {
                    b.Property<int>("AnswerId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerText")
                        .IsRequired();

                    b.Property<int?>("QuestionID");

                    b.Property<bool>("isCorrect");

                    b.HasKey("AnswerId");

                    b.HasIndex("QuestionID");

                    b.ToTable("Answers");
                });

            modelBuilder.Entity("TestExams.DBModel.AppMail", b =>
                {
                    b.Property<int>("AppMailID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Host")
                        .IsRequired();

                    b.Property<string>("MailAddress")
                        .IsRequired();

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<int>("port");

                    b.HasKey("AppMailID");

                    b.ToTable("AppMails");
                });

            modelBuilder.Entity("TestExams.DBModel.Exam", b =>
                {
                    b.Property<int>("ExamID")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ExamTypeID");

                    b.Property<int?>("UserID");

                    b.HasKey("ExamID");

                    b.HasIndex("ExamTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("Exams");
                });

            modelBuilder.Entity("TestExams.DBModel.ExamQuestions", b =>
                {
                    b.Property<int>("ExamQuestionID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Error");

                    b.Property<int?>("ExamID");

                    b.Property<int?>("QuestionID");

                    b.HasKey("ExamQuestionID");

                    b.HasIndex("ExamID");

                    b.HasIndex("QuestionID");

                    b.ToTable("ExamQuestions");
                });

            modelBuilder.Entity("TestExams.DBModel.ExamType", b =>
                {
                    b.Property<int>("ExamTypeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("UserID");

                    b.HasKey("ExamTypeID");

                    b.HasIndex("UserID");

                    b.ToTable("ExamTypes");
                });

            modelBuilder.Entity("TestExams.DBModel.Question", b =>
                {
                    b.Property<int>("QuestionID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("QuestionText")
                        .IsRequired();

                    b.Property<int?>("ThemeID");

                    b.HasKey("QuestionID");

                    b.HasIndex("ThemeID");

                    b.ToTable("Questions");
                });

            modelBuilder.Entity("TestExams.DBModel.Subject", b =>
                {
                    b.Property<int>("SubjectID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("UserID");

                    b.HasKey("SubjectID");

                    b.HasIndex("UserID");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("TestExams.DBModel.Theme", b =>
                {
                    b.Property<int>("ThemeID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int?>("SubjetSubjectID");

                    b.HasKey("ThemeID");

                    b.HasIndex("SubjetSubjectID");

                    b.ToTable("Themes");
                });

            modelBuilder.Entity("TestExams.DBModel.User", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Language");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("UserName")
                        .IsRequired();

                    b.Property<string>("email")
                        .IsRequired();

                    b.HasKey("UserID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TestExams.DBModel.Answer", b =>
                {
                    b.HasOne("TestExams.DBModel.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestExams.DBModel.Exam", b =>
                {
                    b.HasOne("TestExams.DBModel.ExamType", "ExamType")
                        .WithMany()
                        .HasForeignKey("ExamTypeID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TestExams.DBModel.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestExams.DBModel.ExamQuestions", b =>
                {
                    b.HasOne("TestExams.DBModel.Exam", "Exam")
                        .WithMany()
                        .HasForeignKey("ExamID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TestExams.DBModel.Question", "Question")
                        .WithMany()
                        .HasForeignKey("QuestionID");
                });

            modelBuilder.Entity("TestExams.DBModel.ExamType", b =>
                {
                    b.HasOne("TestExams.DBModel.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestExams.DBModel.Question", b =>
                {
                    b.HasOne("TestExams.DBModel.Theme", "Theme")
                        .WithMany()
                        .HasForeignKey("ThemeID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestExams.DBModel.Subject", b =>
                {
                    b.HasOne("TestExams.DBModel.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TestExams.DBModel.Theme", b =>
                {
                    b.HasOne("TestExams.DBModel.Subject", "Subjet")
                        .WithMany()
                        .HasForeignKey("SubjetSubjectID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
