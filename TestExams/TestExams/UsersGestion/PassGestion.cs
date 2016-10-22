using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.UsersGestion
{

    public static class PassGestion
    {
        public static string Encrypt(this string _StringToEncrypt)
        {
            string result = string.Empty;
            byte[] encryted = Encoding.Unicode.GetBytes(_StringToEncrypt);
            result = Convert.ToBase64String(encryted);
            return result;
        }

        public static string Decrypt(this string _StringToDecrypt)
        {
            string result = string.Empty;
            byte[] decryted = Convert.FromBase64String(_StringToDecrypt);
            result = Encoding.Unicode.GetString(decryted);
            return result;
        }
    }
    
}
