using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    /// <summary>
    /// Tabla respuestas
    /// </summary>
    public class Answer
    {
        public int AnswerId { get; set; }

        public string AnswerText { get; set; }

        public virtual Question Question { get; set; }

        public bool isCorrect { get; set; }
    }
}
