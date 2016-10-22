using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;

namespace TestExamsControls
{
    /// <summary>
    /// Lógica de interacción para AddSubjet.xaml
    /// </summary>
    public partial class AddSubjet : UserControl
    {

        #region properties

        /// <summary>
        /// Almacena el texto del botón aceptar
        /// </summary>
        public string Btn_AcceptText { get; set; }

        /// <summary>
        /// Almacena el texto del boón cancelar
        /// </summary>
        public string Btn_CancelText{ get; set; }

        /// <summary>
        /// Almacena el texto de la marca de agua del textbox
        /// </summary>
        public string Tbx_SubjetWaterMark { get; set; }

        /// <summary>
        /// Almacena el texto de la etiqueta de título 
        /// </summary>
        public string lab_TitleText { get; set; }

        #endregion

        public AddSubjet()
        {
            InitializeComponent();
        }

        private void AddSubjet_Loaded(object sender, RoutedEventArgs e)
        {
            btn_Accept.Content = Btn_AcceptText;
            btn_Cancel.Content = Btn_CancelText;
            TextBoxHelper.SetWatermark(Tbx_subjet, Tbx_SubjetWaterMark);
            lab_Title.Content = lab_TitleText;
        }

        private void btn_Accept_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {

        }




    }
}
