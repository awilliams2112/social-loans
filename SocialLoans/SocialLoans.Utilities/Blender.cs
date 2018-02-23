using System.Collections.Generic;
using System.Text;

namespace SocialLoan.Utilities
{
    public class Blender
    {
        public static string Blend(string template, Dictionary<string, string> tokens)
        {
            StringBuilder strBlr = new StringBuilder(template);

            string output = strBlr.ToString();

            foreach (var val in tokens)
            {

                string key = "{{" + val.Key + "}}";

                
                output = output.Replace(key, val.Value);

                string test = template.Replace(key, val.Value);
            }

            return output;
        }
    }
}
