using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace malshinon
{
    internal class Generate_secret_code
    {
        private string secretCode;
        public Generate_secret_code() 
        {
            
            StringBuilder secretCodeBuilder = new StringBuilder();
            Random random = new Random();
            for(int i = 0; i < 8 ; i++)
            {
                int ascii = random.Next(33, 126);
                char charcter = (char)ascii;
                secretCodeBuilder.Append(charcter);
            }
            secretCode = secretCodeBuilder.ToString();
        }
        public string Generate()
        {
            return secretCode;
        }
    }
}
