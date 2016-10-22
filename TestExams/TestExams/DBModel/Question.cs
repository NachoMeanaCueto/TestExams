using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    /// <summary>
    /// Tabla preguntas
    /// </summary>
    public class Question
    {
        public int QuestionID { get; set; }

        public string QuestionText { get; set; }

        public virtual Theme Theme { get; set; }
    }
}
