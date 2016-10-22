using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.DBModel
{
    public class AppMail
    {
        public int AppMailID { get; set; }

        public string MailAddress { get; set; }

        public string Host { get; set; }

        public string Password { get; set; }

        public int port { get; set; }

    }
}
