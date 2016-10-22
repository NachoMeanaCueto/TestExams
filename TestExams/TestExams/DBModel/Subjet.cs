using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    /// <summary>
    /// Tabla asignaturas
    /// </summary>
    public class Subject
    {
        public int SubjectID { get; set; }

        public string Name { get; set; }

        public virtual User User { get; set; }
    }
}
