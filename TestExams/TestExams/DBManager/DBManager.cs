using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestExams.DBModel;
using TestExams.Translation;

namespace TestExams.DBManager
{
    static class DBManager
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
        static DBResult addUser(User user)
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
        /// Comprueba la existencia del usuario y si este está en uso.
        /// </summary>
        /// <param name="user"> Objeto DB.User</param>
        /// <returns>
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => Borrado correctamente
        /// DBResult.result = false => No se pudo borrar
        /// </returns>
        static DBResult RemoveUser(User user)
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
                            result = false,
                            message = WrapperTranslation.GetValue("Error_UserNotFound").ToString()
                        };
                    }
                    else
                    {
                        if((Result = IsUsed(user)).result)
                        {
                        }
                        else
                        {
                            db.Users.Remove(user);
                            db.SaveChanges();
                            Result = new DBResult
                            {
                                result = true,
                                message = WrapperTranslation.GetValue("Message_RemoveUserOk").ToString()
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
        /// Comprueba si el usuario está en uso
        /// </summary>
        /// <param name="user">Objeto DB.User</param>
        /// <returns>        
        /// objeto DBResult {bool result , string message}
        /// DBResult.result = true => usuario en uso
        /// DBResult.result = false => usuario sin uso
        /// </returns>
        static DBResult IsUsed(User user)
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

        //TODO borrar usuario en cascada

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
        //TODO acabar temas
        #endregion
    }
}
