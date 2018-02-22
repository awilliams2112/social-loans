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
                output.ToString().Replace("{{" + val.Key + " }}", val.Value);
            }

            return output;
        }
    }
}
