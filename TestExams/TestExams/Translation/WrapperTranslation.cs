using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TestExams.Translation
{
    //Clase para traducir las cadenas de los recursos
    //http://geeks.ms/jyeray/2011/03/04/wpf-localizacin-de-aplicaciones/

    public class WrapperTranslation
    {
        private static ObjectDataProvider m_provider;

        public WrapperTranslation()
        {
        }

        public Translation GetResourceInstance()
        {
            return new Translation();
        }

        public static ObjectDataProvider ResourceProvider
        {
            get
            {
                if (m_provider == null)
                    m_provider = (ObjectDataProvider)App.Current.FindResource("TranslationRes");
                return m_provider;
            }
        }

        public static void ChangeCulture(CultureInfo culture)
        {
            Properties.Resources.Culture = culture;
            ResourceProvider.Refresh();
        }

        //Métodos para sacar una cadena de un recurso estático desde código
        //http://stackoverflow.com/questions/3577802/wpf-getting-a-property-value-from-a-binding-path
        public static object GetValue(string propertyPath)
        {
            Binding binding = new Binding(propertyPath);
            binding.Mode = BindingMode.OneWay;
            binding.Source = (ObjectDataProvider)App.Current.FindResource("TranslationRes");
            BindingOperations.SetBinding(_dummy, Dummy.ValueProperty, binding);
            return _dummy.GetValue(Dummy.ValueProperty);
        }

        private static readonly Dummy _dummy = new Dummy();

        private class Dummy : DependencyObject
        {
            public static readonly DependencyProperty ValueProperty =
                DependencyProperty.Register("Value", typeof(object), typeof(Dummy), new UIPropertyMetadata(null));
        }
    }
}
