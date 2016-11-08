using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExams.DBModel;
using TestExams.Translation;

namespace TestExams.DBManager
{
    public static class DBManager
    {
        #region  AppMail

        public static void SetAppMail()
        {
            using (var db = new TestExamsContext())
            {
                var appMail = db.AppMails.Count();

                if (appMail == 0)
                {
                    db.AppMails.Add(new AppMail
                    {
                        AppMailID = 1,
                        Host = "smtp.gmail.com",
                        port = 25,
                        MailAddress = "textexams@gmail.com",
                        Password = "dABlAHgAdABlAHgAYQBtAHMAQQBkAG0AaQBuACEAMQAyADMANAA="
                    });

                    db.SaveChanges();
                }
            }
        }

        #endregion

        #region Users

        /// <summary>
        /// Añade un usuario a la base de datos.
        /// Comprueba que ni el nombre de usuario ni el email esten repetidos.
        /// </summary>
        /// <param name="user"> Objeto DB.User</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => usuario en uso
        /// DBResult.result = false => usuario sin uso
        /// </returns>
        public static DBResult addUser(User user)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (db.Users.Select(x => x.UserName == user.UserName).Any())
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("Error_UserRepeat").ToString()
                        };
                    }
                    else
                    {
                        if (db.Users.Select(x => x.email == user.email).Any())
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("Error_MailRepeat").ToString()
                            };
                        }
                        else
                        {
                            db.Users.Add(user);
                            db.SaveChanges();
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("Message_AddUserOk").ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra un usuario de la base de datos.
        /// Comprueba la existencia del usuario.
        /// </summary>
        /// <param name="user"> Objeto DB.User</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveUser(User user)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Users.Select(x => x.UserID == user.UserID).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("Error_UserNotFound").ToString()
                        };
                    }
                    else
                    {
                        db.Users.Remove(user);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Message_RemoveUserOk").ToString()
                        };
                    } 
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si el usuario está en uso
        /// </summary>
        /// <param name="user">Objeto DB.User</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => usuario en uso
        /// DBResult.result = false => usuario sin uso
        /// </returns>
        public static DBResult IsUsed(User user)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Users.Select(x => x.UserID == user.UserID).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Error_UserNotFound").ToString()
                        };
                    }
                    else
                    {
                        if (db.Subjects.Select(x => x.User.UserID == user.UserID).Any()
                            || db.Exams.Select(x => x.User.UserID == user.UserID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("Error_UserInUse").ToString()
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("Message_UserNotInUse").ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra en cascada los registros vunculados a un usuario
        /// </summary>
        /// <param name="user">Objeto DB.User</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Referencias borradas correctamente
        /// DBResult.result = false => Error
        /// </returns>
        public static DBResult RemoveUserReferences(User user)
        {
            DBResult result = null;
            try
            {
                using (var db = new TestExamsContext())
                {
                    var exams = db.Exams.Where(x => x.User.UserID == user.UserID);

                    foreach (var exam in exams)
                    {
                        var examquestions = db.ExamQuestions.Where(x => x.Exam.ExamID == exam.ExamID);
                        foreach (var examQuestion in examquestions)
                        {
                            db.ExamQuestions.Remove(examQuestion);
                            db.SaveChanges();
                        }

                        db.Exams.Remove(exam);
                        db.SaveChanges();
                    }

                    var subjets = db.Subjects.Where(x => x.User.UserID == user.UserID);
                    foreach (var subjet in subjets)
                    {
                        var themes = db.Themes.Where(x => x.Subjet.SubjectID == subjet.SubjectID);

                        foreach (var theme in themes)
                        {
                            var questions = db.Questions.Where(x => x.Theme.ThemeID == theme.ThemeID);

                            foreach (var question in questions)
                            {
                                var answers = db.Answers.Where(x => x.Question.QuestionID == question.QuestionID);

                                foreach (var answer in answers)
                                {
                                    db.Answers.Remove(answer);
                                    db.SaveChanges();
                                }

                                db.Questions.Remove(question);
                                db.SaveChanges();
                            }

                            db.Themes.Remove(theme);
                            db.SaveChanges();
                        }
                    }

                    result = new DBResult
                    {
                        result = true,
                    };
                }
            }
            catch (Exception e)
            {
                result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return result;
        }

        /// <summary>
        /// Borra el usuario y sus referencias
        /// </summary>
        /// <param name="user">Objeto DB.User</param>
        /// <returns></returns>
        public static DBResult RemoveUserAndReferences(User user)
        {
            DBResult Result = null;
            try
            {
                var RemoveUserReferencesResult = RemoveUserReferences(user);
                if (RemoveUserReferencesResult.result)
                {
                    var RemoveUserResult = RemoveUser(user);
                  if (RemoveUserResult.result)
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = RemoveUserResult.message
                        };
                    }
                    else
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = RemoveUserResult.message
                        };
                    }
                }
                else
                {
                    Result = new DBResult
                    {
                        result = false,
                        message = RemoveUserReferencesResult.message
                    };
                }
 
            }
            catch(Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Devuelve el usuario en sesión
        /// </summary>
        /// <returns>Objeto DB.User</returns>
        public static User GetCurrentUser()
        {
            return App.CurrentUser;
        }
        #endregion

        #region Subjets

        /// <summary>
        /// Añade una asignatura a la base de datos.
        /// Comprueba que el nombre  no esté repetido.
        /// </summary>
        /// <param name="subject"> Objeto DB.Subjet</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Asignatura añadida correctamente
        /// DBResult.result = false => Asignatura no añadida
        /// </returns>
        static DBResult AddSubjet(Subject subject)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if ((db.Subjects.Select(x => x.Name == subject.Name && x.User.UserID == subject.User.UserID).Any()))
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("Error_SubjetRepeat").ToString()
                        };
                    }
                    else
                    {

                        db.Subjects.Add(subject);
                        db.SaveChanges();
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Message_AddSubjetOk").ToString()
                        };
                        
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra un tema de la base de datos.
        /// Comprueba la existencia.
        /// </summary>
        /// <param name="user"> Objeto DB.theme</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveSUbjet(Subject subject)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Subjects.Select(x => x.SubjectID == subject.SubjectID).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("Error_SubjetNotFound").ToString()
                        };
                    }
                    else
                    {
                        db.Subjects.Remove(subject);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Message_RemoveSubjetOk").ToString()
                        };
                    }
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si la asignatura está en uso
        /// </summary>
        /// <param name="user">Objeto DB.Subjet</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => asignatura en uso
        /// DBResult.result = false => asignatura sin uso
        /// </returns>
        public static DBResult IsUsed(Subject subjet)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Subjects.Select(x => x.SubjectID == subjet.SubjectID).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Error_SubjetNotFound").ToString()
                        };
                    }
                    else
                    {
                        if (db.Themes.Select(x => x.Subjet.SubjectID == subjet.SubjectID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("Error_SubjetInUse").ToString()
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("Message_SubjetNotInUse").ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }

        public static IEnumerable<Subject> GetSubjetsByCurrentUser()
        {
            using (var db = new TestExamsContext())
            {
                return db.Subjects.Where(x => x.User.UserID == GetCurrentUser().UserID);
            }
        }
        #endregion

        // TODO translates
        #region Themes

        /// <summary>
        /// Añade un tema a la base de datos.
        /// Comprueba que no esté repetido.
        /// </summary>
        /// <param name="theme"> Objeto DB.Theme</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Accion correcta
        /// DBResult.result = false => Accion incorrecta
        /// </returns>
        public static DBResult addTheme(Theme theme)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (db.Themes.Select(x => x.Name == theme.Name && x.Subjet.User.UserID == GetCurrentUser().UserID).Any())
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("Error_ThemeRepeat").ToString()
                        };
                    }
                    else
                    {

                       db.Themes.Add(theme);
                       db.SaveChanges();
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Message_AddThemeOk").ToString()
                        };
                        
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra un tema de la base de datos.
        /// Comprueba la existencia.
        /// </summary>
        /// <param name="user"> Objeto DB.theme</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveTheme(Theme theme)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Themes.Select(x => x.ThemeID == theme.ThemeID).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("Error_ThemeNotFound").ToString()
                        };
                    }
                    else
                    {
                        db.Themes.Remove(theme);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("Message_RemoveThemeOk").ToString()
                        };
                    }
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si el tema está en uso
        /// </summary>
        /// <param name="user">Objeto DB.theme</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => usuario en uso
        /// DBResult.result = false => usuario sin uso
        /// </returns>
        public static DBResult IsUsed(Theme theme)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Themes.Select(x => x.ThemeID == theme.ThemeID).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Error, el tema no existe
                        };
                    }
                    else
                    {
                        if (db.Questions.Select(x => x.Theme.ThemeID == theme.ThemeID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("").ToString() // Error, tema en uso
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("").ToString() // tema sin uso
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }


        public static IEnumerable<Theme> GetThemesByCurrentUser()
        {
            using (var db = new TestExamsContext())
            {
                return db.Themes.Where(x => x.Subjet.User.UserID == GetCurrentUser().UserID);
            }
        }

        #endregion

        // TODO translates
        #region Questions

        /// <summary>
        /// Añade una pregunta a la base de datos.
        /// Comprueba que no esté repetida.
        /// </summary>
        /// <param name="question"> Objeto DB.question</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Accion correcta
        /// DBResult.result = false => Accion incorrecta
        /// </returns>
        public static DBResult addQuestion(Question question)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (db.Questions.Select(x => x.QuestionText == question.QuestionText && x.Theme.ThemeID == question.Theme.ThemeID).Any())
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, Pregunta repetida
                        };
                    }
                    else
                    {

                        db.Questions.Add(question);
                        db.SaveChanges();
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Pregunta añadida correctamente
                        };

                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra una pregunta de la base de datos.
        /// Comprueba la existencia.
        /// </summary>
        /// <param name="user"> Objeto DB.theme</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveQuestion(Question question)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Questions.Select(x => x.QuestionID == question.QuestionID).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, la pregunta no existe
                        };
                    }
                    else
                    {
                        db.Questions.Remove(question);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()//Pregunta borrada correctamente
                        };
                    }
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si la pregunta está en uso
        /// </summary>
        /// <param name="user">Objeto DB.Question</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => en uso
        /// DBResult.result = false => sin uso
        /// </returns>
        public static DBResult IsUsed(Question question)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Questions.Select(x => x.QuestionID == question.QuestionID).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Error, la pregunta no existe
                        };
                    }
                    else
                    {
                        if (db.Answers.Select(x => x.Question.QuestionID == question.QuestionID).Any()
                            || db.ExamQuestions.Select(x => x.Question.QuestionID == question.QuestionID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("").ToString() // Error, pregunta en uso
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("").ToString() // pregunta sin uso
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }

        public static IEnumerable<Question> GetQuestionByTheme(Theme theme)
        {
            using (var db = new TestExamsContext())
            {
                return db.Questions.Where(x => x.Theme.ThemeID == theme.ThemeID);
            }
        }


        public static IEnumerable<Question> GetQuestionBySubjet(Subject subject)
        {
            using (var db = new TestExamsContext())
            {
                return db.Questions.Where(x => x.Theme.Subjet.SubjectID == subject.SubjectID);
            }
        }

        #endregion

        //TODO translates
        #region Answers

        /// <summary>
        /// Añade una respuesta a la base de datos.
        /// Comprueba que no esté repetida.
        /// </summary>
        /// <param name="answer"> Objeto DB.Answer</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Accion correcta
        /// DBResult.result = false => Accion incorrecta
        /// </returns>
        public static DBResult addAnswer(Answer answer)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (db.Answers.Select(x => x.AnswerText == answer.AnswerText && x.Question.QuestionID == answer.Question.QuestionID).Any())
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, Respuesta repetida
                        };
                    }
                    else
                    {

                        db.Answers.Add(answer);
                        db.SaveChanges();
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Respuesta añadida correctamente
                        };

                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra una respuesta de la base de datos.
        /// Comprueba la existencia.
        /// </summary>
        /// <param name="answer"> Objeto DB.Answer</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveAnswer(Answer answer)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Answers.Select(x => x.AnswerId == answer.AnswerId).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, la respuesta no existe
                        };
                    }
                    else
                    {
                        db.Answers.Remove(answer);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()//Respuesta borrada correctamente
                        };
                    }
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si la respuesta está en uso
        /// </summary>
        /// <param name="answer">Objeto DB.Answer</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => en uso
        /// DBResult.result = false => sin uso
        /// </returns>
        public static DBResult IsUsed(Answer answer)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Answers.Select(x => x.AnswerId == answer.AnswerId).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Error, la respuesta no existe
                        };
                    }
                    else
                    {
                        if (db.Questions.Select(x => x.QuestionID == answer.Question.QuestionID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("").ToString() // Error, respuesta en uso
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("").ToString() // respuesta sin uso
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Devuelve las respuestas a una pregunta
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static IEnumerable<Answer> GetAnswersByQuestion(Question question)
        {
            using (var db = new TestExamsContext())
            {
                return db.Answers.Where(x => x.Question.QuestionID == question.QuestionID);
            }
        }
        #endregion

        //TODO translates
        #region Exams

        /// <summary>
        /// Añade un examen a la base de datos.
        /// </summary>
        /// <param name="exam"> Objeto DB.Exam</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Accion correcta
        /// DBResult.result = false => Accion incorrecta
        /// </returns>
        public static DBResult addExam(Exam exam)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    
                    db.Exams.Add(exam);
                    db.SaveChanges();
                    Result = new DBResult
                    {
                        result = true,
                        message = WrapperTranslation.GetValue("").ToString()// examen añadido correctamente
                    };

                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra un examen de la base de datos.
        /// Comprueba la existencia.
        /// </summary>
        /// <param name="exam"> Objeto DB.Exam</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveExam(Exam exam)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Exams.Select(x => x.ExamID == exam.ExamID && x.User.UserID == exam.User.UserID).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, el examen no existe
                        };
                    }
                    else
                    {
                        db.Exams.Remove(exam);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()//Respuesta borrada correctamente
                        };
                    }
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si la respuesta está en uso
        /// </summary>
        /// <param name="exam">Objeto DB.Answer</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => en uso
        /// DBResult.result = false => sin uso
        /// </returns>
        public static DBResult IsUsed(Exam exam)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.Exams.Select(x => x.ExamID == exam.ExamID && x.User.UserID == exam.User.UserID).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Error, el examen no existe
                        };
                    }
                    else
                    {
                        if (db.ExamQuestions.Select(x => x.Exam.ExamID == exam.ExamID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("").ToString() // Error, examen en uso
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("").ToString() // respuesta sin uso
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Devuelve los examenes del usuario en sesión
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Exam> GetAllExamsByCurrentUser()
        {
            using (var db = new TestExamsContext())
            {
                return db.Exams.Where(x => x.User.UserID == GetCurrentUser().UserID);
            }
        }


        /// <summary>
        /// Devuelve los examenes del usuario en sesión de un tema concreto
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static IEnumerable<Exam> GetThemeExamsByCurrentUser(Theme theme)
        {
            using (var db = new TestExamsContext())
            {
                var QuestionsByTheme = db.Questions.Where(y => y.Theme.ThemeID == theme.ThemeID)
                    .Select(z => z.QuestionID);

                var examquestionsByTheme = (from examquestions in db.ExamQuestions
                                            where (QuestionsByTheme.Contains(examquestions.Question.QuestionID))
                                            select examquestions.Exam.ExamID);

                var themeExamsByUser = from themeExams in GetAllExamsByCurrentUser()
                                       where (examquestionsByTheme.Contains(themeExams.ExamID))
                                       select themeExams;

                return themeExamsByUser;
            }
        }


        /// <summary>
        /// Devuelve los examenes del usuario en sesión de una asignatura concreta
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static IEnumerable<Exam> GetSubjetExamsByCurrentUser(Subject subjet)
        {
            using (var db = new TestExamsContext())
            {
                var ThemesBySubjet = db.Themes.Where(x => x.Subjet.SubjectID == subjet.SubjectID)
                                    .Select(x => x.ThemeID);

                var QuestionsBySubjet = (from questions in db.Questions
                                         where (ThemesBySubjet.Contains(questions.Theme.ThemeID))
                                         select questions.QuestionID);

                var examquestionsByTheme = (from examquestions in db.ExamQuestions
                                            where (QuestionsBySubjet.Contains(examquestions.Question.QuestionID))
                                            select examquestions.Exam.ExamID);

                var SubjetExamsByUser = from themeExams in GetAllExamsByCurrentUser()
                                       where (examquestionsByTheme.Contains(themeExams.ExamID))
                                       select themeExams;

                return SubjetExamsByUser;
            }
        }

        #endregion

        //TODO translates
        #region Examquestions

        /// <summary>
        /// Añade un vinculo entre examen y pregunta a la base de datos.
        /// Comprueba que no esté repetida.
        /// </summary>
        /// <param name="question"> Objeto DB.ExamQuestions</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Accion correcta
        /// DBResult.result = false => Accion incorrecta
        /// </returns>
        public static DBResult addExamQuestion(ExamQuestions examQuestion)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (db.ExamQuestions.Select(x => x.Exam.ExamID == examQuestion.Exam.ExamID
                        && x.Question.QuestionID == examQuestion.Question.QuestionID).Any())
                    {
                        Result = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, Pregunta repetida
                        };
                    }
                    else
                    {

                        db.ExamQuestions.Add(examQuestion);
                        db.SaveChanges();
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Pregunta añadida correctamente
                        };

                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return Result;
        }

        /// <summary>
        /// Borra un vinculo entre examen y pregunta de la base de datos.
        /// Comprueba la existencia.
        /// </summary>
        /// <param name="user"> Objeto DB.ExamQuestions</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        public static DBResult RemoveExamQuestion(ExamQuestions examQuestion)
        {
            DBResult res = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.ExamQuestions.Select(x => x.ExamQuestionID == examQuestion.ExamQuestionID).Any())
                    {
                        res = new DBResult
                        {
                            result = false,
                            message = WrapperTranslation.GetValue("").ToString()// Error, la pregunta no existe
                        };
                    }
                    else
                    {
                        db.ExamQuestions.Remove(examQuestion);
                        db.SaveChanges();
                        res = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()//Pregunta borrada correctamente
                        };
                    }
                }
            }
            catch (Exception e)
            {
                res = new DBResult
                {
                    result = false,
                    message = e.Message
                };
            }

            return res;
        }

        /// <summary>
        /// Comprueba si la pregunta está en uso
        /// </summary>
        /// <param name="user">Objeto DB.Question</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => en uso
        /// DBResult.result = false => sin uso
        /// </returns>
        public static DBResult IsUsed(ExamQuestions examQuestion)
        {
            DBResult Result = null;

            try
            {
                using (var db = new TestExamsContext())
                {
                    if (!db.ExamQuestions.Select(x => x.ExamQuestionID == examQuestion.ExamQuestionID).Any())
                    {
                        Result = new DBResult
                        {
                            result = true,
                            message = WrapperTranslation.GetValue("").ToString()// Error, la pregunta no existe
                        };
                    }
                    else
                    {
                        if (db.Exams.Select(x => x.ExamID == examQuestion.Exam.ExamID).Any()
                            || db.Questions.Select(x => x.QuestionID == examQuestion.Question.QuestionID).Any())
                        {
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("").ToString() // Error, pregunta en uso
                            };
                        }
                        else
                        {
                            Result = new DBResult
                            {
                                result = false,
                                message = WrapperTranslation.GetValue("").ToString() // pregunta sin uso
                            };
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Result = new DBResult
                {
                    result = true,
                    message = e.Message
                };
            }

            return Result;
        }

        public static IEnumerable<ExamQuestions> GetExamQuestionsByExam(Exam exam)
        {
            using (var db = new TestExamsContext())
            {
                return db.ExamQuestions.Where(x => x.Exam.ExamID == exam.ExamID);
            }
        }

        #endregion

        #region ExamTypes
        #endregion
    }
}
