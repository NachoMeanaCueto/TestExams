using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    public class ExamQuestions
    {
        public int ExamQuestionID { get; set; }

        public virtual Exam Exam { get; set; }

        public virtual Question Question { get; set; }
    }
}
