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

        #region usuarios

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

        #region Asignaturas

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
        static DBResult addSubjet(Subject subject)
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

        //TODO acabar asignaturas

        #endregion

        #region Temas

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
                            message = WrapperTranslation.GetValue("Error_UserNotFound").ToString()
                        };
                    }
                    else
                    {
                        db.Themes.Remove(theme);
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
        public static DBResult IsUsed(Theme theme)
        {
            DBResult Result = null;

            //try
            //{
            //    using (var db = new TestExamsContext())
            //    {
            //        if (!db.Users.Select(x => x.UserID == user.UserID).Any())
            //        {
            //            Result = new DBResult
            //            {
            //                result = true,
            //                message = WrapperTranslation.GetValue("Error_UserNotFound").ToString()
            //            };
            //        }
            //        else
            //        {
            //            if (db.Subjects.Select(x => x.User.UserID == user.UserID).Any()
            //                || db.Exams.Select(x => x.User.UserID == user.UserID).Any())
            //            {
            //                Result = new DBResult
            //                {
            //                    result = true,
            //                    message = WrapperTranslation.GetValue("Error_UserInUse").ToString()
            //                };
            //            }
            //            else
            //            {
            //                Result = new DBResult
            //                {
            //                    result = false,
            //                    message = WrapperTranslation.GetValue("Message_UserNotInUse").ToString()
            //                };
            //            }
            //        }
            //    }
            //}
            //catch (Exception e)
            //{
            //    Result = new DBResult
            //    {
            //        result = true,
            //        message = e.Message
            //    };
            //}

            return Result;
        }


        #endregion
    }
}
