using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestExams.DBModel;

namespace TestExams
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static User CurrentUser { get; set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DBManager.DBManager.SetAppMail();
        }
    }
}
