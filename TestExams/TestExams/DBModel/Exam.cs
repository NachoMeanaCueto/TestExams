using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    public class Exam
    {
        public int ExamID { get; set; }
        
        public virtual User User { get; set; }

        public virtual List<Question> Questions { get; set; }

    }
}
