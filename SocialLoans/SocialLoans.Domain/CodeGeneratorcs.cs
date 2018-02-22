using System;
using System.Collections.Generic;
using System.Text;

namespace SocialLoans.Domain
{
    public class CodeGenerators
    {

        public static string New(int codeLength)
        {
            Random rand = new Random();
            StringBuilder code = new StringBuilder();
            
            for(int i = 0; i < codeLength; i++)
            {
                code.Append(rand.Next(10));
            }

            return code.ToString();
        }
    }
}
