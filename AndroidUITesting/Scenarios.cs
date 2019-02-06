using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidUITesting
{
    class Scenarios : Fixtures
    {
        [Test]
        [TestCase(TestName = "**", Ignore = "pour CI",
         Author = "TESTING DIGITAL")]
        public void Hmm()
        {
        }
    }
}
