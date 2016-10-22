using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TestExams.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;

namespace TestExams
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_PassLost_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_signIn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AddSubjet addsubjet = new AddSubjet
            {
                lab_TitleText = "Añadir asignatura",
                Tbx_SubjetWaterMark = "Introduzca una asaignatura....",
                Btn_AcceptText = "Aceptar",
                Btn_CancelText = "Cancelar"
            };

            MainGrid.Children.Clear();
            MainGrid.Children.Add(addsubjet);
            
            CustomValidationPopup po = new CustomValidationPopup();
           
            po.Child = new Label { Content = "pop", Foreground = Brushes.White, Background = Brushes.Red};
            po.VerticalOffset = 500;
            po.HorizontalOffset = 500; 
            po.IsOpen = true;
        }



        private void añadirAsig(object sender, RoutedEventArgs e)
        {
        }

        #region Flayout Lenguaje

        /// <summary>
        /// Cambia el lenguaje de la aplicación
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CbxLanguage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cbx = (ComboBox)sender;
            var item = (ComboBoxItem)cbx.SelectedItem;

            switch (item.Name)
            {
                case "EsItem":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("es-ES");
                    Translation.WrapperTranslation.ChangeCulture(Thread.CurrentThread.CurrentUICulture);
                    break;
                case "AsItem":
                    Thread.CurrentThread.CurrentUICulture = new Translation.AsturianCulture();
                    Translation.WrapperTranslation.ChangeCulture(Thread.CurrentThread.CurrentUICulture);
                    break;
            }
        }


        /// <summary>
        /// Abre o cierra el flayout
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LanguajeButton_Click(object sender, RoutedEventArgs e)
        {
            if (LogFlayout.IsOpen)
                LogFlayout.IsOpen = false;

            if (LanguajeFlayout.IsOpen)
                LanguajeFlayout.IsOpen = false;

            else
                LanguajeFlayout.IsOpen = true;
        }


        #endregion

        #region Flayout Login

        /// <summary>
        /// Abre o cierra el flayout del login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            if (LanguajeFlayout.IsOpen)
                LanguajeFlayout.IsOpen = false;

            if (LogFlayout.IsOpen)
                LogFlayout.IsOpen = false;
            else
                LogFlayout.IsOpen = true;
        }

        #endregion

    }
}
