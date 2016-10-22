using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TestExams
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            using (var db = new DBModel.TestExamsContext())
            {
                var appMail = db.AppMails.Count();

                if (appMail == 0)
                {
                    db.AppMails.Add(new DBModel.AppMail
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
    }
}
