using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialLoan.Utilities;
using System.Collections.Generic;

namespace SocialLoans.Utilites.Test
{
    [TestClass]
    public class BlendTests
    {
        [TestMethod]
        public void TokenValidation()
        {
            //arrange
            string templates = "{{first}} {{last}} walks the streets at night.";
       
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("first", "John");
            values.Add("last", "Doe");

            //act
            string result = Blender.Blend(templates, values);

            //assert
            Assert.IsFalse(result == templates);
            Assert.IsTrue(result.Contains("John Doe"));
        }
    }
}
