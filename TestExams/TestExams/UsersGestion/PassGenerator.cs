using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestExams.UsersGestion
{
    /// <summary>
    /// Generador de contraseñas aleatorias
    /// </summary>
    static class PassGenerator
    {
        /// <summary>
        /// Método para generar contraseñas
        /// </summary>
        /// <param name="length"> 
        /// Tamaño de la contraseña
        /// </param>
        /// <returns>
        /// String
        /// </returns>
        public static string Generate(int length)
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0};
            char[] Lowerletter = 
                { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q','r','s','t','u','v','w','x','y','z'};
            char[] Upperletter =
             { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q','R','S','T','U','V','W','X','Y','Z'};
            char[] specialCharacters = { '!', '¡', '?', '¿', '*', '&', '%' };

            StringBuilder pass = new StringBuilder();
            Random rand = new Random();
            int character;

            for (int i = 0; i < length; i++)
            {
                var charType = rand.Next(4);

                switch (charType)
                {
                    case 0:
                        character = rand.Next(numbers.Length);
                        pass.Append(numbers[character]);
                        break;
                    case 1:
                        character = rand.Next(Lowerletter.Length);
                        pass.Append(Lowerletter[character]);
                        break;
                    case 2:
                        character = rand.Next(Upperletter.Length);
                        pass.Append(Upperletter[character]);
                        break;
                    case 3:
                        character = rand.Next(specialCharacters.Length);
                        pass.Append(specialCharacters[character]);
                        break;
                }
            }

            return pass.ToString();
        }
       
    }
}
