using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    /// <summary>
    /// Tabla temas
    /// </summary>
    public class Theme
    {
        public int ThemeID { get; set; }

        public string Name { get; set; }

        public virtual Subject Subjet { get; set; }
    }
}
