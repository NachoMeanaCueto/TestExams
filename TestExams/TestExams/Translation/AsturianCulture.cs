using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.Translation
{
    /// <summary>
    /// AasturianCulture crea una cultureInfo para el asturianu
    /// </summary>
    public class AsturianCulture : CultureInfo
    {
        public AsturianCulture() : base("")
        {

        }
        public override String DisplayName
        {
            get { return "Asturiano (España)"; }
        }

        public override String EnglishName
        {
            get { return "Asturian (Spain)"; }
        }
        public override String Name
        {
            get { return "ast-ES"; }
        }
        public override String NativeName
        {
            get { return "Asturianu (Espa�a)"; }
        }
        public override CultureInfo Parent
        {
            get { return new CultureInfo("es-ES"); }
        }
        public override String ThreeLetterISOLanguageName
        {
            get { return "ast"; }
        }
        public override String ThreeLetterWindowsLanguageName
        {
            get { return "ast"; }
        }
       
        public override int GetHashCode()
        {
            return (this.Name.GetHashCode());
        }

    }

}
