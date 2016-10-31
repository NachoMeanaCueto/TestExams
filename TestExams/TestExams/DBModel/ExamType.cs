﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    public class ExamType
    {
        public int ExamTypeID { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public virtual User User { get; set; }
    }
}
